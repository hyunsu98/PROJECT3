using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon4_LHS : MonoBehaviourPun
{
    public bool Attack = false;
    public int Damage = 15;
    bool check = true;
    public float skillSpeed = 2f;
    GameObject usePlayer;
    // Start is called before the first frame update
    void Start()
    {
        usePlayer = GameObject.Find("Archer(Clone)");
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.name.Contains("Arrow") && check == true && photonView.IsMine)
        {
            if (usePlayer.transform.rotation.y > 0)
            {
                StartCoroutine(Right());
                check = false;
            }
            else
            {
                StartCoroutine(Left());
                check = false;
            }
        }

    }
    IEnumerator Right()
    {
        while (true)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            transform.position += Vector3.right * Time.deltaTime * skillSpeed;
            gameObject.GetComponent<Renderer>().sortingOrder = 50;
            yield return null;
        }

    }
    IEnumerator Left()
    {
        while (true)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
            transform.position += Vector3.left * Time.deltaTime * skillSpeed;
            gameObject.GetComponent<Renderer>().sortingOrder = 50;
            yield return null;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (Attack == true)
        {
            if (other.GetComponent<PlayerHp_HJH>() != null && other.GetComponent<PlayerArcher_LHS>() == null)
            {
                other.GetComponent<PlayerHp_HJH>().Damage(other.transform.position - transform.position, Damage);
                Attack = false;
                if (gameObject.name.Contains("Arrow"))
                {
                    Destroy(this.gameObject);
                }
            }
            else if(other.GetComponent<PlayerArcher_LHS>() != null)
            {
                return;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
