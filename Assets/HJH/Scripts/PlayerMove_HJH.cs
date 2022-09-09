using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_HJH : MonoBehaviour
{
    public float jumpPower = 5;
    GameObject can;
    public float gravity = -9.8f;
    public VariableJoystick joy;
    public float speed;
    Vector3 moveVec;
    CharacterController cc;
    public int jumpCount = 2;
    int firstJumpCount;
    public bool keyboardMode = false;
    Animator am;
    public enum State
    {
        Idle,
        Run,
        Jump,
        Dash,

    }
    public State state = State.Idle;

    // Start is called before the first frame update
    private void Awake()
    {
        am = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        can = GameObject.Find("Controller Canvas");
        moveVec = Vector3.zero;
        firstJumpCount = jumpCount;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Idle)
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
        }
        else if(state == State.Run)
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
        }
        else if(state == State.Jump)
        {

        }
        else if(state == State.Dash)
        {

        }

        
        
        
    }
    void KeyBoardMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveVec.x = x * speed;
        if(moveVec.x == 0 && moveVec.z == 0)
        {
            state = State.Idle;
            am.SetTrigger("Idle");
        }
        else
        {
            am.SetTrigger("Run");
        }
        if (moveVec.x < 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
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
            Jump();
        }
        cc.Move(moveVec * Time.deltaTime);
        
    }

    void JoyStickMove()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        moveVec.x = x * speed;
        if (moveVec.x == 0 && moveVec.z == 0)
        {
            state = State.Idle;
            am.SetTrigger("Idle");
        }
        else
        {
            am.SetTrigger("Run");
        }
        if (moveVec.x < 0)
        {
            transform.eulerAngles = new Vector3(0,-90,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        Debug.Log(cc.isGrounded);
        if (!cc.isGrounded)
        {
            moveVec.y += gravity * Time.deltaTime;
        }
        cc.Move(moveVec * Time.deltaTime);
    }

    public void Jump()
    {
        //더블 점프 버그있음 왜그런지는 모르겠음
        state = State.Jump;

        jumpCount--;
        if (jumpCount>0)
        {
            moveVec.y = jumpPower;
            am.SetTrigger("Jump");
        }
        if (cc.isGrounded)
        {
            Debug.Log("why");
            am.SetTrigger("JumpEnd");
            jumpCount = firstJumpCount;
            state = State.Idle;
        }
    }

    public virtual void Skill1()
    {

    }
}
