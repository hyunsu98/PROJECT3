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
    CameraShaker_HJH cs;
    public GameObject effect;
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
        cs.Shake((float)hp / 100,0.5f);
        GameObject ef = Instantiate(effect);
        ef.transform.position = transform.position + new Vector3(0, 1, 0);
        ef.GetComponent<Renderer>().sortingOrder = 50;
        Hp += power;


    }
    // Start is called before the first frame update
    void Start()
    {
        impact = GetComponent<ImpactReceiver_HJH>();
        if (gameObject.name.Contains("1"))
        {
            pm = GetComponent<PlayerWarrior_HJH>();
        }
        else if (gameObject.name.Contains("2"))
        {
            pm = GetComponent<PlayerDwarf_HJH>();
        }
        am = GetComponent<Animator>();
        cs = GetComponent<CameraShaker_HJH>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
