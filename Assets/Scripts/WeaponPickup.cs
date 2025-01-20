using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public enum Weapon
    {
        AK,
        FLAMETHROWER,
        LASER,
        GRENADE,
        AXE,
        BOTTLE,
        TASER,
        SHOTGUN,
        RIFLE,
        PLASMA,
        CROSSBOW,
        SMG,
        MUSKET,
        DOUBLE_BARREL,
        SYRINGE,
        SNIPER,
        M4,
        PINK_PISTOL
    }

    public Weapon type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateWeaponInHandler()
    {
        WeaponesHandler.instance.ActivateWeapon((int)type);
        this.gameObject.SetActive(false);
    }
}
