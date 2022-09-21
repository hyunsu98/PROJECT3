using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove_LHS : MonoBehaviour
{
    public float speed = 10;

    public float cycle = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ////�¿�� �Դٰ��� �ϰ� �ʹ�
        ////1. ������ �ʿ��ϴ�.
        //float x = Mathf.Sin(Time.time);
        //Vector3 dir = new Vector3(x, 0, 0);
        ////2. �̵��ϰ� �ʹ�.
        //transform.position += dir * speed * Time.deltaTime;

        //�¿�� �Դٰ��� �ϰ� �ʹ�
        //1. ������ �ʿ��ϴ�. (���� / �ֱ�)
        float x = speed * Mathf.Sin(Time.time * cycle);
        Vector3 dir = new Vector3(x, 0, 0);
        //2. �̵��ϰ� �ʹ�.
        transform.position += dir * Time.deltaTime;
    }
}
