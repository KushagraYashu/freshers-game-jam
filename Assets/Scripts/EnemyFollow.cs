using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public bool hit;

    public Animator zombieAnim;

    [SerializeField]private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FeetFinder()
    {
        target = GameObject.FindGameObjectWithTag("PlayerFeet").transform;
    }

    public void UpdateHit(bool hit)
    {
        Debug.Log(this.hit);
        this.hit = hit;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);
        transform.LookAt(-target.position);
    }
}
