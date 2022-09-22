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
        // �����Ҷ����� ���ӸŴ������� ���� -> �÷��̾� ������ ���� 
        if (photonView.IsMine)
        {
            StartCoroutine(StartWhat());
        }

    }
    IEnumerator StartWhat()
    {
        yield return new WaitForSeconds(0.5f);
        playerNum = GameLobbyManager_HJH.instance.playerPhoton.Count;    
        Debug.Log(playerNum);
            mouse = GameObject.Find("PlayerChoice(Clone)").GetComponent<MouseOnCharacterSelect_HJH>();
        mouse.whoConnectThis = playerNum;
            mouse.ui.Add(this.gameObject);
    }
    void Update()
    {
        
    }
}
