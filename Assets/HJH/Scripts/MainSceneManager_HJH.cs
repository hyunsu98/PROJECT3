using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainSceneManager_HJH : MonoBehaviourPun
{
    int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            playerNum = photonView.ViewID / 1000;
            GameObject[] startPoint = GameObject.FindGameObjectsWithTag("StartPoint");
            GameObject player = PhotonNetwork.Instantiate(GameManager.instance.playerCharcters[playerNum].ToString(), startPoint[playerNum - 1].transform.position, Quaternion.Euler(0, 90, 0));
            GameObject ui1 = PhotonNetwork.Instantiate("CharacterUI", Vector3.zero, Quaternion.identity);
            player.GetComponent<Respawn_LHS>().RespawnCount = GameManager.instance.startLife;
            ui1.GetComponent<Player1UI_HJH>().player = player;
            ui1.GetComponent<Player1UI_HJH>().LifeCount = GameManager.instance.startLife;
            Debug.Log("SummonEnd");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
