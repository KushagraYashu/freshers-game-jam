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
        exploded = true;
        foreach (var zombie in LevelManager.instance.zombies)
        {
            if(zombie == null) continue;

            var dist = Vector3.Distance(this.gameObject.transform.position, zombie.transform.position);
            if (dist <= range)
            {
                zombie.GetComponent<EnemyBehaviour>().DecreaseHealth(damage / (dist + 1f), true);
            }
        }
        var effect = Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);
        effect.Play();
        GetComponent<AudioSource>().Play();
        Destroy(effect, 5f);

        GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.gameObject, GetComponent<AudioSource>().clip.length);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<WeaponPickup>(out WeaponPickup pickup))
        {
            pickup.ActivateWeaponInHandler();
            Destroy(pickup.gameObject);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("AnnoyanceShootable"))
        {
            Destroy(other.gameObject);
            ExplodeGrenade();
            exploded = true;
        }

        if (other.gameObject.TryGetComponent<Ability>(out Ability ability))
        {
            AbilityManager.instance.CallAbility((AbilityManager.AbilityType)ability.type, ability.abilityTime);
            other.gameObject.SetActive(false);
            ExplodeGrenade();
            exploded = true;
        }
    }
}
