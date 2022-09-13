using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_LHS : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    CharacterController cc;


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
            cc.enabled = true;
        }
    }
}
