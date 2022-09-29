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
    CameraShaker_HJH cs;
    // ������ ����
    [SerializeField] Transform respawnPoint;

    CharacterController cc;

    // �÷��̾� Hp
    PlayerHp_HJH playerHp;

    // �÷��̾� ��ü �Ⱥ��̰� �ϱ� ���� ����
    GameObject playerObj;
    GameObject playerObj2;

    public GameObject respawnBlock;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cs = GetComponent<CameraShaker_HJH>();
        playerHp = GetComponent<PlayerHp_HJH>();

        //respawnBlock = GameObject.Find("RespawnBlock");
        respawnPoint = GameObject.Find("RespawnPoint").transform;
        respawnBlock = GameObject.Find("RespawnBlock");

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
        GameObject die = Instantiate(Resources.Load<GameObject>("DieEffect"), playerObj.transform.position, Quaternion.identity);
        die.GetComponent<Renderer>().sortingOrder = 50;
        die.transform.GetChild(0).transform.position = playerObj.transform.position;
        cs.Shake(1, 0.5f);
        Destroy(die, 1f);
        die.transform.GetChild(1).transform.position = respawnPoint.position;
        // ������ ����
        cc.enabled = false;

        // ������ ���� ������
        respawnBlock.SetActive(true);

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

        yield return new WaitForSeconds(0.5f);

        // ������ �������� �̵�
        transform.position = respawnPoint.position;

        // �ٽ� ������
        playerObj.SetActive(true);
        playerObj2.SetActive(true);

        cc.enabled = true;

        // �ڷ�ƾ �߻�
        // �����̰�
        // StartCoroutine(ReplayRespawn());
    }

    IEnumerator ReplayRespawn()
    {
        yield return new WaitForSeconds(1f);

        // ������ �� �ְ�
    }

    [PunRPC]
    void SetHp()
    {
        playerHp.hp = 0;
        RespawnCount--;
    }
}
