using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager_Test_LHS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 1. OnPhotonSerializeView 호출빈도
        PhotonNetwork.SerializationRate = 60;

        // 2. Rpc(원격 프로시저 호출)호출빈도 //단발성 원할 때 한번
        PhotonNetwork.SendRate = 60;

        //선택한 플레이어 생성
        PhotonNetwork.Instantiate("Player1", new Vector3(-2,2.3f,0), Quaternion.identity);
        //PhotonNetwork.Instantiate("Player2", Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate("Player3", Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate("Player4", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
