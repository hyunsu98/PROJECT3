using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MouseOnCharacterSelect_HJH : MonoBehaviourPun
{
    public int whoConnectThis;
    bool select = false;
    public GameObject[] ui;
    public Button[] buttons;
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
        photonView.RPC("ChangeImage", RpcTarget.All, 0);
        select = true;
        photonView.RPC("SelectedButton", RpcTarget.All, 0);
        GameManager.instance.playerCharcters[whoConnectThis-1] = GameManager.PlayerCharcter.Aland;
    }
    public void AliceSelect()
    {
        GameManager.instance.playerCharcters[whoConnectThis-1] = GameManager.PlayerCharcter.Alice;
        photonView.RPC("ChangeImage", RpcTarget.All, 1);
        photonView.RPC("SelectedButton", RpcTarget.All, 1);
        select = true;
    }
    public void WarriorSelect()
    {
        GameManager.instance.playerCharcters[whoConnectThis-1] = GameManager.PlayerCharcter.Warrior;
        photonView.RPC("ChangeImage", RpcTarget.All, 2);
        photonView.RPC("SelectedButton", RpcTarget.All, 2);
        select = true;
    }
    public void ArcherSelect()
    {
        GameManager.instance.playerCharcters[whoConnectThis-1] = GameManager.PlayerCharcter.Archer;
        photonView.RPC("ChangeImage", RpcTarget.All, 3);
        photonView.RPC("SelectedButton", RpcTarget.All, 3);
        select = true;
    }
    public void AlandOn()
    {
        if(select == false)
        {
            photonView.RPC("ChangeImage", RpcTarget.All, 0);
        }
        
    }
    public void AliceOn()
    {
        if(select == false)
        {

            photonView.RPC("ChangeImage", RpcTarget.All, 1);

        }
    }
    public void WarriorOn()
    {
        if (select == false)
        {
            photonView.RPC("ChangeImage", RpcTarget.All, 2);
        }
    }
    public void ArcherOn()
    {
        if (select == false)
        {
            photonView.RPC("ChangeImage", RpcTarget.All, 3);
            //ChangeImage(3);
        }
    }
    [PunRPC]
    void ChangeImage(int what)
    {
        for(int i =0; i < 5; i++)
        {
            ui[whoConnectThis-1].transform.GetChild(i).gameObject.SetActive(false);
        }
        ui[whoConnectThis -1].transform.GetChild(what).gameObject.SetActive(true);
    }

    [PunRPC]
    void SelectedButton(int what)
    {
        buttons[what].interactable = false;
    }
}
