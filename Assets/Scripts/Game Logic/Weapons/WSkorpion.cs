using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game_Logic.Weapons
{
    public class WSkorpion : Weapon
    {
        Animator m_anim;

        int shootHash = Animator.StringToHash("SkorpionShoot");
        int idleHash = Animator.StringToHash("SkorpionIdle");
        int reloadHash = Animator.StringToHash("SkorpionReload");

        public override void Reload()
        {
            m_anim.Play(reloadHash);
        }

        public override void Shoot()
        {
            Debug.Log("SKORPION SUT");
            if (MuzzleFlash != null)
            {
                InvokeRepeating("muzzleFlashStuff", 0.0f, 0.075f);
            }
            m_anim.Play(shootHash);
        }

        private void muzzleFlashStuff()
        {
            MuzzleFlash.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0.0f, 360.0f)));
            MuzzleFlash.SetActive(!MuzzleFlash.activeSelf);
        }

        public void ShootAnimEnded()
        {
            CancelInvoke();
            MuzzleFlash.SetActive(false);
        }

        public override void OnStart()
        {
            this.m_type = WeaponMgr.WeaponType.Standard;
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
