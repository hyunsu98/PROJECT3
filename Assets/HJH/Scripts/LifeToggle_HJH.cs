using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LifeToggle_HJH : MonoBehaviourPun
{
    public Toggle[] toggles;
  public void Life2()
    {
        photonView.RPC("LifeSet", RpcTarget.All, 2);
        photonView.RPC("ToggleSet", RpcTarget.All, 0);
    }
    public void Life3()
    {
        photonView.RPC("LifeSet", RpcTarget.All, 3);
        photonView.RPC("ToggleSet", RpcTarget.All, 1);
    }
    public void Life4()
    {
        photonView.RPC("LifeSet", RpcTarget.All, 4);
        photonView.RPC("ToggleSet", RpcTarget.All, 2);
    }
    public void Life5()
    {
        photonView.RPC("LifeSet", RpcTarget.All, 5);
        photonView.RPC("ToggleSet", RpcTarget.All, 3);
        //GameManager.instance.startLife = 5;
    }

    [PunRPC]
    void LifeSet(int life)
    {
        GameManager.instance.startLife = life;
    }
    [PunRPC]
    void ToggleSet(int index)
    {
        toggles[index].isOn = true;
    }
}
