using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private int health = 100;
    
    public float range;

    [SerializeField]private Transform playerFeet;

    // Start is called before the first frame update
    void Start()
    {
        playerFeet = GameObject.FindGameObjectWithTag("PlayerFeet").transform;
            
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            this.GetComponent<EnemyFollow>().enabled = false;
            Destroy(this.gameObject, 1f);
        }

        if(Vector3.Distance(this.transform.position, playerFeet.transform.position) <= range && this.GetComponent<EnemyFollow>().enabled)
        {
            this.GetComponent<EnemyFollow>().enabled = false;
            Debug.Log("Load Lost Screen");
        }
    }

    public void DecreaseHealth(int damage)
    {
        health-= damage;
        Debug.Log("Health "+ health);
    }

}
