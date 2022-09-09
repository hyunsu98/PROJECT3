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
    //점프했는지 체크하게 하기 위해
    bool jumpCheckStart = false;
    Animator am;
    public GameObject dashEffect;
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
        //am.SetInteger("Jump",jumpCount);
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
            if(moveVec.x != 0)
            {
                state = State.Run;
            }
        }
        else if(state == State.Run)
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
            if(moveVec.x == 0)
            {
                state = State.Idle;
            }
        }
        else if(state == State.Jump)
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
            if(jumpCheckStart == true && cc.isGrounded)
            {
                am.SetTrigger("JumpEnd");
                jumpCheckStart = false;
                jumpCount = firstJumpCount;
                state = State.Idle;
            }
        }
        else if(state == State.Dash)
        {

        }
        cc.Move(moveVec * Time.deltaTime);



    }
    void KeyBoardMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveVec.x = x * speed;
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
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Dash();
        }
        
    }

    void JoyStickMove()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        moveVec.x = x * speed;
        if (moveVec.x < 0)
        {
            transform.eulerAngles = new Vector3(0,-90,0);
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

    public void Jump()
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
    public float dashRange = 2;
    public void Dash()
    {
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        StartCoroutine(DashEffect());
    }

    IEnumerator DashEffect()
    {
        cc.Move(new Vector3(dashRange, 0, 0));
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        GameObject sword = null;
        foreach (Transform child in allChildren)
        {
            if(child.gameObject.name == "Sword")
            {
                sword = child.gameObject;
            }
        }
        sword.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        sword.SetActive(true);
    }

    public virtual void Skill1()
    {

    }
}
