using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundMove_LHS1 : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("´ê¾Ò´Ù");

            collision.gameObject.transform.localPosition = transform.parent.transform.position;
            Debug.Log(collision.gameObject.name);
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //        this.transform.parent = null;
    //}
    
}
