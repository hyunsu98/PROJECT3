using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_HJH : MonoBehaviour
{
    public bool Attack = false;
    public int Damage = 10;
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
        if(Attack == true)
        {
            if (other.GetComponent<PlayerHp_HJH>() != null && other.name != "Player1")
            {
                other.GetComponent<PlayerHp_HJH>().Damage(other.transform.position - transform.position,Damage);
                Attack = false;
            }
        }
    }
}
