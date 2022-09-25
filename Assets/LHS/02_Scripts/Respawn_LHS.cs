using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// 리스폰 되면 
// 일정 거리까지 움직이고 사라지는 발판
// (오류)
public class Respawn_LHS : MonoBehaviourPunCallbacks
{
    // 리스폰 카운트
    public int RespawnCount = 3;

    // 리스폰 지점
    [SerializeField] Transform respawnPoint;

    CharacterController cc;

    // 플레이어 Hp
    PlayerHp_HJH playerHp;

    // 플레이어 몸체 안보이게 하기 위한 변수
    GameObject playerObj;
    GameObject playerObj2;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerHp = GetComponent<PlayerHp_HJH>();
        respawnPoint = GameObject.Find("RespawnPoint").transform;

        // 자식의 gameObject 가져오기
        playerObj = gameObject.transform.GetChild(0).gameObject;
        playerObj2 = gameObject.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // 리스폰카운트가 0보다 작거나 같다면 
        // 게임이 끝난다

    }

    // RaspawnTrigger에 충돌했을때 리스폰 지점으로 돌아가고 싶다
    private void OnTriggerEnter(Collider other)
    {
        // 태그가 DeathZone이라면
        if (other.tag == "DeathZone")
        {
            Debug.Log("?");
            // 자식들 안보이게 하기
            playerObj.SetActive(false);
            playerObj2.SetActive(false);

            // 움직임 금지
            cc.enabled = false;

            // 리스폰 지점으로 이동
            transform.position = respawnPoint.position;

            // 코루틴 발생
            StartCoroutine(PlayerRespawn());
        }
    }

    IEnumerator PlayerRespawn()
    {
        // hp를 다시 0으로 생성
        photonView.RPC("SetHp", RpcTarget.All);
        yield return new WaitForSeconds(1f);
        // 다시 켜지기
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
