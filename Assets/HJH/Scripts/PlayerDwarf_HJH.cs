using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDwarf_HJH : PlayerMove_HJH, IPunObservable
{
    public GameObject skillEffect;

    #region [����] ����
    //���� ��ġ
    Vector3 receivePos;
    //ȸ���Ǿ� �ϴ� ��
    Quaternion receiveRot;
    //���� �ӷ�
    public float lerpSpeed = 100;
    #endregion

    // Update is called once per frame
    void Update()
    {
        //[����]
        //���࿡ �����̶�� ������
        if (photonView.IsMine)
        {
            if (Player == true)
            {
                if (state == State.Idle)
                {
                    if (keyboardMode == true)
                    {
                        KeyBoardMove();
                        can.SetActive(false);
                    }
                    else
                    {
                        JoyStickMove();
                        can.SetActive(true);
                    }
                    if (moveVec.x != 0)
                    {
                        ChangeState(State.Run);
                    }
                }
                else if (state == State.Run)
                {
                    if (keyboardMode == true)
                    {
                        KeyBoardMove();
                        can.SetActive(false);
                    }
                    else
                    {
                        JoyStickMove();
                        can.SetActive(true);
                    }
                    if (moveVec.x == 0)
                    {
                        ChangeState(State.Idle);
                    }
                }
                else if (state == State.Jump)
                {
                    if (keyboardMode == true)
                    {
                        KeyBoardMove();
                        can.SetActive(false);
                    }
                    else
                    {
                        JoyStickMove();
                        can.SetActive(true);
                    }
                    if (!cc.isGrounded)
                    {
                        moveVec.y += gravity * Time.deltaTime;
                        jumpCheckStart = true;

                    }
                    else
                    {
                        moveVec.y = 0;
                    }
                    if (jumpCheckStart == true && cc.isGrounded)
                    {
                        am.SetTrigger("JumpEnd");
                        jumpCheckStart = false;
                        Invoke("JumpCountReturn", 1f);
                        state = State.Idle;
                    }
                }
                else if (state == State.Dash)
                {

                }
                else if (state == State.Attack)
                {

                }
                else if (state == State.Attacked)
                {
                    StartCoroutine(Stun(hp.Hp));
                }
                else if (state == State.JumpAttack)
                {
                    //moveVec.y = 0;
                }

            }
            else
            {
                if (state == State.Attacked)
                {
                    GameObject sm = Instantiate(smoke);
                    sm.transform.position = transform.position;
                    StartCoroutine(Stun(hp.Hp));
                }
                if (!cc.isGrounded)
                {
                    moveVec.y += gravity * Time.deltaTime;
                }
            }
            if (transform.position.z != 0)
            {
                cc.Move(new Vector3(0, 0, -transform.position.z));
            }
            else
            {
                moveVec.z = 0;
            }
            cc.Move(moveVec * Time.deltaTime);
        }

        //[����]
        // ������ �ƴ϶��
        else
        {
            //Lerp�� �̿��ؼ� ������, ����������� �̵� �� ȸ��
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }
    }

    public override void Skill1()
    {
        am.SetTrigger("Skill");

    }
    public void SkillOver()
    {
        ChangeState(State.Idle);
    }
    public void SkillEffect()
    {
        //[����]
        photonView.RPC("RpcShowSkillEffect", RpcTarget.All);

        // �ؿ��� Rpc�� ��������ϴ°�?
        audio.clip = audioClips[1];
        audio.Play();
        
        state = State.Attack;
    }
    void JumpCountReturn()
    {
        jumpCount = firstJumpCount;
    }
    public override void Dash()
    {
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        StartCoroutine(DashEffect());
    }
    IEnumerator DashEffect()
    {
        if (transform.rotation.y > 0)
        {
            cc.Move(new Vector3(dashRange, 0, 0));
        }
        else
        {
            cc.Move(new Vector3(-dashRange, 0, 0));
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Weapon.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Weapon.SetActive(true);
    }
    public override void Jump()
    {
        base.Jump();
    }
    public override void JumpAttack()
    {
        am.SetTrigger("JumpAttack");
        Weapon.GetComponent<Weapon2_HJH>().Attack = true;
    }
    public void JumpAttackOver()
    {
        Weapon.GetComponent<Weapon2_HJH>().Attack = false;
    }
    public override void StopAttack()
    {
        am.SetTrigger("Attack");
        Weapon.GetComponent<Weapon2_HJH>().Attack = true;
        state = State.Attack;
        audio.clip = audioClips[0];
        audio.Play();
    }
    public void AttackOver()
    {
        state = State.Idle;
        Weapon.GetComponent<Weapon2_HJH>().Attack = false;
    }

    #region [����] Photon OnPhotonSerializeView ����ȭ
    // 1�ʿ� ��� ������ ��������
    // stream���� value Ÿ�Ը� ���� �� ����
    // ���ӿ�����Ʈ, Transform �ѱ�� X
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //������ ������ //�� PC
        if (stream.IsWriting) // isMine == true
        {
            //position, rotation
            //SendNext�� List�� �����Ǿ� ���� //�ٸ� �����͵� ���� �� ����
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.position);
        }
        //������ �ޱ� //�ٸ� ��� PC���� ȣ���
        else if (stream.IsReading) // ismMine == false
        {
            //���� ������� ����
            //������Ʈ������ �Ǿ��ֱ� ������ ���� ����ȯ �ʼ�
            receiveRot = (Quaternion)stream.ReceiveNext();
            receivePos = (Vector3)stream.ReceiveNext();
        }
    }
    #endregion

    [PunRPC]
    void RpcShowSkillEffect()
    {
        GameObject skill = Instantiate(skillEffect, gameObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Destroy(skill, 1f);
        skill.GetComponent<Weapon2_HJH>().Attack = true;
    }
}
