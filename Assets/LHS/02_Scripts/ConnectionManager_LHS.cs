using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectionManager_LHS : MonoBehaviour
{
    //�г��� InputField
    public InputField inputNickName;

    //���� Button
    public Button btnConnect;

    void Start()
    {
        // �г���(InputField)�� ����ɶ� ȣ��Ǵ� �Լ� ���
        inputNickName.onValueChanged.AddListener(OnValueChanged);
        // �г���(InputField)���� Enter�� ������ ȣ��Ǵ� �Լ� ���
        inputNickName.onSubmit.AddListener(OnSubmit);
    }

    void Update()
    {
        
    }

    //���� ��ư �������� �� ��ȯ -> ��Ʈ��ũ �� ����
    public void LobbySceneChange()
    {
        SceneManager.LoadScene("LobbyScene_LHS");
    }

    // Quit Ŭ�� �� ���� ������
    public void Quit()
    {
        Application.Quit();
    }

    public void OnValueChanged(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        //���� ��ư�� Ȱ��ȭ ����         
        //�׷��� �ʴٸ�
        //���� ��ư�� ��Ȱ��ȭ ����
        btnConnect.interactable = s.Length > 0;

        print("OnValueChanged : " + s);
    }

    //*** ��Ʈ��ũ �� ����
    public void OnSubmit(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        if (s.Length > 0)
        {
            //���� ����! 
            // OnClickConnect();
        }
        print("OnSubmit : " + s);
    }
}
