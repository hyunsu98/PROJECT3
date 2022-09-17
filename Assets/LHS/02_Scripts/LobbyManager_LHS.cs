using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager_LHS : MonoBehaviour
{
    //���� ���� Button
    public Button butQuickJoin;
    //�� ���� Button
    public Button btnJoin;
    //�� �����
    public Button btnCreate;

    //�� ���� InputField
    public InputField inputRoomName;
    //��� �� InputField
    public InputField inputPassword;
    //��� �� Toggle
    public Toggle PassToggle;
    // �ο� ��
    public Toggle numChoice1;
    //�� ���� Button
    public Button roomCreate;
    //��� Button
    public Button roomQuit;

    //���� ������   
    
    //�� ����Ʈ Content
    public Transform trListContent;

    void Start()
    {
        // ���̸�(InputField)�� ����ɶ� ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        //�ο� �� ������ ����ɶ� ȣ��Ǵ� �Լ� ���
        numChoice1.onValueChanged.AddListener(OnToggleClick1);
        //��� ���� ����� ���� �Լ�
        PassToggle.onValueChanged.AddListener(OnPassToggle);

    } 
    public void OnRoomNameValueChanged(string s)
    {
        //����
        //������� �ο� �� ���ý� ����� �� �ְ� -> ��� ����
        roomCreate.interactable = s.Length > 0;
    }

    // �ο� �� ���� �� �ο��� �´� �� ĳ���� ���� �� �� �ְ�
    public void OnToggleClick1(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("�ο� 2");

        }
        else
        {
            return; 
        }
    }

    public void OnPassToggle(bool isOn)
    {
        if (isOn)
        {
            inputPassword.interactable = true;
        }
        else
        {
            inputPassword.interactable = false;
            // �ؽ�Ʈ ����
            
        }
    }

    void Update()
    {
        
    }
}
