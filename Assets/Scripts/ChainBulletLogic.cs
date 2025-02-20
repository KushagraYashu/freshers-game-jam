using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBulletLogic : MonoBehaviour
{
    [Tooltip("damage will be: 10 X DamageMultiplier\nDamage further depends on total hit (if hit-based chain bullet is implemented) otherwise its constant (time-based implementation of chain bullet)")]
    [SerializeField] float damageMultiplier = 1;

    [Tooltip("Speed would be: 20 X SpeedMultiplier")]
    [SerializeField] float speedMultiplier = 2;

    // Count based
    /*[SerializeField] int curHit = 0;
    [SerializeField] int totHit = 5;*/

    // Time based
    [SerializeField] float totTime = 5f;
    private float curTime = 0f;

    public IEnumerator Attack(GameObject[] zombies)
    {
        if (zombies.Length > 0) {
            float damage = 10 * damageMultiplier;
            float speed = 20 * speedMultiplier;
            for (int i = 0; i < zombies.Length; i++)
            {
                if (zombies[i] == null)
                {
                    curTime += Time.deltaTime;
                    continue;
                }

                // Count based chain bullet
                /*if(curHit < totHit)
                {
                    ParticleSystem particleSys = zombies[i].GetComponentInChildren<ParticleSystem>();
                    while (Vector3.Distance(transform.position, particleSys.transform.position) > .1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, zombies[i].GetComponentInChildren<ParticleSystem>().transform.position, 20f * Time.deltaTime);
                        yield return null;
                    }
                    curHit++;
                    Debug.LogError(damage);
                    zombies[i].GetComponent<EnemyBehaviour>().DecreaseHealth(damage / curHit, true);
                }*/

                // Time based
                if (curTime < totTime)
                {
                    ParticleSystem particleSys = zombies[i].GetComponentInChildren<ParticleSystem>();
                    while (Vector3.Distance(transform.position, particleSys.transform.position) > .1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, particleSys.transform.position, speed * Time.deltaTime);
                        curTime += Time.deltaTime;
                        yield return null;
                    }
                    zombies[i].GetComponent<EnemyBehaviour>().DecreaseHealth(damage, true);
                }
            }
            if (curTime >= totTime)
                Destroy(gameObject);
            else
                StartCoroutine(Attack(zombies));
        }
        else
            Destroy(gameObject);
    }
}
