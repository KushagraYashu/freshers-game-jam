using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBulletLogic : MonoBehaviour
{
    [SerializeField] float damage = 20;
    [SerializeField] int curHit = 0;
    [SerializeField] int totHit = 5;

    public IEnumerator Attack(GameObject[] zombies)
    {
        damage = 20;
        for (int i = 0; i < Mathf.Min(totHit, zombies.Length); i++)
        {
            if (zombies[i] == null)
            {
                continue;
            }

            ParticleSystem particleSys = zombies[i].GetComponentInChildren<ParticleSystem>();
            while (Vector3.Distance(transform.position, particleSys.transform.position) > .1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, zombies[i].GetComponentInChildren<ParticleSystem>().transform.position, 20f * Time.deltaTime);
                yield return null;
            }
            curHit++;
            Debug.LogError(damage);
            zombies[i].GetComponent<EnemyBehaviour>().DecreaseHealth(damage / curHit, true);
        }
        Destroy(gameObject, 2f);
    }
}
