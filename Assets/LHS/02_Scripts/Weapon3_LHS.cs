using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3_LHS : MonoBehaviour
{
    public bool Attack = false;
    public int Damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Attack == true)
        {
            if (other.GetComponent<PlayerHp_HJH>() != null && other.GetComponent<PlayerAlice_LHS>() == null) 
            {
                other.GetComponent<PlayerHp_HJH>().Damage(other.transform.position - transform.position, Damage);
                Debug.Log("혹시 니가 실행되니?");
                Attack = false;
            }
        }
    }
}
