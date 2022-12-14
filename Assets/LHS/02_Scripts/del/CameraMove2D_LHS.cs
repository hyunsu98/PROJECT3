using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove2D_LHS : MonoBehaviour
{
    // 플레이어 중심점을 찾기 위한 변수
    public Transform target1;
    public Transform target2;

    // 이동 속도
    public float speed;

    //카메라 제한 영역
    //중심점
    public Vector2 center;
    //크기
    public Vector2 size;

    float height;
    float width;

    void Start()
    {
        // 시작 할 때 위치 = 타겟 위치로
        //transform.position = target.position;

        //카메라 x 회전 각도
        transform.eulerAngles = new Vector3(6, 0, 0);

        //카메라의 월드공간에서 가로/ 세로의 크기를 구해야함
        //세로 절반 크기
        height = Camera.main.orthographicSize;
        //월드 가로 = 월드 세로 * 스크린 가로/ 스크린 세로
        width = height * Screen.width / Screen.height;
    }

    //영역제한 변수값을 시각적으로 보기 위해 함수 생성
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        // 변수에 담으려고 했는데 담기지 않음
        //centerTarget.position = (target1.position + target2.position) * 0.5f;

        // 중심점을 기준으로 (플레이어들의 평균위치)
        // Lerp를 이용한 카메라 이동(target 따라다니게)
        transform.position = Vector3.Lerp(transform.position,(target1.position + target2.position) * 0.5f + new Vector3(0,3,0), Time.deltaTime * speed);

        // 카메라 영역제한
        // Mathf.Clamp(value,min,max) 
        // value값이 min과 max의 사이면 value를 반환하고
        // value값이 min보다 작으면 min을 max보다 크면 max를 반환
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, lx + center.y);

        transform.position = new Vector3(clampX, clampY, -10f); 

    }
}
