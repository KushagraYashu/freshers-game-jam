using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    private bool lvlManagerCalled = false;

    public float dist;

    public bool hit;

    public float health = 100;
    
    public float range;

    public Slider healthSlider;

    public AudioSource[] audioSources = new AudioSource[4];
    int indexAudio;

    [SerializeField]private Transform playerFeet;

    public Animator zombieAnim;

    // Start is called before the first frame update
    void Start()
    {
        dist = Mathf.Infinity;
        FeetFinder();
        indexAudio = Random.Range(0, audioSources.Length);
        audioSources[indexAudio].Play();
    }

    void FeetFinder()
    {
        playerFeet = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(playerFeet);

        healthSlider.value = (float)health;
        
        if(health <= 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            CallLevelManager();
            lvlManagerCalled = true;
            this.GetComponent<EnemyFollow>().StopMotion();
            //zombieAnim.SetBool("Death", true);
            this.gameObject.tag = "KilledZombie";
            DissolvingController dissolvingController;
            if(TryGetComponent<DissolvingController>(out dissolvingController))
            {
                StartCoroutine(dissolvingController.DissolveCo());
            }
            Destroy(this.gameObject, 2f);
        }

        dist = Vector3.Distance(this.transform.position, playerFeet.transform.position);

        if (dist <= range && this.GetComponent<EnemyFollow>().enabled)
        {
            Debug.Log("heyo Stop");
            this.GetComponent<EnemyFollow>().enabled = false;
            LevelManager.instance.LoadDeadScreen(0);
            Debug.Log("Load Lost Screen");
        }
    }

    public void DecreaseHealth(float damage, bool hit)
    {
        //zombieAnim.SetBool("Hit", true);
        GetComponentInChildren<ParticleSystem>().Play();
        //GameObject.FindGameObjectWithTag("zombies").GetComponent<EnemyFollow>().UpdateHit(hit);
        this.hit = hit;
        health-= damage;
        Debug.Log("Health "+ health);
        StartCoroutine(DelayHit());
    }

    IEnumerator DelayHit()
    {
        yield return new WaitForSeconds(0.75f);
        //zombieAnim.SetBool("Hit", false);
        this.hit = false;

    }

    public void CallLevelManager()
    {
        if (!lvlManagerCalled) {
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().ZombieCheck();
        }
    }
}
