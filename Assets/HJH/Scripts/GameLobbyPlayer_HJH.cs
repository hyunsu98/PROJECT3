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
        GameLobbyManager_HJH.instance.AddPlayer(photonView,this);
        mouse = GameObject.Find("PlayerChoice").GetComponent<MouseOnCharacterSelect_HJH>();
        mouse.whoConnectThis = playerNum;
    }
    void Update()
    {
        
    }
}
