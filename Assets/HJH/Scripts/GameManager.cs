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
    bool endingSceneStartTrigger = true;
    public string RoomName = "?";
    public string Winname;

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
        else if (SceneManager.GetActiveScene().name.Contains("EndingScene") && endingSceneStartTrigger == true)
        {
            EndingScene();
        }
        else if(MainsceneStartTrigger == false)
        {
            int a = 0;
            GameObject go = null;
            for(int i =0; i<players.Length; i++)
            {
                if(players[i] != null)
                {
                    if (players[i].GetComponent<Respawn_LHS>().RespawnCount == 0)
                    {
                        a++;
                    }
                    else
                    {
                        go = players[i];
                    }
                }     
            }
            if(a == players.Length-1)
            {
                if(go.name.Contains("Aland"))
                {
                    Winname = go.GetPhotonView().Owner.NickName; 
                    SceneManager.LoadScene("EndingSceneAland_LHS");
                }
                else if (go.name.Contains("Warrior"))
                {
                    Winname = go.GetPhotonView().Owner.NickName;
                    SceneManager.LoadScene("EndingSceneWarrior_LHS");
                }
                else if (go.name.Contains("Archer"))
                {
                    Winname = go.GetPhotonView().Owner.NickName;
                    SceneManager.LoadScene("EndingSceneArcher_LHS");
                }
                else if (go.name.Contains("Alice"))
                {
                    Winname = go.GetPhotonView().Owner.NickName;
                    SceneManager.LoadScene("EndingSceneAlice_LHS");
                }
            }
        }
    }
    //[PunRPC]
    //void Aland()
    //{
    //    PhotonNetwork.LoadLevel("EndingSceneAland_LHS");
    //}
    //[PunRPC]
    //void Warrior()
    //{
    //    PhotonNetwork.LoadLevel("EndingSceneWarrior_LHS");
    //}
    //[PunRPC]
    //void Archer()
    //{
    //    PhotonNetwork.LoadLevel("EndingSceneArcher_LHS");
    //}
    //[PunRPC]
    //void Alice()
    //{
    //    PhotonNetwork.LoadLevel("EndingSceneAlice_LHS");
    //}
    private void GameLobbySene()
    {
        players = new GameObject[PhotonNetwork.CurrentRoom.MaxPlayers];
        GameObject.Find("Background_Text").GetComponent<Text>().text = RoomName;
        lobbySceneStartTrigger = false;
    }

    void EndingScene()
    {
        GameObject nick = GameObject.Find("WinNickName");
        nick.GetComponent<Text>().text = Winname;
        endingSceneStartTrigger = false;
    }

    void MainScene()
    {
        PhotonNetwork.Instantiate("MainSceneManager", Vector3.zero, Quaternion.identity);
        Camera.main.GetComponent<CameraMove3D_LHS>().StartSetting();
        MainsceneStartTrigger = false;
    }

}
