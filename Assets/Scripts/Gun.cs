using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource gunSound;

    public ParticleSystem muzzleFlash;

    public bool semiAuto = false;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int maxAmmo = 30;
    private int curAmmo;
    public float reloadTime = 2f;

    private bool isReloading = false;

    public Camera fpsCam;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        curAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if(curAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (semiAuto)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        curAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        curAmmo--;
        gunSound.Play();
        muzzleFlash.Play();
        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
        }
    }
}
