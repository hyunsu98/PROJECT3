using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ������ �Ǹ� 
// ���� �Ÿ����� �����̰� ������� ����
// (����)
public class Respawn_LHS : MonoBehaviour
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

        // �ڽ��� gameObject ��������
        playerObj = gameObject.transform.GetChild(0).gameObject;
        playerObj2 = gameObject.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // ������ī��Ʈ�� 0���� �۰ų� ���ٸ� 
        // ������ ������
        if (RespawnCount <= 0)
        {
            print("�׾����");
        }
    }

    // RaspawnTrigger�� �浹������ ������ �������� ���ư��� �ʹ�
    private void OnTriggerEnter(Collider other)
    {
        // �±װ� DeathZone�̶��
        if (other.tag == "DeathZone")
        {
            // �ڽĵ� �Ⱥ��̰� �ϱ�
            playerObj.SetActive(false);
            playerObj2.SetActive(false);

            // ������ ����
            cc.enabled = false;

            // ������ �������� �̵�
            transform.position = respawnPoint.position;

            // �ڷ�ƾ �߻�
            StartCoroutine(PlayerRespawn());
        }
    }

    IEnumerator PlayerRespawn()
    {
        // hp�� �ٽ� 0���� ����
        playerHp.hp = 0;
        RespawnCount--;

        yield return new WaitForSeconds(1f);

        // �ٽ� ������
        cc.enabled = true;
        playerObj.SetActive(true);
        playerObj2.SetActive(true);

    }
}
