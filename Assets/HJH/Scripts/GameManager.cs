using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public int roomMember = 4;
    public static GameManager instance;
    public int startLife = 3; //모든 게임메니저에 보내야됨
    public GameObject[] players;
    [SerializeField]
    public Dictionary<int, PlayerCharcter> playerCharcters = new Dictionary<int, PlayerCharcter>();
    public GameObject[] characterPrefabs;
    bool MainsceneStartTrigger = true; // 안보내도됨
    bool lobbySceneStartTrigger = true;
    public string RoomName = "?";

    public enum PlayerCharcter
    {
        Aland,
        Alice,
        Warrior,
        Archer,
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if((SceneManager.GetActiveScene().name == "MainScene_HJH" || SceneManager.GetActiveScene().name == "MainScene_Photon") && MainsceneStartTrigger == true)
        {
            MainScene();
        }
        else if(SceneManager.GetActiveScene().name == "GameLobbyScene_LHS" && lobbySceneStartTrigger == true)
        {
            GameLobbySene();
        }
    }

    private void GameLobbySene()
    {
        players = new GameObject[PhotonNetwork.CurrentRoom.MaxPlayers];
        GameObject.Find("Background_Text").GetComponent<Text>().text = RoomName;
        lobbySceneStartTrigger = false;
    }

    void MainScene()
    {
        PhotonNetwork.Instantiate("MainSceneManager", Vector3.zero, Quaternion.identity);
        Camera.main.GetComponent<CameraMove3D_LHS>().StartSetting();
        MainsceneStartTrigger = false;
    }

}
