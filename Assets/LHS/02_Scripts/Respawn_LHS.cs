using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// ������ �Ǹ� 
// ���� �Ÿ����� �����̰� ������� ����
// (����)
public class Respawn_LHS : MonoBehaviourPunCallbacks
{
    // ������ ī��Ʈ
    public int RespawnCount = 3;

    // ������ ����
    [SerializeField] Transform respawnPoint;

    CharacterController cc;

    // �÷��̾� Hp
    PlayerHp_HJH playerHp;

    // �÷��̾� ��ü �Ⱥ��̰� �ϱ� ���� ����
    GameObject playerObj;
    GameObject playerObj2;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();

        playerHp = GetComponent<PlayerHp_HJH>();

        respawnPoint = GameObject.Find("RespawnPoint").transform;

        // �ڽ��� gameObject ��������
        playerObj = gameObject.transform.GetChild(0).gameObject;
        playerObj2 = gameObject.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // ������ī��Ʈ�� 0���� �۰ų� ���ٸ� 
        // ������ ������
    }

    // RaspawnTrigger�� �浹������ ������ �������� ���ư��� �ʹ�
    private void OnTriggerEnter(Collider other)
    {
        // �±װ� DeathZone�̶��
        if (other.tag == "DeathZone" && photonView.IsMine == true)
        {
            photonView.RPC("RpcShowDie", RpcTarget.All);
        }
    }

    [PunRPC]
    void RpcShowDie()
    {
        // �ڽĵ� �Ⱥ��̰� �ϱ�
        playerObj.SetActive(false);
        playerObj2.SetActive(false);

        // ������ ����
        cc.enabled = false;

        // �ڷ�ƾ �߻�
        StartCoroutine(PlayerRespawn());
    }

    IEnumerator PlayerRespawn()
    {
        // hp�� �ٽ� 0���� ����
        if (photonView.IsMine == true)
        {
            photonView.RPC("SetHp", RpcTarget.All);
        }

        yield return new WaitForSeconds(1f);

        // �ٽ� ������
        // ������ �������� �̵�

        transform.position = respawnPoint.position;
        cc.enabled = true;
        playerObj.SetActive(true);
        playerObj2.SetActive(true);
    }

    [PunRPC]
    void SetHp()
    {
        playerHp.hp = 0;
        RespawnCount--;
    }
}
