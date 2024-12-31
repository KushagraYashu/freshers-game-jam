using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public enum AbilityType
    {
        NONE,           //0
        INFINITE_AMMO,   //1
        SLOW_FIRERATE,   //2
        WIND,            //3
        SKIP,            //4
        SLOW_TIME,        //5
        DAMAGE_FIELD,     //6
        POISION_GAS,
        ADRENALINE,
        RETRY
    }

    public AbilityType type;
    public float abilityTime;
}
