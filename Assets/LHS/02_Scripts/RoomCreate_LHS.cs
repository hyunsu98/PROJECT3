using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//방만들기 선택시 방만들기 팝업창 띄우기
public class RoomCreate_LHS : MonoBehaviour
{
    public GameObject popup;

    Animator anim;

    private void Start()
    {
        anim = popup.GetComponent<Animator>();
    }

    public void OnOpenPopUp()
    {
        popup.SetActive(true);
    }    

    public void OnQuitPopUp()
    {
        anim.SetTrigger("close");
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                popup.SetActive(false);
            }
        }
    }
}
