using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MakeBomb : MonoBehaviourPunCallbacks, IPunObservable
{

    public GameObject bomb;
    [SerializeField]
    Stats stats;
    Vector2 gridPosition;

    public PhotonView PV;
    // Update is called once per frame
    void Start()
    {
        stats = GameObject.Find("StatsManager").GetComponent<Stats>();
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
        if (Input.GetKeyDown(KeyCode.Space) && stats.BombsNum != 0) {
            
            stats.BombsNum -= 1;
            PhotonNetwork.Instantiate("bomb", gridPosition, transform.rotation);

            StartCoroutine(BombTimer()); 

        }
    }
    IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(2f);
        stats.BombsNum += 1;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
