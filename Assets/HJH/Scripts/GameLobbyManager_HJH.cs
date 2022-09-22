using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameLobbyManager_HJH : MonoBehaviour
{
    public List<PhotonView> playerPhoton = new List<PhotonView>();
    public static GameLobbyManager_HJH instance;
    public int selectPeople = 0;

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
            //GameObject tg = GameObject.Find("ToggleGroup(Clone)");
            //Debug.Log(tg.name);
            //for(int i =0; i<4; i++)
            //{
            //    toggles[i] = tg.transform.GetChild(i).GetComponent<Toggle>();
            //}
            //for (int i = 0; i < toggles.Length; i++)
            //{
            //    toggles[i].interactable = false;
            //}
            //for (int i = 0; i < mapButtons.Length; i++)
            //{
            //    mapButtons[i].interactable = false;
            //}
        }
        else
        {
            GameObject tg = PhotonNetwork.Instantiate("ToggleGroup", Vector3.zero, Quaternion.identity);

            GameObject mc = PhotonNetwork.Instantiate("MapChoice", Vector3.zero, Quaternion.identity);

            GameObject pc = PhotonNetwork.Instantiate("PlayerChoice", Vector3.zero, Quaternion.identity);
            
        }
        GameObject ct = PhotonNetwork.Instantiate("Character", Vector3.zero, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && selectPeople == playerPhoton.Count)
        {
            GameObject bt = GameObject.Find("BtnPlay");
            bt.GetComponent<Button>().interactable = true;
        }
    }

    //게임로비플레이어의 포톤뷰가 들어감

    public void AddPlayer(PhotonView pv, GameLobbyPlayer_HJH i)
    {
        playerPhoton.Add(pv);
        //게임로비플레이어한테 너가 몇번째인지 알려
    }
}