using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public enum AbilityType
    {
        INFINITE_AMMO,   //0
        TIME_FREEZE,    //1
        SLOW_FIRERATE,   //2
        WIND            //3
    }

    public AbilityType type;
    public float abilityTime;
}
