using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameLobbyPlayer_HJH : MonoBehaviourPun
{

    public int playerNum;
    MouseOnCharacterSelect_HJH mouse;

    // Start is called before the first frame update
    void Start()
    { 
        // ���� �κ� �Ŵ������� �÷��̾� ������ �� �� �ְ�
        GameLobbyManager_HJH.instance.AddPlayer(photonView, this);
        mouse = GameObject.Find("PlayerChoice(Clone)").GetComponent<MouseOnCharacterSelect_HJH>();
        mouse.ui.Add(this.gameObject);
        // �����Ҷ����� ���ӸŴ������� ���� -> �÷��̾� ������ ���� 
        if (photonView.IsMine)
        {
            playerNum = PhotonNetwork.CountOfPlayersInRooms;
            mouse.whoConnectThis = playerNum;
            
            transform.SetAsLastSibling();
        }

    }

    void Update()
    {
        
    }
}
