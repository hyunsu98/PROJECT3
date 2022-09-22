using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MouseOnCharacterSelect_HJH : MonoBehaviourPun
{
    // 누가 바꾸고 있는가를 알려주는 변수
    public int whoConnectThis;

    bool select = false;
    public List<GameObject> ui = new List<GameObject>();
    public Button[] buttons;
    bool change = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AlondSelect()
    {
        photonView.RPC("ChangeImage", RpcTarget.All, 0, whoConnectThis);
        select = true;
        photonView.RPC("SelectedButton", RpcTarget.All, 0);
        GameManager.instance.playerCharcters[whoConnectThis] = GameManager.PlayerCharcter.Aland;
    }
    public void AliceSelect()
    {
        GameManager.instance.playerCharcters[whoConnectThis] = GameManager.PlayerCharcter.Alice;
        photonView.RPC("ChangeImage", RpcTarget.All, 1, whoConnectThis);
        photonView.RPC("SelectedButton", RpcTarget.All, 1);
        select = true;
    }
    public void WarriorSelect()
    {
        GameManager.instance.playerCharcters[whoConnectThis] = GameManager.PlayerCharcter.Warrior;
        photonView.RPC("ChangeImage", RpcTarget.All, 2, whoConnectThis);
        photonView.RPC("SelectedButton", RpcTarget.All, 2);
        select = true;
    }
    public void ArcherSelect()
    {
        GameManager.instance.playerCharcters[whoConnectThis] = GameManager.PlayerCharcter.Archer;
        photonView.RPC("ChangeImage", RpcTarget.All, 3, whoConnectThis);
        photonView.RPC("SelectedButton", RpcTarget.All, 3);
        select = true;
    }
    public void AlandOn()
    {
        if (select == false)
        {
            photonView.RPC("ChangeImage", RpcTarget.All, 0, whoConnectThis);
        }
        
    }
    public void AliceOn()
    {

        if (select == false)
        {

            photonView.RPC("ChangeImage", RpcTarget.All, 1, whoConnectThis);

        }
    }
    public void WarriorOn()
    {
        if (select == false)
        {
            photonView.RPC("ChangeImage", RpcTarget.All, 2, whoConnectThis);
        }
    }
    public void ArcherOn()
    {
        if (select == false)
        {
            photonView.RPC("ChangeImage", RpcTarget.All, 3,whoConnectThis);
            //ChangeImage(3);
        }
    }
    void Change()
    {
        change = true;
        ui[0].transform.SetAsLastSibling();
    }

    // 알아서 생성되게 -> UI안에 들어가게
    [PunRPC]
    void ChangeImage(int what,int who)
    {

            for (int i = 0; i < 5; i++)
            {
                ui[who].transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
            ui[who].transform.GetChild(0).GetChild(what).gameObject.SetActive(true);

        
    }

    [PunRPC]
    void SelectedButton(int what)
    {
        buttons[what].interactable = false;
        GameLobbyManager_HJH.instance.selectPeople += 1;
    }
}
