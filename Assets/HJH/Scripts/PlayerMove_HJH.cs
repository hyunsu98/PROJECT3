using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_HJH : MonoBehaviour
{
    public float jumpPower = 5;
    protected GameObject can;
    public float gravity = -9.8f;
    protected VariableJoystick joy;
    public float speed;
    public Vector3 moveVec;
    protected CharacterController cc;
    public int jumpCount = 2;
    protected int firstJumpCount;
    public bool keyboardMode = false;
    //점프했는지 체크하게 하기 위해
    protected bool jumpCheckStart = false;
    protected Animator am;
    public GameObject dashEffect;
    public bool Player = false;
    public float upDown = 0;
    PlayerHp_HJH hp;
    public enum State
    {
        Idle,
        Run,
        Jump,
        Dash,
        Attack,
        Attacked,
        JumpAttack,
    }
    public State state = State.Idle;
    protected GameObject Weapon = null;
        
    // Start is called before the first frame update
    private void Awake()
    {
        hp = GetComponent<PlayerHp_HJH>();
        joy = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.name == weapon)
            {
                Weapon = child.gameObject;
            }
        }
        am = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        can = GameObject.Find("Controller Canvas");
        moveVec = Vector3.zero;
        //am.SetInteger("Jump",jumpCount);
        firstJumpCount = jumpCount;
    }
    void Start()
    {
        
    }
    public void ChangeState(State s)
    {
        if (state == s) return;
        state = s;
        switch (s)
        {
            case State.Idle:
                am.SetTrigger("Idle");
                break;
            case State.Run:
                am.SetTrigger("Run");
                break;

        }

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
                    moveVec.y = 0;
                    am.SetTrigger("JumpEnd");
                    jumpCheckStart = false;
                    Invoke("JumpCountReturn", 3f);
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
                StartCoroutine(Stun(hp.Hp));
            }

        }
        else
        {
            if(state == State.Attacked)
            {
                StartCoroutine(Stun(hp.Hp));
            }
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

    IEnumerator Stun(int stunTime)
    {
        am.SetTrigger("Damage");
        yield return new WaitForSeconds(stunTime / 150);
        if(cc.isGrounded == true)
        {
            state = State.Jump;
        }
        else
        {
            ChangeState(State.Idle);
        }
    }
    protected void KeyBoardMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        upDown = z;
        moveVec.x = x * speed;
        if (moveVec.x < 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else if(moveVec.x == 0)
        {
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (!cc.isGrounded)
        {
            moveVec.y += gravity * Time.deltaTime;
           
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            JButton();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            SButton();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            AButton();
        }
        
    }

    protected void JoyStickMove()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        upDown = z;
        moveVec.x = x * speed;
        if (moveVec.x < 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else if (moveVec.x == 0)
        {
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        if (!cc.isGrounded)
        {
            moveVec.y += gravity * Time.deltaTime;
        }
        
    }

    public void JButton()
    {
        if(upDown < 0)
        {
            DownJump();
        }
        else
        {
            Jump();
        }

    }
    public virtual void DownJump()
    {

    }


    public virtual void Jump()
    {
        ChangeState(State.Jump);
        //더블 점프 버그있음 왜그런지는 모르겠음

        if (jumpCount > 0)
        {
            moveVec.y = jumpPower;
            am.SetTrigger("Jump");
        }
        jumpCount--;
    }
    public void SButton()
    {
        if(state != State.Idle)
        {
            Dash();
        }
        else
        {
            Skill1();
        }
    }

    public void AButton()
    {
        if (state == State.Idle)
        {
            StopAttack();
        }
        else if (state == State.Run)
        {
            MoveAttack();
        }
        else if (state == State.Jump)
        {
            JumpAttack();
        }
    }
    
    public float dashRange = 2;
    public virtual void Dash()
    {

    }
    public string weapon;


    public virtual void Skill1()
    {

    }

    public virtual void StopAttack()
    {
      
    }

    public virtual void MoveAttack()
    {

    }

    public virtual void JumpAttack()
    {

    }
    
}
