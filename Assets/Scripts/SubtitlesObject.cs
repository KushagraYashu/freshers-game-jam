using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Subtitles", menuName = "Subtitles/Create Subtitles Object")]
public class SubtitlesObject : ScriptableObject
{
    public string[] lines;
    public float[] lineTime;
}
