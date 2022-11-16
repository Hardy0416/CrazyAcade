using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BombController : MonoBehaviourPunCallbacks
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    [SerializeField]
    Stats stats;

    
    Vector2 _position;


    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    bool explosionTime = false;
    

    void Start()
    {
        _position = transform.position;
        stats = GameObject.Find("StatsManager").GetComponent<Stats>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InteractionByRay();
        StartCoroutine(ExplosionTimer()); 
    }



    void InteractionByRay( ){
        Vector2[] positionArray = new[]
        { new Vector2(0,stats.Power),  //up
          new Vector2(0,-stats.Power), //down
          new Vector2(-stats.Power,0), //left
          new Vector2(stats.Power,0) };//right

        Vector2 myVec = new Vector2(rigid.position.x , rigid.position.y);
       
        for(int i = 0; i < 4; i++) { // up side down
            Debug.DrawRay(myVec, positionArray[i], new Color(0, 1, 1));
            RaycastHit2D rayHit = Physics2D.Raycast(myVec, positionArray[i], stats.Power, LayerMask.GetMask("Enable"));
            if(rayHit.collider != null) {
                BreakBrick(rayHit.transform.gameObject);
            }
        } 
    }

    void BreakBrick(GameObject target)
    {
        if(target.transform.tag == "Brick" && explosionTime) {
            
            Destroy(target.transform.gameObject);
        }
    }

    IEnumerator ExplosionTimer(){
        
        yield return new WaitForSeconds(2f);

        if (explosionTime == false) {
            Explosion explosionStart = Instantiate(explosionPrefab, _position, Quaternion.identity);
            explosionStart.SetActiveRenderer(explosionStart.start);
            Destroy(explosionStart.gameObject, 0.7f);

            Explode(_position, Vector2.up, stats.Power - 0.4f);
            Explode(_position, Vector2.down, stats.Power - 0.4f);
            Explode(_position, Vector2.left, stats.Power - 0.4f);
            Explode(_position, Vector2.right, stats.Power - 0.4f);
            explosionTime = true;
        }
        spriteRenderer.enabled = false;

        Destroy(gameObject, 0.7f);


    }

    void Explode(Vector2 position, Vector2 direction, float length)
    {
        if (length <= 0) {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask)) {
            return;
        }

        Explosion cloneExplosion = Instantiate(explosionPrefab, position, Quaternion.identity); ;
        cloneExplosion.SetActiveRenderer(length > 1 ? cloneExplosion.middle : cloneExplosion.end);
        cloneExplosion.SetDirection(direction);
        Destroy(cloneExplosion.gameObject, 0.7f);

        Explode(position, direction, length-1);
    }
}
