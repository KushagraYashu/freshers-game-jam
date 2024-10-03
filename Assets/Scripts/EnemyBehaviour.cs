using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    private bool lvlManagerCalled = false;

    public bool hit;

    public int health = 100;
    
    public float range;

    public Slider healthSlider;

    public AudioSource[] audioSources = new AudioSource[4];
    int indexAudio;

    [SerializeField]private Transform playerFeet;

    public Animator zombieAnim;

    // Start is called before the first frame update
    void Start()
    {
        FeetFinder();
        indexAudio = Random.Range(0, audioSources.Length);
        audioSources[indexAudio].Play();
    }

    void FeetFinder()
    {
        playerFeet = GameObject.FindGameObjectWithTag("PlayerFeet").transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        healthSlider.value = (float)health;
        
        if(health <= 0)
        {
            CallLevelManager();
            lvlManagerCalled = true;
            this.GetComponent<EnemyFollow>().enabled = false;
            zombieAnim.SetBool("Death", true);
            Destroy(this.gameObject, 5f);
        }

        if(Vector3.Distance(this.transform.position, playerFeet.transform.position) <= range && this.GetComponent<EnemyFollow>().enabled)
        {
            this.GetComponent<EnemyFollow>().enabled = false;
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().LoadDeadScreen();
            Debug.Log("Load Lost Screen");
        }
    }

    public void DecreaseHealth(int damage, bool hit)
    {
        zombieAnim.SetBool("Hit", true);
        GetComponentInChildren<ParticleSystem>().Play();
        GameObject.FindGameObjectWithTag("zombies").GetComponent<EnemyFollow>().UpdateHit(hit);
        this.hit = hit;
        health-= damage;
        Debug.Log("Health "+ health);
        StartCoroutine(DelayHit());
    }

    IEnumerator DelayHit()
    {
        yield return new WaitForSeconds(0.75f);
        zombieAnim.SetBool("Hit", false);
        this.hit = false;

    }

    public void CallLevelManager()
    {
        if (!lvlManagerCalled) {
            GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>().ZombieCheck();
        }
    }
}
