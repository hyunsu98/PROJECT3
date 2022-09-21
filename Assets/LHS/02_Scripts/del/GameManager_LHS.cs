using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_LHS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //접속 버튼 눌렀을때 씬 전환 -> 네트워크 시 변경
    public void MainSceneChange()
    {
        SceneManager.LoadScene("MainScene_HJH");
    }
}
