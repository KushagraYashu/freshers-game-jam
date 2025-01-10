using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitle;

    public string dialogue;
    List<string> words = new();
    string curWordToWrite;

    public float wpm;
    float eachWordTime;

    public int wordLimit;
    int curWord;

    public static SubtitleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        eachWordTime = wpm / 60;
        eachWordTime = 1 / eachWordTime;

        //List<char> charToAdd = new();
        //foreach(char c in dialogue.ToCharArray())
        //{
        //    if(c != ' ')
        //    {
        //        charToAdd.Add(c);
        //    }
        //    else
        //    {
        //        string res = string.Empty;
        //        foreach(var ch in charToAdd)
        //        {
        //            res += ch;
        //        }
        //        words.Add(res);
        //        charToAdd.Clear();
        //    }
        //}

        foreach (var word in dialogue.Split(' '))
        {
            if (!string.IsNullOrEmpty(word))
            {
                words.Add(word);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartSubtitle()
    {
        foreach (string word in words)
        {
            curWordToWrite = word;
            if (curWord >= wordLimit)
            {
                subtitle.text += word + " ";
                curWord++;
                yield return new WaitForSeconds(eachWordTime * curWord);
                curWord = 0;
                subtitle.text = "";
            }
            else
            {
                subtitle.text += word + " ";
                curWord++;
                //yield return new WaitForSeconds(eachWordTime);
            }
        }

        yield return new WaitForSeconds(.5f);
        subtitle.text = "";
    }
}
