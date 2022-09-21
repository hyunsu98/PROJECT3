using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomItem_LHS : MonoBehaviour
{
    // 내용 (방이름 (0/0)) 변경 스크립트
    public Text roomInfo;

    // 설명
    public Text roomDesc;

    // 비번 설명
    public Text roomLock;

    //클릭이 되었을 때 호출되는 함수를 가지고있는 변수 // 매개변수가 있어야함
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
        //게임오브젝트의 이름을 roomName으로!
        name = roomName;
        // 방이름 (0/0)
        roomInfo.text = roomName + " ( " + currPlayer + " / " + maxPlayer + " ) ";
    }

    public void SetInfo(RoomInfo info)
    {
        SetInfo((string)info.CustomProperties["room_name"], info.PlayerCount, info.MaxPlayers);

        //desc 설정
        roomDesc.text = (string)info.CustomProperties["desc"];

        // 비번이 null이라면 비번잠금 ui이가 뜨게 하고 싶은데 어디에 넣어야하는지 모르겠음
        //if ((string)info.CustomProperties["password"] == null)
        //{
        //    print("비번없음");
        //    GameObject go = GameObject.Find("Lock");
        //    go.SetActive(false);
        //}

    }

    public void OnClick()
    {
        //만약에 onClickAction 가 null이 아니라면
        if (onClickAction != null)
        {
            //onClickAction 실행
            onClickAction(name);
        }

        ////InputRoomName 게임오브젝 찾자
        //GameObject go = GameObject.Find("FindRoomName");
        ////InputField 컴포넌트 가져오자
        //InputField inputField = go.GetComponent<InputField>();
        //// text에 roomName 넣자
        //inputField.text = name;

    }
}
