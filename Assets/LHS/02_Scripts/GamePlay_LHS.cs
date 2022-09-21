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
    //**** ���� �߰� ���� ��ư �������� �� ��ȯ -> ��Ʈ��ũ �� ����
    public void MainSceneChange()
    {
        SceneManager.LoadScene("MainScene_Photon");
    }

}
