using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFire : MonoBehaviourPun
{
    //����ȿ������
    public GameObject bulletImapctFactory;

    //�Ѿ˰���
    public GameObject bulletFactory;

    //�ѱ�
    public Transform firePos;

    //���� ���� ��� �ִ�?
    public bool isMyTurn;

    void Start()
    {
        
    }

    void Update()
    {
        //������ �ƴ϶�� �Լ��� ������.
        if (photonView.IsMine == false) return;
        //������ �ƴ϶�� �Լ��� ������
        if (isMyTurn == false) return;

        //1. ���� ��Ʈ��Ű ������ 
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            FireRay();
            photonView.RPC("RequestChangeTurn", RpcTarget.MasterClient);
        }

        // 1. ���� ��ƮŰ�� ������
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            //// 2. �Ѿ˰��� �Ѿ� �����.
            //GameObject bullet = Instantiate(bulletFactory);
            //// 3. �Ѿ��� �ѱ��� ��ġ ��Ų��.
            //bullet.transform.position = firePos.position;
            //// 4. �Ѿ��� �չ����� �ѱ��������� �Ѵ�.
            //bullet.transform.forward = firePos.forward;
            PhotonNetwork.Instantiate("Bullet", firePos.position, firePos.rotation);
            photonView.RPC("RequestChangeTurn", RpcTarget.MasterClient);
        }
    }

    void FireRay()
    {
        //2. ī�޶� �߽�, ī�޶� �չ������� ������ Ray�� ����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //3. ������ Ray�� �߻��ؼ� ��򰡿� �ε����ٸ�
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //���� �𿡰� PlayerMove ������Ʈ�� �����´�.
            PlayerMove pm = hit.transform.GetComponent<PlayerMove>();
            //���࿡ ������ ������Ʈ�� null�� �ƴ϶��
            if(pm != null)
            {
                //OnDamaged ����
                pm.OnDamaged();
            }

            // ������ RpcShowBulletImpact �Լ� ���� ��û
            photonView.RPC("RpcShowBulletImpact", RpcTarget.All, hit.point, hit.normal);

            //���� �ִϸ��̼�
            photonView.RPC("RpcSetTrigger", RpcTarget.All, "Attack");
        }
    }

    [PunRPC]
    void RpcShowBulletImpact(Vector3 point, Vector3 normal)
    {
        //4. bulletImpact ȿ���� �����.
        GameObject bulletImapct = Instantiate(bulletImapctFactory);
        //5. ����ȿ���� �ε��� ��ġ�� ���´�.
        bulletImapct.transform.position = point;
        //6. ����ȿ���� �չ����� normal�������� �Ѵ�.
        bulletImapct.transform.forward = normal;
        //7. 2�ʵڿ� �ı��Ѵ�.
        Destroy(bulletImapct, 2);

        GameManager.instance.ChangeTurn();
    }

    [PunRPC]
    void SetMyTurn(bool myTurn)
    {
        isMyTurn = myTurn;
    }

    [PunRPC]
    void RequestChangeTurn()
    {
        GameManager.instance.ChangeTurn();
    }
}

