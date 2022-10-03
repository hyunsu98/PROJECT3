using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MapChoice_HJH : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        // ������ �ƴ϶�� �� ���� ����!
        if (!PhotonNetwork.IsMasterClient)
        {
            //GameObject MapChoice = GameObject.Find("MapChoice(Clone)");
            //Button[] buttonS = MapChoice.GetComponentsInChildren<Button>();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
        }
    }

    public Button [] buttons;

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���常 ���� �� �ְ�!
    public void map1()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("map11", RpcTarget.All);
            photonView.RPC("mapSet", RpcTarget.Others, 0);
        }
    }

    public void map2()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("map22", RpcTarget.All);
            photonView.RPC("mapSet", RpcTarget.Others, 1);
        }
    }
    [PunRPC]
    void map11()
    {
        GameManager.instance.mapName = "MainScene_Photon";
    }
    [PunRPC]
    void map22()
    {
        GameManager.instance.mapName = "MainScene2_Photon";
    }

    [PunRPC]
    void mapSet(int index)
    {
            buttons[index].interactable = true;
    }
}
