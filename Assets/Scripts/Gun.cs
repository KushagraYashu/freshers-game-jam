using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public bool semiAuto = false;
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

        if (semiAuto)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
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
            Debug.Log(hitInfo.transform.name);
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
                AbilityManager.instance.CallAbility((int)hitInfo.transform.gameObject.GetComponent<Ability>().type, hitInfo.transform.gameObject.GetComponent<Ability>().abilityTime);
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

    public void SlowFire()
    {

    }
}
