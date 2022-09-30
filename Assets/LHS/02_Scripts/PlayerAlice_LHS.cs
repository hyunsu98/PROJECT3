using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAlice_LHS : PlayerMove_HJH
{
    public GameObject skillEffect;
    // Start is called before the first frame updatepublic float upDown = 0;
    public AudioClip[] sound;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
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
                    if (jumpCheckStart == true && cc.isGrounded)
                    {
                        am.SetInteger("State", 0);
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
    }

    void JumpCountReturn()
    {
        jumpCount = firstJumpCount;
    }
    public override void Skill1()
    {
        am.SetInteger("State", 5);
    }

    public void Skill()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RpcShowSkillEffect2", RpcTarget.All);
        }
        state = State.Attack;
    }
    public void SkillOver()
    {
        ChangeState(State.Idle);
    }

    public override void StopAttack()
    {
        am.SetInteger("State", 4);
        photonView.RPC("AtSet", RpcTarget.All, true);
        state = State.Attack;
        //audio.clip = audioClips[0];
        audio.Play();

    }
    public void Test()
    {
        ChangeState(State.Idle);
        photonView.RPC("AtSet", RpcTarget.All, false);
    }
    public void AttackOver()
    {
        ChangeState(State.Idle);
        //photonView.RPC("AtSet", RpcTarget.All, false);
        Debug.Log("Hello");
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
        am.SetInteger("State", 3);
        Weapon.GetComponent<Weapon3_LHS>().Attack = true;
    }

    public void JumpAttackOver()
    {
        Weapon.GetComponent<Weapon3_LHS>().Attack = false;
    }

    [PunRPC]
    void AtSet(bool set)
    {
        Weapon.GetComponent<Weapon3_LHS>().Attack = set;
    }

    [PunRPC]
    void RpcShowSkillEffect2()
    {

        //audio.clip = audioClips[1];
        //audio.Play();
        if (photonView.IsMine)
        {
            GameObject skill = PhotonNetwork.Instantiate("Player3SkillEffect",transform.position,Quaternion.Euler(new Vector3(-90,0,0)));
            Destroy(skill, 3f);
            skill.GetComponent<Weapon3_LHS>().Attack = true;
        }
    }
}
