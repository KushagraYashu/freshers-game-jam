using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public GameObject bottleMesh;
    public float damage = 30;
    public float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("zombies"))
        {
            other.gameObject.GetComponent<EnemyBehaviour>().DecreaseHealth(damage, true);
            GetComponent<AudioSource>().Play();
            bottleMesh.SetActive(false);
            Destroy(this.gameObject, GetComponent<AudioSource>().clip.length);
        }

        if (other.gameObject.TryGetComponent<WeaponPickup>(out WeaponPickup pickup))
        {
            pickup.ActivateWeaponInHandler();
            Destroy(pickup.gameObject);
            GetComponent<AudioSource>().Play();
            bottleMesh.SetActive(false);
            Destroy(this.gameObject, GetComponent<AudioSource>().clip.length);
        }

        if (other.gameObject.CompareTag("AnnoyanceShootable"))
        {
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
            bottleMesh.SetActive(false);
            Destroy(this.gameObject, GetComponent<AudioSource>().clip.length);
        }

        if (other.gameObject.TryGetComponent<Ability>(out Ability ability))
        {
            AbilityManager.instance.CallAbility((AbilityManager.AbilityType)ability.type, ability.abilityTime);
            other.gameObject.SetActive(false);
            GetComponent<AudioSource>().Play();
            bottleMesh.SetActive(false);
            Destroy(this.gameObject, GetComponent<AudioSource>().clip.length);
        }

        GetComponent<AudioSource>().Play();
        bottleMesh.SetActive(false);
        Destroy(this.gameObject, GetComponent<AudioSource>().clip.length);
    }
}
