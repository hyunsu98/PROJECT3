using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay_LHS : MonoBehaviour
{
    //**** ���� �߰� ���� ��ư �������� �� ��ȯ -> ��Ʈ��ũ �� ����
    public void MainSceneChange()
    {
        SceneManager.LoadScene("MainScene_Photon");
    }

}
