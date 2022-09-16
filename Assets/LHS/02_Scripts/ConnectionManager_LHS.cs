using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager_LHS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LobbySceneChange()
    {
        SceneManager.LoadScene("LobbyScene_LHS");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
