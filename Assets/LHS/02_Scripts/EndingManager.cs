using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class EndingManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.LeaveRoom();
    }
    public GameObject BGM;
    // Quit Ŭ�� �� ���� ������
    public void Quit()
    {
        Application.Quit();
    }
    public void Lobby()
    {
        GameManager.instance.SetTriggers();
        string nick = PhotonNetwork.NickName;
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.NickName = nick;
        PhotonNetwork.JoinLobby();
        Instantiate(BGM);
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        //LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("LobbyScene_LHS");



    }
}
