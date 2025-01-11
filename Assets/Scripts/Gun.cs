using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource hitFeedback;

    public Animator animator;

    public TextMeshProUGUI curAmmoTxt;
    public TextMeshProUGUI reloadTxt;
    public TextMeshProUGUI reloadingTxt;


    public AudioSource gunSound;
    public AudioSource gunReload;

    public ParticleSystem muzzleFlash;

    public bool laser = false;
    public LineRenderer laserBeam;

    public bool flameThrower = false;
    public ParticleSystem flameThrowerParticleEffect;
    public float flameDist;

    public bool grenade = false;
    public ParticleSystem grenadeLauncherParticleEffect;
    public GameObject grenadePrefab;
    public float grenadeThrowForce;

    public int damage = 10;
    public float fireRate = 15f;

    public int maxAmmo = 30;
    [SerializeField]private int curAmmo;
    public float reloadTime = 2f;

    public bool isReloading = false;

    public Camera fpsCam;

    private float nextTimeToFire = 0f;

    public int getCurAmmo()
    {
        return curAmmo;
    }

    private void Start()
    {
        curAmmo = maxAmmo;
        
    }

    private void OnEnable()
    {
        //curAmmoTxt = GameObject.FindGameObjectWithTag("CurAmmoTxt").GetComponent<TextMeshProUGUI>();
        reloadTxt.gameObject.SetActive(false);
        reloadingTxt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(curAmmo > maxAmmo)
        {
            curAmmoTxt.text = "" + Mathf.Infinity;
        }
        else
        {
            curAmmoTxt.text = "" + curAmmo;
        }

        if (isReloading)
            return;

        if(Input.GetKeyDown(KeyCode.R) && curAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if(curAmmo <= 0)
        {
            reloadTxt.gameObject.SetActive(true);
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            if (!laser && !flameThrower && !grenade)
            {
                Shoot();
            }
            else if(laser)
            {
                ShootLaser();
            }
            else if(flameThrower)
            {
                ShootFlame();
            }
            else if (grenade)
            {
                ShootGrenade();
            }
        }

        
    }

    void ShootGrenade()
    {
        curAmmo--;
        gunSound.Play();
        //grenadeLauncherParticleEffect.Play();
        var grenadeGO = Instantiate(grenadePrefab, GetComponentInChildren<ParticleSystem>().transform.position, Quaternion.identity);
        grenadeGO.GetComponent<Rigidbody>().AddForce(1000 * grenadeThrowForce * GetComponentInChildren<ParticleSystem>().transform.forward);
    }

    void ShootFlame()
    {
        curAmmo--;
        gunSound.Play();
        //flameThrowerParticleEffect.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hitInfo, flameDist))
        {
            //Debug.Log(hitInfo.transform.name);
            if (hitInfo.transform.CompareTag("AnnoyanceShootable"))
            {
                hitFeedback.Play();
                Destroy(hitInfo.transform.gameObject, .2f);
            }
            if (hitInfo.transform.gameObject.GetComponent<EnemyBehaviour>())
            {
                hitFeedback.Play();
                hitInfo.transform.gameObject.GetComponent<EnemyBehaviour>().DecreaseHealth(damage, true);
            }
            if (hitInfo.transform.gameObject.GetComponent<PlayableMenuButtons>())
            {
                hitInfo.transform.gameObject.GetComponent<PlayableMenuButtons>().hit = true;
            }
            if (hitInfo.transform.gameObject.GetComponent<Ability>())
            {
                AbilityManager.instance.CallAbility((AbilityManager.AbilityType)hitInfo.transform.gameObject.GetComponent<Ability>().type, hitInfo.transform.gameObject.GetComponent<Ability>().abilityTime);
                hitInfo.transform.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Not hit");
        }
    }

    void ShootLaser()
    {
        curAmmo--;
        gunSound.Play();
        //muzzleFlash.Play();
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo))
        {
            StartCoroutine(ShowLaserBeam(hitInfo.point));
            //Debug.Log(hitInfo.transform.name);
            if (hitInfo.transform.CompareTag("AnnoyanceShootable"))
            {
                hitFeedback.Play();
                Destroy(hitInfo.transform.gameObject, .2f);
            }
            if (hitInfo.transform.gameObject.GetComponent<EnemyBehaviour>())
            {
                hitFeedback.Play();
                hitInfo.transform.gameObject.GetComponent<EnemyBehaviour>().DecreaseHealth(damage, true);
            }
            if (hitInfo.transform.gameObject.GetComponent<PlayableMenuButtons>())
            {
                hitInfo.transform.gameObject.GetComponent<PlayableMenuButtons>().hit = true;
            }
            if (hitInfo.transform.gameObject.GetComponent<Ability>())
            {
                AbilityManager.instance.CallAbility((AbilityManager.AbilityType)hitInfo.transform.gameObject.GetComponent<Ability>().type, hitInfo.transform.gameObject.GetComponent<Ability>().abilityTime);
                hitInfo.transform.gameObject.SetActive(false);
            }
        }
        else
        {
            StartCoroutine(ShowLaserBeam(GetComponentInChildren<ParticleSystem>().transform.position + GetComponentInChildren<ParticleSystem>().transform.forward * 1000f));
        }
    }

    IEnumerator ShowLaserBeam(Vector3 endPoint)
    {
        laserBeam.SetPosition(0, GetComponentInChildren<ParticleSystem>().transform.position);
        laserBeam.SetPosition(1, endPoint);
        laserBeam.enabled = true;

        yield return new WaitForSeconds(.5f);

        laserBeam.enabled = false;
    }


    IEnumerator Reload()
    {
        isReloading = true;
        gunReload.Play();
        animator.SetBool("isReloading", isReloading);
        reloadingTxt.gameObject.SetActive(true);
        reloadTxt.gameObject.SetActive(false);
        
        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        curAmmo = maxAmmo;
        isReloading = false;
        animator.SetBool("isReloading", isReloading);

        reloadingTxt.gameObject.SetActive(false);
    }

    void Shoot()
    {
        curAmmo--;
        gunSound.Play();
        muzzleFlash.Play();
        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo))
        {
            //Debug.Log(hitInfo.transform.name);
            if (hitInfo.transform.CompareTag("AnnoyanceShootable"))
            {
                hitFeedback.Play();
                Destroy(hitInfo.transform.gameObject, .2f);
            }
            if (hitInfo.transform.gameObject.GetComponent<EnemyBehaviour>())
            {
                hitFeedback.Play();
                hitInfo.transform.gameObject.GetComponent<EnemyBehaviour>().DecreaseHealth(damage, true);
            }
            if (hitInfo.transform.gameObject.GetComponent<PlayableMenuButtons>())
            {
                hitInfo.transform.gameObject.GetComponent<PlayableMenuButtons>().hit = true;
            }
            if (hitInfo.transform.gameObject.GetComponent<Ability>())
            {
                AbilityManager.instance.CallAbility((AbilityManager.AbilityType)hitInfo.transform.gameObject.GetComponent<Ability>().type, hitInfo.transform.gameObject.GetComponent<Ability>().abilityTime);
                hitInfo.transform.gameObject.SetActive(false);
            }
        }
    }

    public void InfiniteAmmo(float time)
    {
        Debug.Log("infite ammo");
        int curCurAmmo = curAmmo;
        curAmmo = 100000;
        StartCoroutine(ResetAmmo(time, curCurAmmo));
    }
    IEnumerator ResetAmmo(float time, int curCurAmmo)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("reset ammo");
        curAmmo = curCurAmmo;
    }

    public void Adrenaline(float time)
    {
        float curFireRate = fireRate;
        float curReloadTime = reloadTime;
        fireRate *= 2;
        reloadTime /= 2;
        StartCoroutine(ResetAdrenaline(time, curFireRate, reloadTime));
    }

    IEnumerator ResetAdrenaline(float time, float curFireRate, float curReloadTime)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("reset ammo");
        fireRate = curFireRate;
        reloadTime = curReloadTime;
    }

    public void SlowFire(float time)
    {

    }
}
