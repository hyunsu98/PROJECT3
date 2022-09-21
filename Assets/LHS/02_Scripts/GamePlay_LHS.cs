using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlay_LHS : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.gameStartButton = this.gameObject.GetComponent<Button>();
    }
    //**** 현숙 추가 접속 버튼 눌렀을때 씬 전환 -> 네트워크 시 변경
    public void MainSceneChange()
    {
        SceneManager.LoadScene("MainScene_Photon");
    }

}
