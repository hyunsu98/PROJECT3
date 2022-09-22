using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameLobbyManager_HJH : MonoBehaviour
{
    public List<PhotonView> playerPhoton = new List<PhotonView>();
    public static GameLobbyManager_HJH instance;

   
    private void Awake()
    {
        instance = this;
    }
    public Toggle[] toggles;
    public Button[] mapButtons;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            for(int i =0; i<toggles.Length; i++)
            {
                toggles[i].interactable = false;
            }
            for(int i = 0; i< mapButtons.Length; i++)
            {
                mapButtons[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //게임로비플레이어의 포톤뷰가 들어감
    public void AddPlayer(PhotonView pv,GameLobbyPlayer_HJH i)
    {
        playerPhoton.Add(pv);
        //게임로비플레이어한테 너가 몇번째인지 알려줌
        i.playerNum = playerPhoton.Count;
    }
}
