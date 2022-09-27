using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3_LHS : MonoBehaviour
{
    public bool Attack = false;
    // 체력이 늘어나는 정도
    public int Damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("SkillEffect"))
        {
            StartCoroutine(UpDamage(Damage));
        }
    }
    IEnumerator UpDamage(int StartDamage)
    {
        gameObject.GetComponent<Renderer>().sortingOrder = 50;
        for (int i =0; i< 3; i++)
        {
            Damage += 20;
            yield return new WaitForSeconds(1f);
        }
        Damage = StartDamage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Attack == true)
        {
            if (other.GetComponent<PlayerHp_HJH>() != null && other.GetComponent<PlayerAlice_LHS>() == null) 
            {
                other.GetComponent<PlayerHp_HJH>().Damage(other.transform.position - transform.position, Damage);
                Attack = false;
            }
        }
    }
}
