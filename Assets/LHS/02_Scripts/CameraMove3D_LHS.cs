using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove3D_LHS : MonoBehaviour
{
    public List<Transform> targets;

    // 중심점에서 카메라 조절하기위해
    public Vector3 offset;
    public float smoothTime = 0.5f;

    // 줌아웃 크기
    public float minZoom = 40f;
    public float maxZoom = 10f;
    // 확대/축소 제한기
    public float zoomLimiter = 50f;

    // 이동속도
    private Vector3 velocity;
    private Camera cam;

    Vector3 newPosition;


    public void StartSetting()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i <  a.Length; i++)
        {
            targets.Add(a[i].transform);
        }
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        StartSetting();

        // 만약 타겟이 0이라면 아무것도 하지 않는다
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    void Zoom()
    {
        // 최대거리 설정
        //Debug.Log(GetGreatestDistance());
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            // 캡슐화
            bounds.Encapsulate(targets[i].position);
        }
        // 상자의 너비
        return bounds.size.x;
    }

    // 카메라 움직임
    void Move()
    {
        // 카메라 위치 = 모든 목표의 중심으로 설정
        Vector3 centerPoint = GetCenterPoint();

        // 중심점에서 + 내가 위치 조절
        Vector3 newPosition = centerPoint + offset;

        // 내 위치 = 중심점
        //transform.position = newPosition;
        // 부드럽게 움직이기 위해 
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    // 중심점 찾기
    Vector3 GetCenterPoint()
    {
        // 만약 타겟이 한명이라면
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        // 여러개의 대상의 중심점을 구하는 방법
        // BOUNDS 여러개체를 캡슐화하는 데 사용할 수 있는 클래스

        // 첫번째 대상 중심에서 시작, 크기는 0으로
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

}
