using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameLobbyPlayer_HJH : MonoBehaviourPun
{

    MouseOnCharacterSelect_HJH mouse;

    // Start is called before the first frame update
    void Start()
    { 
        // ���� �κ� �Ŵ������� �÷��̾� ������ �� �� �ְ�
        GameLobbyManager_HJH.instance.AddPlayer(photonView);
        mouse = GameObject.Find("PlayerChoice(Clone)").GetComponent<MouseOnCharacterSelect_HJH>();
        mouse.ui.Add(this.gameObject);
        // �����Ҷ����� ���ӸŴ������� ���� -> �÷��̾� ������ ���� 
        if (photonView.IsMine)
        {
            mouse.whoConnectThis = photonView.ViewID;
        }
    }

    void Update()
    {
        
    }
}
