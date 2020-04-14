using UnityEngine;
using System.Collections;


public abstract class Weapon : MonoBehaviour {

    // amount of damage
    public float m_dmg;
    protected int m_maxFireRate; // in ms
    public WeaponMgr.WeaponType m_type;
    public GameObject projectile;

    public GameObject MuzzleFlash = null;
    public int FireRate = 20;

	// Use this for initialization
	void Start () {
        OnStart();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public WeaponMgr.WeaponType GetWeaponType()
    {
        return m_type;
    }

    public abstract void Shoot();
    // maybe won't need this
    public abstract void Reload();

    public abstract void OnStart();
}
