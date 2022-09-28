using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBlock_LHS : MonoBehaviour
{
    [SerializeField] float fallTime = 0.5f, returnTime = 0.7f;
    Vector3 startPos;
    bool isBack;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 20 * Time.deltaTime);
        }

        if(transform.position.y == startPos.y)
        {
            isBack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isBack)
        {
            Invoke("FallPlatform", fallTime);
        }
    }

    void FallPlatform()
    {
        // rb.isKinematic = false;
        Invoke("BackPlatform", returnTime);
    }

    void BackPlatform()
    {
        // rb.velocity = Vector2.zero;
        // rb.isKinematic = true;
        isBack = true;
    }    
}
