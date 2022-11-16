using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    //movement
    public Rigidbody2D rigid;
    Animator anim;
    Vector2 moveVec;
    float vertical;
    float horizontal;

    [Header("Stats")]
    public int speed = 100;
    public int bombsNum = 1; 
    public float power = 1.4f;

    //Photon 
    public PhotonView PV;
    public Text NickName;
    void Awake()
    {
        //�г��� ����   
        NickName.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickName.color = PV.IsMine ? Color.green : Color.red;
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        if (PV.IsMine) {
            Move();
            AnimationController();
        }
        
    }

    void Move() 
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        if (vertical == 0 || horizontal == 0) { // �밢�� �������� �������� �ʵ���
            moveVec = new Vector2(horizontal, vertical); // moveVec only have one value vertical or horizontal
            //Debug.Log(vertical + " " + horizontal);
            rigid.velocity = moveVec * speed * Time.deltaTime;
        }
    }
    void AnimationController()
    {
        //�������� ��ǲ�� ���� �׹����� animation ����
        anim.SetInteger("vertical", (int)moveVec.y); 
        anim.SetInteger("horizontal", (int)moveVec.x);


        if(moveVec != Vector2.zero) {
            anim.SetBool("isMove", true);
        }
        else anim.SetBool("isMove", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PV.IsMine) {
            if (collision.transform.tag == "Skate") {
                Debug.Log(collision.transform.name);
                speed += 10;
            }
            if (collision.transform.tag == "Ballon") {
                Debug.Log(collision.transform.name);
                bombsNum += 1;
            }
            if (collision.transform.tag == "Power_Potion") {
                Debug.Log(collision.transform.name);
                power += 2f;
            }
            if (collision.transform.tag == "Potion") {
                Debug.Log(collision.transform.name);
                power += 1f;
            }
        }
    }
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(speed);
            stream.SendNext(bombsNum);
            stream.SendNext(power);
        }
        else {
            speed = (int)stream.ReceiveNext();
            bombsNum = (int)stream.ReceiveNext();
            power = (float)stream.ReceiveNext();
        }
        
        
    }

}
