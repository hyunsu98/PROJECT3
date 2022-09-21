using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAnim_LHS : MonoBehaviour
{
    public GameObject popup;

    Animator anim;
    private void Start()
    {
        anim = popup.GetComponent<Animator>();
    }

    public void OnQuitPopUp()
    {
        anim.SetTrigger("close");
    }

    private void Update()
    {
        // �ִϸ��̼��� ������ �˾�â �ݱ�
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("close"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                popup.SetActive(false);
            }
        }
    }
}
