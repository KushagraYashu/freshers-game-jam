using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float curTime;
    public float timeout = 4f;
    public float range = 100f;
    public float damage = 50;

    public ParticleSystem explosionParticleSystem;

    bool exploded = false;

    private void Start()
    {
        curTime = timeout;

        //remove these lines later, for dev purposes only
        //LevelManager.instance.BakeNavMesh();
        //LevelManager.instance.GetZomies();
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime <= 0 && !exploded)
        {
            ExplodeGrenade();
            exploded = true;
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    void ExplodeGrenade()
    {
        foreach (var zombie in LevelManager.instance.zombies)
        {
            var dist = Vector3.Distance(this.gameObject.transform.position, zombie.transform.position);
            Debug.LogError(dist);
            if (dist <= range)
            {
                zombie.GetComponent<EnemyBehaviour>().DecreaseHealth(damage / (dist + 1f), true);
            }
        }
        explosionParticleSystem.Play();
        Destroy(this.gameObject, 2);
    }
}
