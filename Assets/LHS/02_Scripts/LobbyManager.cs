using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //***�� ����(�� �̸�) InputField
    public InputField inputRoomName;
    //***��� ��(��й�ȣ) InputField
    public InputField inputPassword;
    //***��� �� Toggle
    public Toggle PassToggle;
    //***�ο� �� / �� �ο�
    public Toggle numChoice1;
    public int maxPlayer; // �� �ο� (��ȣó��)
    // ���ο�(����ó��)
    public InputField inputMaxPlayer;
    //***�� ���� Button
    public Button btnCreate;

    //���� �˾�â
    public GameObject roomFail;

    //���� ����(��Ī) Button
    public Button btnQuickJoin;
    //�� ����(�� ����) Button
    public Button btnJoin;

    //���� ������   
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    //�� ����Ʈ Content
    public Transform trListContent;

    //map Thumbnail
    public GameObject[] mapThumbs;

    void Start()
    {
        // ���̸�(InputField)�� ����ɶ� ȣ��Ǵ� �Լ� ���
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        // ���̸�(InputField)�� ����ɶ� ȣ��Ǵ� �Լ� ���
        inputMaxPlayer.onValueChanged.AddListener(OnMaxPlayerValueChanged);

        //�ο� �� ������ ����ɶ� ȣ��Ǵ� �Լ� ���
        numChoice1.onValueChanged.AddListener(OnToggleClick1);
        //��� ���� ����� ���� �Լ�
        PassToggle.onValueChanged.AddListener(OnPassToggle);

    }

    public void OnRoomNameValueChanged(string s)
    {
        // ���̸��� ���� �ɶ� üũ
        // ������ -> ���̸��� �� ������ ���� // ->�� ���常 �����ϰ� ���������� (����)
        // btnJoin.interactable = s.Length > 0;
        // ����� -> ���̸��� �� �ο��� �ִٸ� ���� -> ��� ������ �Ѵٸ� (�ΰ� ���� ��)
        btnCreate.interactable = s.Length > 0 && inputMaxPlayer.text.Length > 0;
    }

    public void OnMaxPlayerValueChanged(string s)
    {
        // ����� -> ���̸��� �� �ο��� �ִٸ� ���� -> ��� ������ �Ѵٸ� (�ΰ� ���� ��)
        btnCreate.interactable = s.Length > 0 && inputRoomName.text.Length > 0;
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

    // ��� �� ��� ���� �� �н����� �ۼ��� �� �ְ�
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
            inputPassword.text = "";
        }
    }

    //�� ����
    public void CreateRoom()
    {
        // �� �ɼ��� ����
        RoomOptions roomOptions = new RoomOptions();
        // �ִ� �ο� (0�̸� �ִ��ο�)  = �츮�� 4��
        roomOptions.MaxPlayers = 4; //byte.Parse(inputMaxPlayer.text);
        // �� ����Ʈ�� ������ �ʰ�? ���̰�?
        roomOptions.IsVisible = true;

        //custom ������ ����
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash["desc"] = "���� �ʺ����̴�! " + Random.Range(1, 1000);
        hash["map_id"] = Random.Range(0, mapThumbs.Length);
        hash["room_name"] = inputRoomName.text;
        hash["password"] = inputPassword.text;
        roomOptions.CustomRoomProperties = hash;
        // custom ������ �����ϴ� ����
        roomOptions.CustomRoomPropertiesForLobby = new string[] {
            "desc", "map_id", "room_name", "password"
        };

        // �� ���� ��û (�ش� �ɼ��� �̿��ؼ�)
        PhotonNetwork.CreateRoom(inputRoomName.text + inputPassword.text, roomOptions);
    }

    //���� �����Ǹ� ȣ�� �Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    //�� ������ ���� �ɶ� ȣ�� �Ǵ� �Լ�
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
        // ���� �˾�â
        roomFail.SetActive(true);
    }

    //�� ���� ��û
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputRoomName.text + inputPassword.text);
    }

    //�� ������ �Ϸ� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom");
        PhotonNetwork.LoadLevel("GameLobbyScene_LHS");
    }

    //�� ������ ���� �Ǿ��� �� ȣ�� �Ǵ� �Լ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }

    //�濡 ���� ������ ����Ǹ� ȣ�� �Ǵ� �Լ�(�߰�/����/����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        //�븮��Ʈ UI �� ��ü����
        DeleteRoomListUI();
        //�븮��Ʈ ������ ������Ʈ
        UpdateRoomCache(roomList);
        //�븮��Ʈ UI ��ü ����
        CreateRoomListUI();
    }

    void DeleteRoomListUI()
    {
        foreach (Transform tr in trListContent)
        {
            Destroy(tr.gameObject);
        }
    }

    void UpdateRoomCache(List<RoomInfo> roomList)
    {

        for (int i = 0; i < roomList.Count; i++)
        {
            // ����, ����
            if (roomCache.ContainsKey(roomList[i].Name))
            {
                //���࿡ �ش� ���� �����Ȱ��̶��
                if (roomList[i].RemovedFromList)
                {
                    //roomCache ���� �ش� ������ ����
                    roomCache.Remove(roomList[i].Name);
                }
                //�׷��� �ʴٸ�
                else
                {
                    //���� ����
                    roomCache[roomList[i].Name] = roomList[i];
                }
            }
            //�߰�
            else
            {
                roomCache[roomList[i].Name] = roomList[i];
            }
        }

        //for (int i = 0; i < roomList.Count; i++)
        //{
        //    // ����, ����
        //    if (roomCache.ContainsKey(roomList[i].Name))
        //    {
        //        //���࿡ �ش� ���� �����Ȱ��̶��
        //        if (roomList[i].RemovedFromList)
        //        {
        //            //roomCache ���� �ش� ������ ����
        //            roomCache.Remove(roomList[i].Name);
        //            continue;
        //        }                
        //    }
        //    roomCache[roomList[i].Name] = roomList[i];            
        //}
    }

    public GameObject roomItemFactory;
    void CreateRoomListUI()
    {
        //foreach (RoomInfo info in roomCache.Values)
        //{
        //    //������� �����.
        //    GameObject go = Instantiate(roomItemFactory, trListContent);
        //    //������� ������ ����(������(0/0))
        //    RoomItem item = go.GetComponent<RoomItem>();
        //    item.SetInfo(info);

        //    //roomItem ��ư�� Ŭ���Ǹ� ȣ��Ǵ� �Լ� ���
        //    item.onClickAction = SetRoomName;
        //    //���ٽ�
        //    //item.onClickAction = (string room) => {
        //    //    inputRoomName.text = room;
        //    //};

        //    string desc = (string)info.CustomProperties["desc"];
        //    int map_id = (int)info.CustomProperties["map_id"];
        //    print(desc + ", " + map_id);
        //}
    }


    //���� Thumbnail id
    int prevMapId = -1;
    void SetRoomName(string room, int map_id)
    {
        //���̸� ����
        inputRoomName.text = room;

        //���࿡ ���� �� Thumbnail�� Ȱ��ȭ�� �Ǿ��ִٸ�
        if (prevMapId > -1)
        {
            //���� �� Thumbnail�� ��Ȱ��ȭ
            mapThumbs[prevMapId].SetActive(false);
        }

        //�� Thumbnail ����
        mapThumbs[map_id].SetActive(true);

        //���� �� id ����
        prevMapId = map_id;
    }

    //���� ��ư �������� �� ��ȯ -> ��Ʈ��ũ �� ����
    public void GameLobbySceneChange()
    {
        //SceneManager.LoadScene("GameLobbyScene_LHS");
    }

    void Update()
    {
        
    }
}
