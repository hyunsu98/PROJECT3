using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp_HJH : MonoBehaviour
{
    [SerializeField]
    int hp = 0;
    PlayerMove_HJH pm;
    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            pm.state = PlayerMove_HJH.State.Attacked;
            hp = value;
            Debug.Log(hp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMove_HJH>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
