using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager_Test_LHS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 1. OnPhotonSerializeView ȣ���
        PhotonNetwork.SerializationRate = 60;

        // 2. Rpc(���� ���ν��� ȣ��)ȣ��� //�ܹ߼� ���� �� �ѹ�
        PhotonNetwork.SendRate = 60;

        //������ �÷��̾� ����
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
