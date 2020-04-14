using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game_Logic.Weapons
{
    public class WHorse : Weapon
    {
        Animator m_anim;

        int shootHash = Animator.StringToHash("HorseShoot");
        int idleHash = Animator.StringToHash("HorseIdle");

        public override void Reload()
        {
            // m_anim.Play(reloadHash);
        }

        public override void Shoot()
        {
            Debug.Log("HORSE SUT");
            m_anim.Play(shootHash);
        }

        private void muzzleFlashStuff()
        {
            CancelInvoke();
            if (MuzzleFlash != null)
            {
                MuzzleFlash.SetActive(false);
            }
        }

        public void ShootAnimEnded()
        {
            CancelInvoke();
        }

        public override void OnStart()
        {
            this.m_type = WeaponMgr.WeaponType.Horse;
            WeaponMgr.AddWeapon(this);

            if(WeaponMgr.ACTIVE_WEAPON)
            {
                WeaponMgr.ACTIVE_WEAPON.gameObject.SetActive(false);
            }
            
            WeaponMgr.ACTIVE_WEAPON = this;
            WeaponMgr.ACTIVE_WEAPON.gameObject.SetActive(true);
            MuzzleFlash.SetActive(false);
            
            m_anim = transform.GetComponent<Animator>();
        }
    }
}
