using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerArcher_LHS : PlayerMove_HJH
{
    public GameObject skillEffect;
    // Start is called before the first frame updatepublic float upDown = 0;
    public AudioClip[] sound;
    public GameObject arrow;

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
    }

    void JumpCountReturn()
    {
        jumpCount = firstJumpCount;
    }
    public override void Skill1()
    {
        am.SetTrigger("Skill");
    }
    public void SkillOver()
    {
        Debug.Log("hi");
        state = State.Idle;
    }
    public void AttackOverr()
    {
        Debug.Log("eg");
        state = State.Idle;
    }

    public override void StopAttack()
    {
        am.SetTrigger("Attack");
        state = State.Attack;
        //audio.clip = audioClips[0];
        //audio.Play();
    }
    public void ShootArrow()
    {
        photonView.RPC("StopAttackShoot", RpcTarget.All);
    }

    [PunRPC]
    void StopAttackShoot()
    {
        if (photonView.IsMine)
        {
            GameObject arrow = PhotonNetwork.Instantiate("Arrow", transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
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
        //am.SetTrigger("JumpAttack");
        //Weapon.GetComponent<Weapon4_LHS>().Attack = true;
    }

    public void JumpAttackOver()
    {
        Weapon.GetComponent<Weapon4_LHS>().Attack = false;
    }


}
