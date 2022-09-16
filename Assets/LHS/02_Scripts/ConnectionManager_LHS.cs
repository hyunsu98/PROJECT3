using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionManager_LHS : MonoBehaviour
{
    //�г��� InputField
    public InputField inputNickName;

    //���� Button
    public Button btnConnect;

    // Start is called before the first frame update
    void Start()
    {
        // �г���(InputField)�� ����ɶ� ȣ��Ǵ� �Լ� ���
        inputNickName.onValueChanged.AddListener(OnValueChanged);
        // �г���(InputField)���� Enter�� ������ ȣ��Ǵ� �Լ� ���
        inputNickName.onSubmit.AddListener(OnSubmit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LobbySceneChange()
    {
        SceneManager.LoadScene("LobbyScene_LHS");
    }

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

    public void OnSubmit(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        if (s.Length > 0)
        {
            //���� ����!
            LobbySceneChange();
        }
        print("OnSubmit : " + s);
    }
}
