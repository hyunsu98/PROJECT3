using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager_LHS : MonoBehaviour
{
    //빠른 입장 Button
    public Button butQuickJoin;
    //방 입장 Button
    public Button btnJoin;
    //방 만들기
    public Button btnCreate;

    //방 제목 InputField
    public InputField inputRoomName;
    //비밀 방 InputField
    public InputField inputPassword;
    //비밀 방 Toggle
    public Toggle PassToggle;
    // 인원 수
    public Toggle numChoice1;
    //방 생성 Button
    public Button roomCreate;
    //취소 Button
    public Button roomQuit;

    //방의 정보들   
    
    //룸 리스트 Content
    public Transform trListContent;

    void Start()
    {
        // 방이름(InputField)이 변경될때 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        //인원 수 제한이 변경될때 호출되는 함수 등록
        numChoice1.onValueChanged.AddListener(OnToggleClick1);
        //비밀 방을 만들기 위한 함수
        PassToggle.onValueChanged.AddListener(OnPassToggle);

    } 
    public void OnRoomNameValueChanged(string s)
    {
        //생성
        //방제목과 인원 수 선택시 실행될 수 있게 -> 토글 오류
        roomCreate.interactable = s.Length > 0;
    }

    // 인원 수 선택 시 인원에 맞는 방 캐릭터 생성 될 수 있게
    public void OnToggleClick1(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("인원 2");

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
            // 텍스트 삭제
            
        }
    }

    void Update()
    {
        
    }
}
