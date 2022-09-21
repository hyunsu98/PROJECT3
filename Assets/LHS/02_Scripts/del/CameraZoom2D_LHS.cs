using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom2D_LHS : MonoBehaviour
{
    // Test��
    //public bool ZoomActive;

    public Camera Cam;

    // Lerp �ӵ�
    public float Speed;

    // �÷��̾��
    public GameObject target1;
    public GameObject target2;

    //Ÿ���� �Ÿ�
    float distance_target;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
    }

    public void LateUpdate()
    {
        // �� �ƿ�
        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, distance_target, Speed);

        //if(ZoomActive)
        //{
        //    Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 5, Speed);
        //}
        //else
        //{
        //    Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 8, Speed);
        //}

        // �� Ÿ�� ������ �Ÿ�
        distance_target = Vector3.Distance(target1.transform.position, target2.transform.position);

        if (distance_target < 4)
        {
            distance_target = 4;
        }

        else if (distance_target > 5)
        {
            distance_target = 5;
        }

        // �� Ÿ���� �Ÿ���  5�� ������ ZoomActive �� ������
        //if (Vector3.Distance(target1.transform.position, target2.transform.position) < 5)
        //{
        //    ZoomActive = true;
        //}
        //else
        //{ 
        //    ZoomActive = false;
        //}
    }

    void Update()
    {

    }
}
