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
    PlayerCharcter playerCharcter;
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
        //mainScene¿¡ µé¾î°¬À» ¶§ - Àú°Å ºôµå¼¼ÆÃ¿¡ µû¶ó¼­ ¼ýÀÚ ¹Ù²ãÁà¾ßµÊ
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
            //´Ð³×ÀÓ º¯°æ ÇØÁà¾ßµÊ
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
}
