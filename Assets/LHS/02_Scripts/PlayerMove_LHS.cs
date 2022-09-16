using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_LHS : MonoBehaviour
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
    PlayerHp_HJH hp;

    #region 현숙추가
    //****레이어 충돌 변수
    int playerLayer, groundLayer;
    bool fallGround;

    //****충돌무시(열림)
    void IgnoreLayerTrue()
    {
        Physics.IgnoreLayerCollision(playerLayer, groundLayer, true);
    }
    //****충돌적용(닫힘)
    void IgnoreLayerFalse()
    {
        Physics.IgnoreLayerCollision(playerLayer, groundLayer, false);
    }
    //****착지면에 떨어지는 키를 눌렀을때 0.2초간 레이어 충돌이 무시된 후 다시 적용
    IEnumerator LayerOpenClose()
    {
        fallGround = true;
        IgnoreLayerTrue();
        yield return new WaitForSeconds(0.3f);
        IgnoreLayerFalse();
        fallGround = false;
    }

    //**** Ray변수
    private RaycastHit hit;
    private int layerMask;
    private int layerMask2;
    public float distance = 3;
    #endregion

    public enum State
    {
        Idle,
        Run,
        Jump,
        Dash,
        Attack,
        Attacked,

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
        #region 현숙추가
        //****LayerMask 지정
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");

        layerMask = 1 << 8;
        layerMask2 = 1 << 9;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region 현숙추가
        //****Alpha1 누르면 떨어지기 
        // DownJump()
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("alpha1 누름");
            StartCoroutine(LayerOpenClose());
        }
        #endregion

        if (Player == true)
        {
            if (state == State.Idle)
            {
                am.SetTrigger("Idle");
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
                    state = State.Run;
                }
            }
            else if (state == State.Run)
            {
                am.SetTrigger("Run");
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
                    state = State.Idle;
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
                    Invoke("JumpCountReturn", 3f);
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
        am.SetTrigger("Idle");
        if(cc.isGrounded == true)
        {
            state = State.Jump;
            
        }
        else
        {
            state = State.Idle;
        }
    }
    protected void KeyBoardMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //****
            // 점프중이라면 True
            IgnoreLayerTrue();
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Dash();
        }

        #region 현숙추가
        // 아래로 레이를 쐈을 때 
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hit, 10, layerMask) && !fallGround)
        {
            print(hit.transform.name);
            IgnoreLayerFalse();
        }

        // 벽 점프
        if (Physics.Raycast(this.transform.position + new Vector3(0, 1, 0), this.transform.forward, out hit, 1, layerMask2) && moveVec.x != 0)
        {
            Debug.DrawRay(this.transform.position + new Vector3(0, 1, 0), this.transform.forward, Color.green, 1);
            print(hit.transform.name);
            moveVec = Vector3.zero;

            am.SetBool("WallJump", true);
        }
        #endregion

    }

    protected void JoyStickMove()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
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



    public virtual void Jump()
    {
        state = State.Jump;
        //더블 점프 버그있음 왜그런지는 모르겠음

        if (jumpCount > 0)
        {
            Debug.Log("Jump");
            moveVec.y = jumpPower;
            am.SetTrigger("Jump");
        }
        jumpCount--;
    }
    public void SButton()
    {
        if (state != State.Idle)
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
            Attack1();
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

    public virtual void Attack1()
    {
      
    }

    public virtual void Attack2()
    {

    }
    
}
