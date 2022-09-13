using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundMove_LHS : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            this.transform.parent = collision.transform;

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            this.transform.parent = null;
    }
}
