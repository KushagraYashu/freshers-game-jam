using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitle;

    [SerializeField] SubtitlesObject subtitleObj;

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
        for (int i = 0; i < subtitleObj.lines.Length; i++)
        {
            subtitle.text = subtitleObj.lines[i];
            yield return new WaitForSeconds(subtitleObj.lineTime[i]);
        }
        subtitle.text = "";
    }

    public void StopSubtitles()
    {
        StopAllCoroutines();
        subtitle.gameObject.SetActive(false);
    }
}
