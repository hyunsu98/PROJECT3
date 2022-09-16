using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerState : MonoBehaviourPun
{
    //�÷��̾� ���� ����
    public enum State
    {
        IDLE,
        MOVE,
        DIE
    }

    //���� ����
    public State currState;
    //Animator
    public Animator anim;

    //DIE �����϶� ��Ȱ��ȭ �Ǿ���ϴ� ���ӿ�����Ʈ��
    public GameObject[] disableGo;
    //DIE �����϶� ��Ȱ��ȭ �Ǿ���ϴ� ������Ʈ��
    public MonoBehaviour [] disableCom;


    //���� ����
    public void ChangeState(State s)
    {
        photonView.RPC("RpcChangeState", RpcTarget.All, s);
    }

    [PunRPC]
    public void RpcChangeState(State s)
    {
        //���� ���°� s�� ���ٸ� �Լ��� ������.
        if (currState == s) return;

        //���� ���¸� s�� ����
        currState = s;

        //s�� ���� animation �÷���
        switch (s)
        {
            case State.IDLE:
                anim.SetTrigger("Idle");
                break;
            case State.MOVE:
                anim.SetTrigger("Move");
                break;
            case State.DIE:
                //���ӿ�����Ʈ�� ��Ȱ��ȭ
                for (int i = 0; i < disableGo.Length; i++)
                    disableGo[i].SetActive(false);
                //������Ʈ�� ��Ȱ��ȭ
                for (int i = 0; i < disableCom.Length; i++)
                    disableCom[i].enabled = false;                
                break;
        }
    }

    [PunRPC]
    public void RpcSetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}
