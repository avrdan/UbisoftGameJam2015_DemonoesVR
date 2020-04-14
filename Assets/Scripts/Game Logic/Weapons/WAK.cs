using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game_Logic.Weapons
{
    public class WAK : Weapon
    {
        Animator m_anim;

        int shootHash = Animator.StringToHash("AK47Shoot");
        int idleHash = Animator.StringToHash("AK47Idle");

        public override void Reload()
        {
            m_anim.Play(idleHash);
        }

        public override void Shoot()
        {
            if (MuzzleFlash != null)
            {
                InvokeRepeating("muzzleFlashStuff", 0.0f, 0.075f);
            }
            m_anim.Play(shootHash);
        }
        
        private void muzzleFlashStuff()
        {
            if(MuzzleFlash != null)
            {
                MuzzleFlash.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0.0f, 360.0f)));
                MuzzleFlash.SetActive(!MuzzleFlash.activeSelf);
            }
        }
        
        public void ShootAnimEnded()
        {
            CancelInvoke();
            if (MuzzleFlash != null)
            {
                MuzzleFlash.SetActive(false);
            }
        }
        
        public override void OnStart()
        {
            this.m_type = WeaponMgr.WeaponType.Automatic;
            WeaponMgr.AddWeapon(this);
            if (WeaponMgr.ACTIVE_WEAPON)
            {
                WeaponMgr.ACTIVE_WEAPON.gameObject.SetActive(false);
            }
            WeaponMgr.ACTIVE_WEAPON = this;
            WeaponMgr.ACTIVE_WEAPON.gameObject.SetActive(true);
           
            Debug.Log("AKK");
            if (MuzzleFlash != null)
            {
                MuzzleFlash.SetActive(false);
            }
            m_anim = transform.GetComponent<Animator>();
        }
    }
}
