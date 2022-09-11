using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove3D_LHS : MonoBehaviour
{
    //public Transform target;

    // 따라다닐 타겟1
    public Transform target1;
    public Transform target2;

    // 타겟들의 중심점
    Transform centerTarget;

    // 이동 속도
    public float speed;

    //카메라 제한 영역
    //중심점
    public Vector2 center;
    //크기
    public Vector2 size;

    float height;
    float width;

    // Start is called before the first frame update
    void Start()
    {
        // 시작 할 때 위치 = 타겟 위치로
        //transform.position = target.position;
        //x 회전 각도
        //transform.eulerAngles = new Vector3(6, 0, 0);

        //카메라의 월드공간에서 가로/ 세로의 크기를 구해야함
        //세로 절반 크기
        //height = Camera.main.orthographicSize;
        //월드 가로 = 월드 세로 * 스크린 가로/ 스크린 세로
        //width = height * Screen.width / Screen.height;
    }

    //변수값을 시각적으로 보기 위해 함수 생성
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        centerTarget.position = (target1.position + target2.position) * 0.5f;

        //Lerp를 이용한 카메라 이동(target 따라다니게)
        transform.position = Vector3.Lerp(transform.position, centerTarget.position, Time.deltaTime * speed);
        //z축 
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        // Mathf.Clamp(value,min,max) 
        // value값이 min과 max의 사이면 value를 반환하고
        // value값이 min보다 작으면 min을 max보다 크면 max를 반환
        //float lx = size.x * 0.5f - width;
        //float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        //float ly = size.y * 0.5f - height;
        //float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, lx + center.y);

        //transform.position = new Vector3(clampX, clampY, -10f); 

    }
}
