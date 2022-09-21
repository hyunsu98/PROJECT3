using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnCharacterSelect_HJH : MonoBehaviour
{
    bool select = false;
    public GameObject[] ui;
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
        ChangeImage(0);
        select = true;
        GameManager.instance.playerCharcters[GameManager.instance.whoIam] = GameManager.PlayerCharcter.Aland;
    }
    public void AliceSelect()
    {
        GameManager.instance.playerCharcters[GameManager.instance.whoIam] = GameManager.PlayerCharcter.Alice;
        ChangeImage(1);
        select = true;
    }
    public void WarriorSelect()
    {
        GameManager.instance.playerCharcters[GameManager.instance.whoIam] = GameManager.PlayerCharcter.Warrior;
        ChangeImage(2);
        select = true;
    }
    public void ArcherSelect()
    {
        GameManager.instance.playerCharcters[GameManager.instance.whoIam] = GameManager.PlayerCharcter.Archer;
        ChangeImage(3);
        select = true;
    }
    public void AlandOn()
    {
        if(select == false)
        {
            
            ChangeImage(0);
        }
        
    }
    public void AliceOn()
    {
        if(select == false)
        {

            ChangeImage(1);

        }
    }
    public void WarriorOn()
    {
        if (select == false)
        {
            ChangeImage(2);
        }
    }
    public void ArcherOn()
    {
        if (select == false)
        {
            ChangeImage(3);
        }
    }

    void ChangeImage(int what)
    {
        for(int i =0; i < 5; i++)
        {
            ui[0].transform.GetChild(i).gameObject.SetActive(false);
        }
        ui[0].transform.GetChild(what).gameObject.SetActive(true);
    }
}
