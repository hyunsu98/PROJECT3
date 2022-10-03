using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GamePlay_LHS : MonoBehaviourPun
{

    //**** 현숙 추가 접속 버튼 눌렀을때 씬 전환 -> 네트워크 시 변경
    public void MainSceneChange()
    {
        photonView.RPC("MoveMainScene", RpcTarget.All);
    }
    [PunRPC]
    void MoveMainScene()
    {
        PhotonNetwork.LoadLevel(GameManager.instance.mapName);
    }

}
