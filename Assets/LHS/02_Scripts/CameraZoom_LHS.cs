using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom_LHS : MonoBehaviour
{
    public bool ZoomActive;

    public Vector3[] Target;

    public Camera Cam;

    public float Speed;

    public GameObject target1;
    public GameObject target2;

    float distance_target2target;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
    }

    public void LateUpdate()
    {
        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, distance_target2target, Speed);
        //if(ZoomActive)
        //{
        //    Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 5, Speed);
        //    //Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target[1], Speed);
        //}
        //else
        //{
        //    Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 8, Speed);
        //    //Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target[1], Speed);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // 두 타겟 사이의 거리
        distance_target2target = Vector3.Distance(target1.transform.position, target2.transform.position);

        if (distance_target2target < 3)
        {
            distance_target2target = 3;
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
}
