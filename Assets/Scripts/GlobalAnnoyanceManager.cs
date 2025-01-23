using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAnnoyanceManager : MonoBehaviour
{
    public static GlobalAnnoyanceManager Instance;

    public GameObject[] globalAnnoyances;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGlobalAnnoyance(int index)
    {
        if (index == -1)
        {
            return;
        }
        else
        {
            Instantiate(globalAnnoyances[index - 1], this.gameObject.transform);
        }
    }

    public void DisableGlobalAnnoyances()
    {
        foreach(var i in globalAnnoyances)
        {
            i.SetActive(false);
        }
    }
}
