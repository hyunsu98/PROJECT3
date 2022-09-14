using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove2_LHS : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //좌우로 왔다갔다 하고 싶다
        //1. 방향이 필요하다.
        float x = Mathf.Sin(Time.time);
        Vector3 dir = new Vector3(-x, 0, 0);
        //2. 이동하고 싶다.
        transform.position += dir * speed * Time.deltaTime;
    }
}
