using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitle;

    public string[] lines;
    public float[] lineTime;

    public static SubtitleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartSubtitle()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            subtitle.text = lines[i];
            yield return new WaitForSeconds(lineTime[i]);
        }
        subtitle.text = "";
    }
}
