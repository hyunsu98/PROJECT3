using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //***방 제목(방 이름) InputField
    public InputField inputRoomName;
    // 방 찾는 제목 InputField
    public InputField findRoomName;
    // 방 설명
    public InputField inputRoomDesc;

    //***비밀 방(비밀번호) InputField
    public InputField inputPassword;
    public InputField findPassword;
    //***비밀 방 Toggle
    public Toggle PassToggle;
    //***인원 수 / 총 인원
    public Toggle numChoice1;
    public int maxPlayer; // 총 인원 (진호처럼)
    // 총인원(수업처럼)
    public InputField inputMaxPlayer;
    //***방 생성 Button
    public Button btnCreate;

    //실패 팝업창
    public GameObject roomFail;
    public GameObject joinFail;

    //빠른 입장(매칭) Button
    public Button btnQuickJoin;
    //방 입장(방 참가) Button
    public Button btnJoin;

    //방의 정보들 Dictionary (key value 값을 찾을 수 있음)
    //방 이름 , 방 정보
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    //룸 리스트 Content
    public Transform trListContent;

    //map Thumbnail
    public GameObject[] mapThumbs;

    void Start()
    {
        // 방이름(InputField)이 변경될때 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        // 방이름(InputField)이 변경될때 호출되는 함수 등록
        inputMaxPlayer.onValueChanged.AddListener(OnMaxPlayerValueChanged);
        // 방찾기(InputField)이 변경될때 호출되는 함수 등록
        findRoomName.onValueChanged.AddListener(OnFindRoomNameValueChanged);

        //인원 수 제한이 변경될때 호출되는 함수 등록
        numChoice1.onValueChanged.AddListener(OnToggleClick1);
        //비밀 방을 만들기 위한 함수
        PassToggle.onValueChanged.AddListener(OnPassToggle);

    }

    public void OnRoomNameValueChanged(string s)
    {
        // 방이름이 변경 될때 체크
        // 방참가 -> 방이름만 들어가 있으면 가능 // ->방 입장만 가능하게 만들어줘야함 (변경)
        // btnJoin.interactable = s.Length > 0;
        // 방생성 -> 방이름과 총 인원이 있다면 가능 -> 토글 선택을 한다면 (두개 만족 시)
        btnCreate.interactable = s.Length > 0 && inputMaxPlayer.text.Length > 0 && inputMaxPlayer.text.Length < 2;
    }

    public void OnMaxPlayerValueChanged(string s)
    {
        // 방생성 -> 방이름과 총 인원이 있다면 가능 -> 토글 선택을 한다면 (두개 만족 시)
        btnCreate.interactable = s.Length > 0 && s.Length < 2 && inputRoomName.text.Length > 0;
    }

    public void OnFindRoomNameValueChanged(string s)
    {
        // 방찾기가 변경 될때 방입장 체크
        btnJoin.interactable = s.Length > 0;
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

    // 비밀 방 토글 선택 시 패스워드 작성할 수 있게
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
            inputPassword.text = "";
        }
    }

    //방 생성
    public void CreateRoom()
    {
        // 방 옵션을 설정
        RoomOptions roomOptions = new RoomOptions();
        // 최대 인원 (0이면 최대인원)  = 우리는 4명
        roomOptions.MaxPlayers = byte.Parse(inputMaxPlayer.text);
        // 룸 리스트에 보이지 않게? 보이게?
        roomOptions.IsVisible = true;

        // 추가 셋팅
        //custom 정보를 Dictionary<object, object>
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();

        hash["desc"] = inputRoomDesc.text; //"여긴 초보방이다! " + Random.Range(1, 1000);
        hash["map_id"] = Random.Range(0, mapThumbs.Length);
        hash["room_name"] = inputRoomName.text;
        hash["password"] = inputPassword.text;
        roomOptions.CustomRoomProperties = hash;
        // custom 정보를 공개하는 설정 // 공개하고 싶은 키값들
        roomOptions.CustomRoomPropertiesForLobby = new string[] {
            "desc", "map_id", "room_name", "password"
        };

        // 방 생성 요청 (해당 옵션을 이용해서)
        PhotonNetwork.CreateRoom(inputRoomName.text + inputPassword.text, roomOptions);
    }

    //방이 생성되면 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //방 생성이 실패 될때 호출 되는 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
        // 실패 팝업창
        roomFail.SetActive(true);
    }

    //방 참가 요청
    public void JoinRoom()
    {
        //PhotonNetwork.JoinRoom(inputRoomName.text + inputPassword.text);
        PhotonNetwork.JoinRoom(findRoomName.text + findPassword.text);
    }

    //방 참가가 완료 되었을 때 호출 되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        //PhotonNetwork.LoadLevel("GameLobbyScene_LHS");
        PhotonNetwork.LoadLevel("MainScene_Photon");
    }

    //방 참가가 실패 되었을 때 호출 되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
        // 실패 팝업창
        joinFail.SetActive(true);
    }

    // 방에 대한 정보가 변경되면 호출 되는 함수(추가/삭제/수정)
    // 최초에 들어갈 때만 한번! 그 다음부터는 변경된 것만 들어옴
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        //룸리스트 UI 를 전체삭제 
        DeleteRoomListUI();
        //룸리스트 정보를 업데이트
        UpdateRoomCache(roomList);
        //룸리스트 UI 전체 생성
        CreateRoomListUI();
    }

    void DeleteRoomListUI()
    {
        // 부모의 자식들의 갯수만큼 Transform을 담아줌
        foreach (Transform tr in trListContent)
        {
            Destroy(tr.gameObject);
        }
    }

    // 매개변수 필요
    // 방이 없어지면 true 0 -> roomList
    void UpdateRoomCache(List<RoomInfo> roomList)
    {

        for (int i = 0; i < roomList.Count; i++)
        {
            // 수정, 삭제
            // 만약 키 값이 있다면
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                //만약에 해당 룸이 삭제된것이라면
                if (roomList[i].RemovedFromList)
                {
                    //roomCache 에서 해당 정보를 삭제
                    roomCache.Remove(roomList[i].Name);
                }
                //그렇지 않다면
                else
                {
                    //정보 수정
                    roomCache[roomList[i].Name] = roomList[i];
                }
            }
            //추가
            //방이름.방정보 -> 새로운 정보로 덮어씌우기
            else
            {
                roomCache[roomList[i].Name] = roomList[i];
            }
        }
    }

    public GameObject roomItemFactory;

    // 하나씩 받을 것임
    void CreateRoomListUI()
    {
        foreach (RoomInfo info in roomCache.Values)
        {
            //룸 아이템 만든다. // 부모의 자식으로 들어감
            GameObject go = Instantiate(roomItemFactory, trListContent);
            //룸아이템 정보를 셋팅(방제목(0/0))
            RoomItem_LHS item = go.GetComponent<RoomItem_LHS>();
            item.SetInfo(info);
            //item.SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);

            //roomItem 버튼이 클릭되면 호출되는 함수 등록
            item.onClickAction = SetRoomName;

            //람다식
            //item.onClickAction = (string room) => {
            //    inputRoomName.text = room;
            //};

            string desc = (string)info.CustomProperties["desc"];
            int map_id = (int)info.CustomProperties["map_id"];
            print(desc + ", " + map_id);
        }
    }


    //이전 Thumbnail id
    //int prevMapId = -1;
    void SetRoomName(string room)
    {
        //룸이름 설정
        findRoomName.text = room;

        ////만약에 이전 맵 Thumbnail이 활성화가 되어있다면
        //if (prevMapId > -1)
        //{
        //    //이전 맵 Thumbnail을 비활성화
        //    mapThumbs[prevMapId].SetActive(false);
        //}

        ////맵 Thumbnail 설정
        //mapThumbs[map_id].SetActive(true);

        ////이전 맵 id 저장
        //prevMapId = map_id;
    }

    void Update()
    {
        
    }
}
