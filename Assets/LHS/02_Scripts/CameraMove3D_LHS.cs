using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove3D_LHS : MonoBehaviour
{
    public List<Transform> targets;

    // �߽������� ī�޶� �����ϱ�����
    public Vector3 offset;
    public float smoothTime = 0.5f;

    // �ܾƿ� ũ��
    public float minZoom = 40f;
    public float maxZoom = 10f;
    // Ȯ��/��� ���ѱ�
    public float zoomLimiter = 50f;

    // �̵��ӵ�
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

        // ���� Ÿ���� 0�̶�� �ƹ��͵� ���� �ʴ´�
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    void Zoom()
    {
        // �ִ�Ÿ� ����
        //Debug.Log(GetGreatestDistance());
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            // ĸ��ȭ
            bounds.Encapsulate(targets[i].position);
        }
        // ������ �ʺ�
        return bounds.size.x;
    }

    // ī�޶� ������
    void Move()
    {
        // ī�޶� ��ġ = ��� ��ǥ�� �߽����� ����
        Vector3 centerPoint = GetCenterPoint();

        // �߽������� + ���� ��ġ ����
        Vector3 newPosition = centerPoint + offset;

        // �� ��ġ = �߽���
        //transform.position = newPosition;
        // �ε巴�� �����̱� ���� 
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    // �߽��� ã��
    Vector3 GetCenterPoint()
    {
        // ���� Ÿ���� �Ѹ��̶��
        if(targets.Count == 1)
        {
            return targets[0].position;
        }

        // �������� ����� �߽����� ���ϴ� ���
        // BOUNDS ������ü�� ĸ��ȭ�ϴ� �� ����� �� �ִ� Ŭ����

        // ù��° ��� �߽ɿ��� ����, ũ��� 0����
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

}
