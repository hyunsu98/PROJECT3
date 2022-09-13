using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove2D_LHS : MonoBehaviour
{
    // �÷��̾� �߽����� ã�� ���� ����
    public Transform target1;
    public Transform target2;

    // �̵� �ӵ�
    public float speed;

    //ī�޶� ���� ����
    //�߽���
    public Vector2 center;
    //ũ��
    public Vector2 size;

    float height;
    float width;

    void Start()
    {
        // ���� �� �� ��ġ = Ÿ�� ��ġ��
        //transform.position = target.position;

        //ī�޶� x ȸ�� ����
        transform.eulerAngles = new Vector3(6, 0, 0);

        //ī�޶��� ����������� ����/ ������ ũ�⸦ ���ؾ���
        //���� ���� ũ��
        height = Camera.main.orthographicSize;
        //���� ���� = ���� ���� * ��ũ�� ����/ ��ũ�� ����
        width = height * Screen.width / Screen.height;
    }

    //�������� �������� �ð������� ���� ���� �Լ� ����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        // ������ �������� �ߴµ� ����� ����
        //centerTarget.position = (target1.position + target2.position) * 0.5f;

        // �߽����� �������� (�÷��̾���� �����ġ)
        // Lerp�� �̿��� ī�޶� �̵�(target ����ٴϰ�)
        transform.position = Vector3.Lerp(transform.position,(target1.position + target2.position) * 0.5f + new Vector3(0,3,0), Time.deltaTime * speed);

        // ī�޶� ��������
        // Mathf.Clamp(value,min,max) 
        // value���� min�� max�� ���̸� value�� ��ȯ�ϰ�
        // value���� min���� ������ min�� max���� ũ�� max�� ��ȯ
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, lx + center.y);

        transform.position = new Vector3(clampX, clampY, -10f); 

    }
}
