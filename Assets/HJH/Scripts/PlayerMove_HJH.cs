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
    // Start is called before the first frame update
    private void Awake()
    {
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
        if(keyboardMode == true)
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
    void KeyBoardMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveVec.x = x * speed;
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
        if (!cc.isGrounded)
        {
            moveVec.y += gravity * Time.deltaTime;
        }
        cc.Move(moveVec * Time.deltaTime);
    }

    public void Jump()
    {
        jumpCount--;
        if (jumpCount>0)
        {
            moveVec.y = jumpPower;
        }
        if (cc.isGrounded)
        {
            jumpCount = firstJumpCount;
        }
    }

    public virtual void Skill1()
    {

    }
}
