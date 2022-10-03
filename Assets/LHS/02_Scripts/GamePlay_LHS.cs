using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GamePlay_LHS : MonoBehaviourPun
{

    //**** ���� �߰� ���� ��ư �������� �� ��ȯ -> ��Ʈ��ũ �� ����
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
