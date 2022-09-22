using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindYourPos_HJH : MonoBehaviour
{
    public string x;
    // Start is called before the first frame update
    void Start()
    {
        if (x == "ToggleGroup")
        {
            transform.parent = GameObject.Find("M_NumChoice").transform;
            GetComponent<RectTransform>().anchoredPosition = new Vector3(19, -1, 0);
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        else if (x == "MapChoice")
        {
            transform.parent = GameObject.Find("Canvas").transform;
            GetComponent<RectTransform>().anchoredPosition = new Vector3(-40.9f, 103f, 0);
            GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else if(x == "PlayerChoice")
        {
            transform.parent = GameObject.Find("Canvas").transform;
            GetComponent<RectTransform>().anchoredPosition = new Vector3(54f, -3f, 0);
            GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else if(x == "Character")
        {
            transform.parent = GameObject.Find("Uis").transform;
            GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
