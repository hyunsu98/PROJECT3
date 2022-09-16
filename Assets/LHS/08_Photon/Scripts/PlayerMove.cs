using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


//��, CharacterController�� ���

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    //�ӷ�
    public float moveSpeed = 5;
    //characterController ���� ����
    CharacterController cc;

    //�߷�
    float gravity = -9.81f;
    //�����Ŀ�
    public float jumpPower = 5;
    //y���� �ӷ�
    float yVelocity;

    //�г��� UI
    public Text nickName;

    //���� ��ġ
    Vector3 receivePos;
    //ȸ���Ǿ� �ϴ� ��
    Quaternion receiveRot;
    //���� �ӷ�
    public float lerpSpeed = 100;

    //PlayerState ������Ʈ
    PlayerState playerState;

    void Start()
    {        
        //characterController �� ����
        cc = GetComponent<CharacterController>();
        //����ü���� �ִ�ü������ ����
        currHp = maxHp;
        //�г��� ����
        nickName.text = photonView.Owner.NickName;
        //PlayerState ������Ʈ ��������
        playerState = GetComponent<PlayerState>();
        //GameManager���� ���� PhotonView�� ����
        GameManager.instance.AppPlayer(photonView);
    }

    void Update()
    {
        //���࿡ �����̶��
        if (photonView.IsMine)
        {
            // WSAD�� ������ ��,��,��,��� �̵�
            //1. WSAD�� ��ȣ�� ����.
            float h = Input.GetAxisRaw("Horizontal"); //A : -1, D : 1, ������ ������ : 0
            float v = Input.GetAxisRaw("Vertical");

            //2. ���� ��ȣ�� ������ �����.
            Vector3 dir = transform.forward * v + transform.right * h; // new Vector3(h, 0, v);
                                                                       //������ ũ�⸦ 1���Ѵ�.
            dir.Normalize();

            //���࿡ �ٴڿ� ����ִٸ� yVelocity�� 0���� ����
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            //���࿡ �����̹�(Jump)�� ������
            if (Input.GetButtonDown("Jump"))
            {
                //yVelocity�� jumpPower�� ����
                yVelocity = jumpPower;
            }

            //yVelocity���� �߷����� ���ҽ�Ų��.
            yVelocity += gravity * Time.deltaTime;

            //dir.y�� yVelocity���� ����
            dir.y = yVelocity;

            //3. �� �������� ��������.
            //P = P0 + vt
            cc.Move(dir * moveSpeed * Time.deltaTime);

            //���࿡ �����δٸ�
            if(h != 0 || v != 0)
            {
                //���¸� Move��
                playerState.ChangeState(PlayerState.State.MOVE);
            }
            //�׷��� �ʴٸ�
            else
            {
                //���¸� Idle��
                playerState.ChangeState(PlayerState.State.IDLE);
            }

        }
        //������ �ƴ϶��
        else
        {
            //Lerp�� �̿��ؼ� ������, ����������� �̵� �� ȸ��
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }        
    }


    //���� ü��
    public int maxHp = 10;
    public int currHp;
    //�ǰݵǾ��� �� ȣ��Ǵ� �Լ�
   
    public void OnDamaged()
    {
        photonView.RPC("RpcOnDamaged", RpcTarget.All);
    }

    [PunRPC]
    void RpcOnDamaged()
    {
        //1. ���� ü�� 1 �ٿ��ְ�
        currHp--;
        print("����ü�� : " + currHp);
        //2. ���࿡ ���� ü���� 0���� ���ų� �۾�����
        if (currHp <= 0)
        {
            //3. ���¸� DIE�� �ٲ۴�.
            playerState.RpcChangeState(PlayerState.State.DIE);
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //������ ������
        if(stream.IsWriting) // isMine == true
        {
            //position, rotation
            stream.SendNext(transform.rotation);            
            stream.SendNext(transform.position);
        }
        //������ �ޱ�
        else if(stream.IsReading) // ismMine == false
        {
            receiveRot = (Quaternion)stream.ReceiveNext();
            receivePos = (Vector3)stream.ReceiveNext();
        }
    }
}
