using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
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
        // �г���(InputField)���� Focusing�� �Ҿ����� ȣ��Ǵ� �Լ� ���
        inputNickName.onEndEdit.AddListener(OnEndEdit);
    }

    public void OnValueChanged(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        //���� ��ư�� Ȱ��ȭ ����         
        //�׷��� �ʴٸ�
        //���� ��ư�� ��Ȱ��ȭ ����
        btnConnect.interactable = s.Length > 0;
    }

    public void OnSubmit(string s)
    {
        //���࿡ s�� ���̰� 0���� ũ�ٸ�
        if (s.Length > 0)
        {
            //���� ����! 
            OnClickConnect();
        } 
    }

    public void OnEndEdit(string s)
    {
    }


    public void OnClickConnect()
    {
        //���� ���� ��û
        PhotonNetwork.ConnectUsingSettings();
    }

    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� ���� ����)
    public override void OnConnected()
    {
        base.OnConnected();
    }

    //������ ���� ���Ӽ����� ȣ��(Lobby�� ������ �� �ִ� ����)
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        //�� �г��� ����
        PhotonNetwork.NickName = inputNickName.text;
        //�κ� ���� ��û
        PhotonNetwork.JoinLobby();
    }

    //�κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        //LobbyScene���� �̵�
        PhotonNetwork.LoadLevel("LobbyScene_LHS");



    }

    // Quit Ŭ�� �� ���� ������
    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {

    }
}
