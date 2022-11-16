using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MakeBomb : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    PlayerController player;
    Vector2 gridPosition;

    public PhotonView PV;
    // Update is called once per frame
    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (PV.IsMine) {
            GetGridPosition();
            BombInstallation();
        }
    }
    void GetGridPosition()
    {
        Vector3 behind = transform.TransformDirection(Vector3.back) * 1;
        Debug.DrawRay(transform.position, behind, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, behind, 1, LayerMask.GetMask("Ground"));
        gridPosition = rayHit.transform.position;
    }
    
    void BombInstallation()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.bombsNum != 0) {
            
            player.bombsNum -= 1;
            PhotonNetwork.Instantiate("Bomb", gridPosition, transform.rotation);

            StartCoroutine(BombTimer()); 

        }
    }
    IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(2f);
        player.bombsNum += 1;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
    
}
