using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem_LHS : MonoBehaviour
{
    // ���� (���̸� (0/0)) ���� ��ũ��Ʈ
    public Text roomInfo;

    //Ŭ���� �Ǿ��� �� ȣ��Ǵ� �Լ��� �������ִ� ���� // �Ű������� �־����
    public System.Action<string> onClickAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, byte maxPlayer)
    {
        //���ӿ�����Ʈ�� �̸��� roomName����!
        name = roomName;
        // ���̸� (0/0)
        roomInfo.text = roomName + " ( " + currPlayer + " / " + maxPlayer + " ) ";
    }

    public void OnClick()
    {
        //���࿡ onClickAction �� null�� �ƴ϶��
        if (onClickAction != null)
        {
            //onClickAction ����
            onClickAction(name);
        }

        ////InputRoomName ���ӿ����� ã��
        //GameObject go = GameObject.Find("FindRoomName");
        ////InputField ������Ʈ ��������
        //InputField inputField = go.GetComponent<InputField>();
        //// text�� roomName ����
        //inputField.text = name;
    }
}
