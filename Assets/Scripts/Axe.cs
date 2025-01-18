using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public float damage = 50;
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
            Destroy(this.gameObject);
        }

        if (other.gameObject.TryGetComponent<WeaponPickup>(out WeaponPickup pickup))
        {
            pickup.ActivateWeaponInHandler();
            Destroy(pickup.gameObject);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject, 3);
    }
}
