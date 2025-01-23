using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    public float damage = 35;

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
            Destroy(this.gameObject);
        }

        if (other.gameObject.TryGetComponent<Ability>(out Ability ability))
        {
            AbilityManager.instance.CallAbility((AbilityManager.AbilityType)ability.type, ability.abilityTime);
            other.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        if (other.transform.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            enemy.DecreaseHealth(damage, true);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject, 2f);
    }
}
