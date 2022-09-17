using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectionManager_LHS : MonoBehaviour
{
    //닉네임 InputField
    public InputField inputNickName;

    //접속 Button
    public Button btnConnect;

    void Start()
    {
        // 닉네임(InputField)이 변경될때 호출되는 함수 등록
        inputNickName.onValueChanged.AddListener(OnValueChanged);
        // 닉네임(InputField)에서 Enter를 쳤을때 호출되는 함수 등록
        inputNickName.onSubmit.AddListener(OnSubmit);
    }

    void Update()
    {
        
    }

    //접속 버튼 눌렀을때 씬 전환 -> 네트워크 시 변경
    public void LobbySceneChange()
    {
        SceneManager.LoadScene("LobbyScene_LHS");
    }

    // Quit 클릭 시 게임 나가기
    public void Quit()
    {
        Application.Quit();
    }

    public void OnValueChanged(string s)
    {
        //만약에 s의 길이가 0보다 크다면
        //접속 버튼을 활성화 하자         
        //그렇지 않다면
        //접속 버튼을 비활성화 하자
        btnConnect.interactable = s.Length > 0;

        print("OnValueChanged : " + s);
    }

    //*** 네트워크 시 변경
    public void OnSubmit(string s)
    {
        //만약에 s의 길이가 0보다 크다면
        if (s.Length > 0)
        {
            //접속 하자! 
            // OnClickConnect();
        }
        print("OnSubmit : " + s);
    }
}
