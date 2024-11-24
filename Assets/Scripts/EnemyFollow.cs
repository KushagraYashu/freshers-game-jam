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
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        FeetFinder();
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
        Vector3 movementDirection = GetComponent<NavMeshAgent>().velocity;
        if (movementDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void StopMotion()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }
}
