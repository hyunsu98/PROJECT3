using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp_HJH : MonoBehaviour
{
    ImpactReceiver_HJH impact;
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

    public void Damage(Vector3 point)
    {
        if(point.x > 0)
        {
            pm.moveVec = Vector3.zero;
            impact.AddImpact(new Vector3(1, 1, 0), ((hp/30) + 1) * 30);
        }
        else
        {
            pm.moveVec = Vector3.zero;
            impact.AddImpact(new Vector3(-1, 1, 0), ((hp / 30) + 1) * 30);
        }
        Hp += 10;


    }
    // Start is called before the first frame update
    void Start()
    {
        impact = GetComponent<ImpactReceiver_HJH>();
        pm = GetComponent<PlayerMove_HJH>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
