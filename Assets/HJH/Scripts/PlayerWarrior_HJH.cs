using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarrior_HJH : PlayerMove_HJH
{
    public GameObject skillEffect;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
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
                    ChangeState(State.Idle);
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

            }

        }
        else
        {
            if (!cc.isGrounded)
            {
                moveVec.y += gravity * Time.deltaTime;
            }
        }

        cc.Move(moveVec * Time.deltaTime);

    }
    void JumpCountReturn()
    {
        jumpCount = firstJumpCount;
    }
    public override void Skill1()
    {
        am.SetTrigger("Skill");


    }
    public void Skill()
    {
        GameObject skill = Instantiate(skillEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(skill, 1f);
        skill.GetComponent<Weapon_HJH>().Attack = true;
        state = State.Attack;
    }
    public void SkillOver()
    {
        state = State.Idle;
    }
    public override void StopAttack()
    {
        am.SetTrigger("Attack");
        Weapon.GetComponent<Weapon_HJH>().Attack = true;
        state = State.Attack;

    }
    public void AttackOver()
    {
        state = State.Idle;
        Weapon.GetComponent<Weapon_HJH>().Attack = false;
    }
    public override void Dash()
    {
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        StartCoroutine(DashEffect());
    }
    public override void Jump()
    {
        base.Jump();
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
    public override void JumpAttack()
    {
        am.SetTrigger("JumpAttack");
        Weapon.GetComponent<Weapon_HJH>().Attack = true;
    }
}
