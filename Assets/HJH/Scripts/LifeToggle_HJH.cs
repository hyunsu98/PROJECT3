using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeToggle_HJH : MonoBehaviour
{
  public void Life2()
    {
        GameManager.instance.startLife = 2;
    }
    public void Life3()
    {
        GameManager.instance.startLife = 3;

    }
    public void Life4()
    {
        GameManager.instance.startLife = 4;
    }
    public void Life5()
    {
        GameManager.instance.startLife = 5;
    }
}
