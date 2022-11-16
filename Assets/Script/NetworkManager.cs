using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPaner;
    public GameObject RespqwnPanel;


    private void Awake()
    {
        //Phonton서버와 동기화
        Screen.SetResolution(1200, 800, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings(); // 접속

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; // 포톤 로컬플레이어 닉네임을 닉네임 인풋에 넣는다.
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 4}, null);// 최대인원 2명이 참여할 수 있는 방을 만든다
    }
    public override void OnJoinedRoom()
    {
        DisconnectPaner.SetActive(false);// 연결되지 않음 창을 비활성화 시킨다
        Spawn();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&& PhotonNetwork.IsConnected) { 
            PhotonNetwork.Disconnect(); // 접속되어있는 상태에서 ESC를 누르면 접속종료
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPaner.SetActive(true);
        RespqwnPanel.SetActive(false);
    }
    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        RespqwnPanel.SetActive(false);
        
    }
}
