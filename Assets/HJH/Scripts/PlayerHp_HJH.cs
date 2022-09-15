using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp_HJH : MonoBehaviour
{
    ImpactReceiver_HJH impact;
    [SerializeField]
    public int hp = 0;
    Animator am;
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

    public void Damage(Vector3 point,int power)
    {
        if(point.x > 0)
        {
            pm.moveVec = Vector3.zero;
            impact.AddImpact(new Vector3(1, 1, 0), ((hp/30) + 1) * 50);
        }
        else
        {
            pm.moveVec = Vector3.zero;
            impact.AddImpact(new Vector3(-1, 1, 0), ((hp / 30) + 1) * 50);
        }
        Hp += power;


    }
    // Start is called before the first frame update
    void Start()
    {
        impact = GetComponent<ImpactReceiver_HJH>();
        pm = GetComponent<PlayerMove_HJH>();
        am = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
