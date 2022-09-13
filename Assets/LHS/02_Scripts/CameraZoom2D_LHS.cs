using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom2D_LHS : MonoBehaviour
{
    // Test용
    //public bool ZoomActive;

    public Camera Cam;

    // Lerp 속도
    public float Speed;

    // 플레이어들
    public GameObject target1;
    public GameObject target2;

    //타겟의 거리
    float distance_target;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
    }

    public void LateUpdate()
    {
        // 줌 아웃
        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, distance_target, Speed);

        //if(ZoomActive)
        //{
        //    Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 5, Speed);
        //}
        //else
        //{
        //    Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 8, Speed);
        //}

        // 두 타겟 사이의 거리
        distance_target = Vector3.Distance(target1.transform.position, target2.transform.position);

        if (distance_target < 4)
        {
            distance_target = 4;
        }

        else if (distance_target > 5)
        {
            distance_target = 5;
        }

        // 두 타겟의 거리가  5가 넘으면 ZoomActive 가 켜지게
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
