using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public Gun gun;
    public GameObject player;
    public TextMeshProUGUI abilityTimeTxt;
    public TextMeshProUGUI abilityInventoryTxt;

    public GameObject chainBulletPrefab;

    public LevelManager levelManager;

    bool timer = false;
    float time;

    public enum AbilityType
    {
        NONE,           //0
        INFINITE_AMMO,   //1
        SLOW_FIRERATE,   //2
        WIND,            //3
        SKIP,            //4
        SLOW_TIME,        //5
        DAMAGE_FIELD,      //6
        CHAIN_BULLET,
        POISION_GAS,
        ADRENALINE,
        RETRY
    }

    public AbilityType abilityType;
    public float abilityTime;

    public bool damageField = false;

    private void Awake()
    {
        if (instance != null && instance != this) { 
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void WindEffect()
    {
        Vector3 windDirection = new(0, 0, 1);
        float windStrength = 2f;
        float windDuration = 2f;
        StartTimer(windDuration);
        foreach (GameObject go in levelManager.zombies)
        {
            if (Vector3.Distance(go.transform.position, player.transform.position) < 10f)
            {
                Debug.LogError("Applying Wind");
                StartCoroutine(ApplyWind(go, windDirection, windStrength, windDuration));
            }
        }
    }

    private IEnumerator ApplyWind(GameObject zombie, Vector3 windDirection, float windStrength, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Vector3 windEffect = Time.deltaTime * windStrength * windDirection;
            zombie.transform.position += windEffect;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void CallAbility(AbilityType type, float time)
    {
        abilityTime = time;
        switch (type)
        {
            case AbilityType.WIND:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Wind";
                break;

            case AbilityType.SKIP:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Skip Level";
                break;

            case AbilityType.INFINITE_AMMO:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Infinite Ammo";
                break;

            case AbilityType.SLOW_FIRERATE:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Slow Firerate";
                break;

            case AbilityType.SLOW_TIME:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Slow Time";
                break;

            case AbilityType.DAMAGE_FIELD:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Activate Damage Field";
                break;

            case AbilityType.POISION_GAS:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Activate Poison Gas";
                break;

            case AbilityType.CHAIN_BULLET:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Chain Bullet";
                break;

            case AbilityType.ADRENALINE:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Adrenaline";
                break;

            case AbilityType.RETRY:
                abilityType = type;
                abilityInventoryTxt.text = "Q - Retry";
                break;

            default:
                Debug.Log("error in ability");
                break;
        }
    }

    
    void StartTimer(float time)
    {
        timer = true;
        this.time = time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer && time>0)
        {
            time -= Time.deltaTime;
            time = Mathf.Max(0, time);
            abilityTimeTxt.text = "Ability Time: " + time.ToString("F2");
        }
        else if(time <= 0)
        {
            timer = false;
            abilityTimeTxt.text = "";
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (abilityType) {
                case AbilityType.NONE:
                    break;

                case AbilityType.INFINITE_AMMO:
                    abilityType = AbilityType.NONE;
                    gun = GameObject.FindAnyObjectByType<Gun>();
                    gun.InfiniteAmmo(abilityTime);
                    StartTimer(abilityTime);
                    gun = null;
                    break;

                case AbilityType.SLOW_FIRERATE:
                    abilityType = AbilityType.NONE;
                    gun = GameObject.FindAnyObjectByType<Gun>();
                    gun.SlowFire(abilityTime);
                    StartTimer(abilityTime);
                    gun = null;
                    break;

                case AbilityType.WIND:
                    WindEffect();
                    abilityType = AbilityType.NONE;
                    abilityInventoryTxt.text = "";
                    break;

                case AbilityType.SKIP:
                    SkipLevel();
                    abilityType = AbilityType.NONE;
                    abilityInventoryTxt.text = "";
                    break;

                case AbilityType.SLOW_TIME:
                    abilityType = AbilityType.NONE;
                    abilityInventoryTxt.text = "";
                    SlowTime();
                    StartTimer(abilityTime);
                    break;

                case AbilityType.DAMAGE_FIELD:
                    DamageField();
                    StartTimer(abilityTime);
                    abilityType = AbilityType.NONE;
                    abilityInventoryTxt.text = "";
                    break;

                case AbilityType.POISION_GAS:
                    PoisionGas();
                    StartTimer(abilityTime);
                    abilityType = AbilityType.NONE;
                    abilityInventoryTxt.text = "";
                    break;

                case AbilityType.CHAIN_BULLET:
                    ChainBullet();
                    abilityType = AbilityType.NONE;
                    abilityInventoryTxt.text = "";
                    break;

                case AbilityType.ADRENALINE:
                    abilityType = AbilityType.NONE;
                    gun = GameObject.FindAnyObjectByType<Gun>();
                    gun.Adrenaline(abilityTime);
                    StartTimer(abilityTime);
                    gun = null;
                    break;

                case AbilityType.RETRY:
                    levelManager.Retry();
                    abilityType = AbilityType.NONE;
                    abilityInventoryTxt.text = "";
                    break;

                default:
                    break;

            }
        }
    }

    public void ChainBullet()
    {
        gun = GameObject.FindAnyObjectByType<Gun>();
        StartCoroutine(Instantiate(chainBulletPrefab, gun.GetComponentInChildren<Transform>().position, Quaternion.identity).GetComponent<ChainBulletLogic>().Attack(levelManager.zombies));
        gun = null;
    }

    public void PoisionGas()
    {
        foreach (GameObject go in levelManager.zombies)
        {
            if (Vector3.Distance(go.transform.position, player.transform.position) < 50f)
            {
                Debug.LogError("Applying Wind");
                StartCoroutine(ApplyDamage(go, go.GetComponent<EnemyBehaviour>().health / 20, 1.25f));
            }
        }
    }

    public void DamageField()
    {
        foreach (GameObject go in levelManager.zombies)
        {
            if (Vector3.Distance(go.transform.position, player.transform.position) < 50f)
            {
                Debug.LogError("Applying Wind");
                StartCoroutine(ApplyDamage(go, go.GetComponent<EnemyBehaviour>().health/20, .75f));
            }
        }
    }

    private IEnumerator ApplyDamage(GameObject zombie, float damage, float tick)
    {
        float elapsedTime = 0;
        while (elapsedTime < abilityTime) {
            zombie.GetComponent<EnemyBehaviour>().DecreaseHealth((int)damage, true);
            elapsedTime += tick;
            yield return new WaitForSeconds(tick);
        }
    }

    public void SkipLevel()
    {
        levelManager.SkipLevel();
    }

    public void SlowTime()
    {
        float[] zomSpeed = new float[levelManager.zombies.Length];
        int i = 0;
        foreach (GameObject zombie in levelManager.zombies) {
            zomSpeed[i] = zombie.GetComponent<NavMeshAgent>().speed;
            zombie.GetComponent<NavMeshAgent>().speed /= 2;
            i++;
        }
        StartCoroutine(SpeedReset(zomSpeed, levelManager.zombies));
    }

    IEnumerator SpeedReset(float[] speed, GameObject[] zombies)
    {
        yield return new WaitForSeconds(abilityTime);
        int i = 0;
        foreach (GameObject zombie in zombies)
        {
            zombie.GetComponent<NavMeshAgent>().speed = speed[i];
            i++;
        }
    }
}
