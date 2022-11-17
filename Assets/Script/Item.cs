using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Item : MonoBehaviourPunCallbacks
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.transform.tag == "Player") { 
            Destroy(gameObject);
        }
    }
  
}
