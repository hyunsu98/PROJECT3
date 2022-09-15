using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_LHS : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    CharacterController cc;

    public int RespawnCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // RaspawnTrigger�� �浹������ ������ �������� ���ư��� �ʹ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathZone")
        {
            cc.enabled = false;
            transform.position = respawnPoint.position;

            StartCoroutine(PlayerRespawn());


        }
    }

    IEnumerator PlayerRespawn()
    {
        cc.enabled = true;
        if (RespawnCount > 0)
        {
            print("�׾����");
        }
        RespawnCount--;
        yield return new WaitForSeconds(0.3f);
        
    }
}
