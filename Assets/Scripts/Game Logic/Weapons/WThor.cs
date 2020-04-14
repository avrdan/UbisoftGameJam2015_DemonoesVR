using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game_Logic.Weapons
{
    public class WThor : Weapon
    {
        Animator m_anim;

        int shootHash = Animator.StringToHash("SkorpionShoot");
        int idleHash = Animator.StringToHash("SkorpionIdle");
        int reloadHash = Animator.StringToHash("SkorpionReload");

        public override void Reload()
        {
            // m_anim.Play(reloadHash);
        }

        public override void Shoot()
        {
            Debug.Log("THOR SUT");
        }

        private void muzzleFlashStuff()
        {
        }

        public void ShootAnimEnded()
        {
            CancelInvoke();
        }

        public override void OnStart()
        {
            this.m_type = WeaponMgr.WeaponType.Lightning;
            WeaponMgr.AddWeapon(this);

            if(WeaponMgr.ACTIVE_WEAPON)
            {
                WeaponMgr.ACTIVE_WEAPON.gameObject.SetActive(false);
            }
            
            WeaponMgr.ACTIVE_WEAPON = this;
            WeaponMgr.ACTIVE_WEAPON.gameObject.SetActive(true);
            MuzzleFlash.SetActive(false);
            Debug.Log("SKORPIO");
            
            m_anim = transform.GetComponent<Animator>();
        }
    }
}
