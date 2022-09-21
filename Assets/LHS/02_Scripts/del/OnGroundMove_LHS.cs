using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundMove_LHS : MonoBehaviour
{
    CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("´ê¾Ò´Ù");
        if (hit.gameObject.CompareTag("Ground"))
        {
            Debug.Log("´ê¾Ò´Ù");
            this.transform.parent = hit.transform;
            

        }
    }
    //private void OnControllerCollider(Collision collision)
    //{
       

    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //        this.transform.parent = null;
    //}
}
