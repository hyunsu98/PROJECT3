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
    CameraShaker_HJH cs;
    // 리스폰 지점
    [SerializeField] Transform respawnPoint;

    CharacterController cc;

    // 플레이어 Hp
    PlayerHp_HJH playerHp;

    // 플레이어 몸체 안보이게 하기 위한 변수
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
        if (other.tag == "DeathZone" && photonView.IsMine == true)
        {
            photonView.RPC("RpcShowDie", RpcTarget.All);
        }
    }

    [PunRPC]
    void RpcShowDie()
    {
        // 자식들 안보이게 하기
        playerObj.SetActive(false);
        playerObj2.SetActive(false);
        GameObject die = Instantiate(Resources.Load<GameObject>("DieEffect"), playerObj.transform.position, Quaternion.identity);
        die.GetComponent<Renderer>().sortingOrder = 50;
        die.transform.GetChild(0).transform.position = playerObj.transform.position;
        cs.Shake(1, 0.5f);
        Destroy(die, 1f);
        die.transform.GetChild(1).transform.position = respawnPoint.position;
        // 움직임 금지
        cc.enabled = false;

        // 리스폰 발판 켜지기
        respawnBlock.SetActive(true);

        // 코루틴 발생
        StartCoroutine(PlayerRespawn());
    }

    IEnumerator PlayerRespawn()
    {
        // hp를 다시 0으로 생성
        if (photonView.IsMine == true)
        {
            photonView.RPC("SetHp", RpcTarget.All);
        }

        yield return new WaitForSeconds(0.5f);

        // 리스폰 지점으로 이동
        transform.position = respawnPoint.position;

        // 다시 켜지기
        playerObj.SetActive(true);
        playerObj2.SetActive(true);

        cc.enabled = true;

        // 코루틴 발생
        // 깜빡이고
        // StartCoroutine(ReplayRespawn());
    }

    IEnumerator ReplayRespawn()
    {
        yield return new WaitForSeconds(1f);

        // 움직일 수 있게
    }

    [PunRPC]
    void SetHp()
    {
        playerHp.hp = 0;
        RespawnCount--;
    }
}
