using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int roomMember = 4;
    public static GameManager instance;
    public int startLife = 3; //모든 게임메니저에 보내야됨
    public GameObject[] players;
    public PlayerCharcter[] playerCharcters;
    public int whoIam = 0 ;
    public GameObject[] characterPrefabs;

    bool MainsceneStartTrigger = true; // 안보내도됨
    bool lobbySceneStartTrigger = true;


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
            LobbySene();
        }
    }

    private void LobbySene()
    {
        playerCharcters = new PlayerCharcter[roomMember];
        players = new GameObject[roomMember];
        lobbySceneStartTrigger = false;
    }

    void MainScene()
    {
        GameObject[] startPoint = GameObject.FindGameObjectsWithTag("StartPoint");
        for(int i =0; i < players.Length; i++)
        {
            switch (i)
            {
                case 0:
                    players[i] = Instantiate(characterPrefabs[(int)playerCharcters[i]], startPoint[i].transform.position, Quaternion.Euler(0, 90, 0));
                    break;
            }
        }


        GameObject[] ui = GameObject.FindGameObjectsWithTag("CharacterUi");
        for(int i =0; i<players.Length; i++)
        {
            players[i].GetComponent<Respawn_LHS>().RespawnCount = startLife;
            ui[i].GetComponent<Player1UI_HJH>().player = players[i];
            ui[i].GetComponent<Player1UI_HJH>().LifeCount = startLife;
            //닉네임 변경 해줘야됨
        }

        for(int i = 0; i < ui.Length; i++)
        {
            if(ui[i].GetComponent<Player1UI_HJH>().player == null)
            {
                Destroy(ui[i]);
            }
        }
        Camera.main.GetComponent<CameraMove3D_LHS>().StartSetting();
        MainsceneStartTrigger = false;

    }

}
