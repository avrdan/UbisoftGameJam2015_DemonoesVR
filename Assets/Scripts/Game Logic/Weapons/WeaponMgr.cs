using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class WeaponMgr {

    private static List<Weapon> m_weapons = new List<Weapon>();
    // ALSO USED AS ID => Assued once weapon per type
    public enum WeaponType { Standard = 0, Lightning, Horse, Automatic, WEAPON_COUNT };
    //public static Weapon ACTIVE_WEAPON
    public static WeaponType ACTIVE_WEAPON_TYPE;
    public static Weapon ACTIVE_WEAPON;

    static bool forced = false;

    public static void ChangeWeapon(WeaponType weaponType)
    {
        ACTIVE_WEAPON_TYPE = weaponType;
        
        foreach(Weapon w in m_weapons)
        {
            if (w.GetWeaponType() == weaponType)
            {
                w.OnStart();
                return;
            }
        }
        // change weapon
        if(weaponType == WeaponType.Horse && !forced)
        {
            Debug.Log("shiet");
            forced = true;
            ChangeWeapon(WeaponType.Standard);
        }
    }

    public static void AddWeapon(Weapon wpn)
    {
        if(!m_weapons.Contains(wpn))
            m_weapons.Add(wpn);
    }

    public static void RemoveWeapon(Weapon wpn)
    {
        m_weapons.Remove(wpn);
    }

    public static List<Weapon> GetWeapons()
    {
        return m_weapons;
    }
}
