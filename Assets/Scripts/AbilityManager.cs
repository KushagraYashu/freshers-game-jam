using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public Gun gun;

    public TextMeshProUGUI abilityTimeTxt;

    bool timer = false;
    float time;

    private void Awake()
    {
        if (instance != null && instance != this) { 
            Destroy(gameObject);
            return;
        }
        instance = this;
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
                gun.SlowFire();
                StartTimer(time);
                gun = null;
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
    }
}
