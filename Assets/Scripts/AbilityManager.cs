using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public Gun gun;
    public GameObject player;
    public TextMeshProUGUI abilityTimeTxt;
    public TextMeshProUGUI abilityInventoryTxt;

    public LevelManager levelManager;

    bool timer = false;
    float time;

    public bool hasWind = false;
    public bool hasSkip = false;

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

    public void CallAbility(int index, float time)
    {
        switch (index)
        {
            case 0:
                gun = GameObject.FindAnyObjectByType<Gun>();
                gun.InfiniteAmmo(time);
                StartTimer(time);
                gun = null;
                break;

            case 1:
                TimeFreeze();
                break;

            case 2:
                gun = GameObject.FindAnyObjectByType<Gun>();
                gun.SlowFire(time);
                StartTimer(time);
                gun = null;
                break;

            case 3:
                hasWind = true;
                hasSkip = false;
                abilityInventoryTxt.text = "Q - Wind";
                break;

            case 4:
                hasSkip = true;
                hasWind = false;
                abilityInventoryTxt.text = "Q - Skip";
                break;

            default:
                Debug.Log("error in ability");
                break;
        }
    }

    

    void TimeFreeze()
    {

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
            abilityTimeTxt.text = "";
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hasWind)
            {
                WindEffect();
                hasWind = false;
            }
            if (hasSkip)
            {
                SkipLevel();
                hasSkip = false;
            }
        }
    }

    public void SkipLevel()
    {
        levelManager.SkipLevel();
    }
}
