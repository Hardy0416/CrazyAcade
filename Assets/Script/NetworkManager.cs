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
        //Phonton������ ����ȭ
        Screen.SetResolution(1200, 800, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings(); // ����

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; // ���� �����÷��̾� �г����� �г��� ��ǲ�� �ִ´�.
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 4}, null);// �ִ��ο� 2���� ������ �� �ִ� ���� �����
    }
    public override void OnJoinedRoom()
    {
        DisconnectPaner.SetActive(false);// ������� ���� â�� ��Ȱ��ȭ ��Ų��
        Spawn();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&& PhotonNetwork.IsConnected) { 
            PhotonNetwork.Disconnect(); // ���ӵǾ��ִ� ���¿��� ESC�� ������ ��������
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
