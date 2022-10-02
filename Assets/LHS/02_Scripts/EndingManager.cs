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
    // Quit 클릭 시 게임 나가기
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
        //LobbyScene으로 이동
        PhotonNetwork.LoadLevel("LobbyScene_LHS");



    }
}
