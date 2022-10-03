using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MapChoice_HJH : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void map1()
    {
        photonView.RPC("map11", RpcTarget.All);
    }

    public void map2()
    {
        photonView.RPC("map22", RpcTarget.All);
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
}
