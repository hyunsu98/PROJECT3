using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int startLife = 3;
    bool sceneStartTrigger = true;
    public GameObject[] players;
    public enum PlayerCharcter
    {
        Aland,
        Alice,
        Warrior,
        Archer,
    }
    public PlayerCharcter playerCharcter;
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
        //mainScene에 들어갔을 때 - 저거 빌드세팅에 따라서 숫자 바꿔줘야됨
        if(SceneManager.GetActiveScene().name == "MainScene_HJH" && sceneStartTrigger == true)
        {
            MainScene();
        }
    }
    void MainScene()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
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

        sceneStartTrigger = false;

    }

    //**** 현숙 추가 접속 버튼 눌렀을때 씬 전환 -> 네트워크 시 변경
    public void MainSceneChange()
    {
        SceneManager.LoadScene("MainScene_HJH");
    }

}
