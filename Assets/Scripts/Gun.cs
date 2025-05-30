using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public enum WeaponType
    {
        NONE,
        GUN,
        FLAMETHROWER,
        LASER,
        GRENADE,
        AXE,
        BOTTLE,
        PLASMA,
        SYRINGE
    }

    public WeaponType type = WeaponType.NONE;

    public LineRenderer laserBeam;

    public ParticleSystem flameThrowerParticleEffect;
    public float flameDist;

    public ParticleSystem grenadeLauncherParticleEffect;
    public GameObject grenadePrefab;
    public float grenadeThrowForce;

    public GameObject axePrefab;
    public float axeThrowForce;

    public GameObject bottlePrefab;
    public float bottleThrowForce;

    public GameObject plasmaPrefab;
    public float plasmaThrowForce;

    public GameObject syringePrefab;
    public float syringThrowForce;

    public int damage = 10;
    public float fireRate = 15f;

    public Image fireCooldownImg;

    public int maxAmmo = 30;
    [SerializeField]private int curAmmo;
    public float reloadTime = 2f;

    float reloadStartTime;
    public Image reloadCooldownImg;

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
        if(curAmmo > maxAmmo || type == WeaponType.AXE || type == WeaponType.BOTTLE)
        {
            curAmmoTxt.text = "" + Mathf.Infinity;
        }
        else
        {
            curAmmoTxt.text = "" + curAmmo;
        }

        if (isReloading)
            return;

        if(Input.GetKeyDown(KeyCode.R) && curAmmo < maxAmmo && !(type == WeaponType.AXE) && !(type == WeaponType.BOTTLE))
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
            switch (type)
            {
                case WeaponType.NONE:
                    break;

                case WeaponType.GUN:
                    Shoot();
                    break;

                case WeaponType.AXE:
                    maxAmmo = int.MaxValue;
                    curAmmo = int.MaxValue;
                    ShootAxe(); 
                    break;

                case WeaponType.GRENADE:
                    ShootGrenade(); 
                    break;

                case WeaponType.FLAMETHROWER:
                    ShootFlame(); 
                    break;

                case WeaponType.LASER:
                    ShootLaser(); 
                    break;

                case WeaponType.BOTTLE:
                    maxAmmo = int.MaxValue;
                    curAmmo = int.MaxValue;
                    ShootBottle();
                    break;

                case WeaponType.PLASMA:
                    ShootPlasma();
                    break;

                case WeaponType.SYRINGE:
                    ShootSyringe(); 
                    break;
            }
        }

        UpdateFireCoolDown();
        UpdateReloadCoolDown();
    }

    void UpdateFireCoolDown()
    {
        if (fireCooldownImg != null)
        {
            float timeSinceLastShot = Time.time - (nextTimeToFire - 1f / fireRate);
            float cooldownProgress = Mathf.Clamp01(timeSinceLastShot / (1f / fireRate));
            fireCooldownImg.fillAmount = 1 - cooldownProgress;
        }
    }

    void UpdateReloadCoolDown()
    {
        reloadCooldownImg.fillAmount = (float)curAmmo / (float)maxAmmo;
    }

    IEnumerator UpdateReloadCoolDownReloading()
    {
        while (isReloading)
        {
            float elapsedReloadTime = Time.time - reloadStartTime;
            float reloadProgress = Mathf.Clamp01(elapsedReloadTime / reloadTime);
            //Debug.Log(reloadProgress);
            reloadCooldownImg.fillAmount = reloadProgress;
            yield return null;
        }
    }

    void ShootAxe()
    {
        curAmmo--;
        gunSound.Play();
        //grenadeLauncherParticleEffect.Play();
        var axeGO = Instantiate(axePrefab, GetComponentInChildren<ParticleSystem>().transform.position, axePrefab.transform.rotation);
        axeGO.GetComponent<Rigidbody>().AddForce(1000 * axeThrowForce * GetComponentInChildren<ParticleSystem>().transform.forward);
    }

    void ShootBottle()
    {
        curAmmo--;
        gunSound.Play();
        //grenadeLauncherParticleEffect.Play();
        var bottleGO = Instantiate(bottlePrefab, GetComponentInChildren<ParticleSystem>().transform.position, bottlePrefab.transform.rotation);
        bottleGO.GetComponent<Rigidbody>().AddForce(1000 * bottleThrowForce * GetComponentInChildren<ParticleSystem>().transform.forward);
    }

    void ShootGrenade()
    {
        curAmmo--;
        gunSound.Play();
        //grenadeLauncherParticleEffect.Play();
        var grenadeGO = Instantiate(grenadePrefab, GetComponentInChildren<ParticleSystem>().transform.position, Quaternion.identity);
        grenadeGO.GetComponent<Rigidbody>().AddForce(1000 * grenadeThrowForce * GetComponentInChildren<ParticleSystem>().transform.forward);
    }

    void ShootPlasma()
    {
        curAmmo--;
        gunSound.Play();
        //grenadeLauncherParticleEffect.Play();
        var plasmaGO = Instantiate(plasmaPrefab, GetComponentInChildren<ParticleSystem>().transform.position, Quaternion.identity);
        plasmaGO.GetComponent<Rigidbody>().AddForce(1000 * plasmaThrowForce * GetComponentInChildren<ParticleSystem>().transform.forward);
    }

    void ShootSyringe()
    {
        curAmmo--;
        gunSound.Play();
        //grenadeLauncherParticleEffect.Play();
        var syringeGO = Instantiate(syringePrefab, GetComponentInChildren<ParticleSystem>().transform.position, syringePrefab.transform.rotation);
        syringeGO.GetComponent<Rigidbody>().AddForce(1000 * syringThrowForce * GetComponentInChildren<ParticleSystem>().transform.forward);
    }

    void ShootFlame()
    {
        curAmmo--;
        gunSound.Play();
        flameThrowerParticleEffect.Play();
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
            if (hitInfo.transform.gameObject.TryGetComponent<WeaponPickup>(out WeaponPickup pickup))
            {
                if (AbilityManager.instance.playerFlamethrower.activeInHierarchy ||
                    AbilityManager.instance.playerGrenade.activeInHierarchy ||
                    AbilityManager.instance.playerLaser.activeInHierarchy)
                {
                    AbilityManager.instance.abilityType = AbilityManager.AbilityType.NONE;
                    AbilityManager.instance.abilityInventoryTxt.text = "";
                }
                pickup.ActivateWeaponInHandler();
                Destroy(pickup.gameObject);
                
                //Destroy(this.gameObject);
            }
        }
        else
        {
            //Debug.LogWarning("Not hit");
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
            if (hitInfo.transform.gameObject.TryGetComponent<WeaponPickup>(out WeaponPickup pickup))
            {
                if (AbilityManager.instance.playerFlamethrower.activeInHierarchy ||
                    AbilityManager.instance.playerGrenade.activeInHierarchy ||
                    AbilityManager.instance.playerLaser.activeInHierarchy)
                {
                    AbilityManager.instance.abilityType = AbilityManager.AbilityType.NONE;
                    AbilityManager.instance.abilityInventoryTxt.text = "";
                }
                pickup.ActivateWeaponInHandler();
                Destroy(pickup.gameObject);
                
                //Destroy(this.gameObject);
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

    public void MasterReload()
    {
        curAmmo = maxAmmo;
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        reloadStartTime = Time.time;
        StartCoroutine(UpdateReloadCoolDownReloading());
        gunReload.Play();
        animator.SetBool("isReloading", isReloading);
        reloadingTxt.gameObject.SetActive(true);
        reloadTxt.gameObject.SetActive(false);
        
        //Debug.Log("Reloading");

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
        if(muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
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
            if (hitInfo.transform.gameObject.TryGetComponent<WeaponPickup>(out WeaponPickup pickup))
            {
                if (AbilityManager.instance.playerFlamethrower.activeInHierarchy ||
                    AbilityManager.instance.playerGrenade.activeInHierarchy ||
                    AbilityManager.instance.playerLaser.activeInHierarchy)
                {
                    AbilityManager.instance.abilityType = AbilityManager.AbilityType.NONE;
                    AbilityManager.instance.abilityInventoryTxt.text = "";
                }
                pickup.ActivateWeaponInHandler();
                Destroy(pickup.gameObject);
                //Destroy(this.gameObject);
            }
        }
    }

    public void InfiniteAmmo(float time)
    {
        //Debug.Log("infite ammo");
        int curCurAmmo = curAmmo;
        curAmmo = 100000;
        StartCoroutine(ResetAmmo(time, curCurAmmo));
    }
    IEnumerator ResetAmmo(float time, int curCurAmmo)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("reset ammo");
        AbilityManager.instance.abilityInventoryTxt.text = "";
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
        //Debug.Log("reset ammo");
        fireRate = curFireRate;
        reloadTime = curReloadTime;
    }

    public void SlowFire(float time)
    {
        AbilityManager.instance.abilityInventoryTxt.text = "";
    }
}
