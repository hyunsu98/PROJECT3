using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleGroup_LHS : MonoBehaviour
{
    ToggleGroup toggleGroupInstance;

    public Toggle currentSelection
    {
        // ��� �׷쿡�� ���õ� ����� �������� ���ش�
        get { return toggleGroupInstance.ActiveToggles().FirstOrDefault(); }
    }

    void Start()
    {
        toggleGroupInstance = GetComponent<ToggleGroup>();
        Debug.Log("FirestSelected" + currentSelection.name);
    }

    public void SelectToggle(int id)
    {
        var toggles = toggleGroupInstance.GetComponentInChildren<Toggle>();
        //toggles[id].isOn = true;
    }
    void Update()
    {
        
    }
}
