using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn2_LHS : MonoBehaviour
{
    GameObject spawnPoint;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("spawnPoint");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            GameObject Clone;
            Clone = Instantiate(Player, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
