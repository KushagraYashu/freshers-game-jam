using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public bool atMid = false;
    public float midRange;

    public float speed;
    [SerializeField]private Transform target;
    [SerializeField] private Transform intermediatePoint;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerFeet").transform;
        intermediatePoint = GameObject.FindGameObjectWithTag("mid").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, intermediatePoint.transform.position) < midRange && !atMid)
        {
            atMid = true;
        }

        if (atMid)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, intermediatePoint.position, speed * Time.deltaTime);
        }
    }
}
