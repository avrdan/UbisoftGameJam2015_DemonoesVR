using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour 
{
    [SerializeField]
    Transform xForm;
    [SerializeField]
    Transform projTarget;
    [SerializeField]
    Transform projStart;

    [SerializeField]
    float xSpeed = 5;
    [SerializeField]
    float ySpeed = 5;
    [SerializeField]
    float shotLimit = 200;
    [SerializeField]
    GUICanvas guiObject;

    [SerializeField]
    int minRange = 5;
    [SerializeField]
    int maxRange = 10;

    public AudioClip playerShoot;
    public AudioClip playerDie;
    public AudioClip playerWeaponExplosion;

    public GameObject prefabExplosion;
    float x = 0;
    float y = 0;

    bool m_oculusConnected;
    float m_timeSinceShot = 0;

    Vector3 prevMousePos;

    bool m_isDead;

    int m_weaponChangeTime;
    float m_currentWeaponTime;

    void Start()
    {
        prevMousePos = Input.mousePosition;
        OVRManager.instance.usePositionTracking = false;
        m_timeSinceShot = shotLimit;
        m_oculusConnected = false;
        m_isDead = false;
        UnityEngine.Random.seed = (int)System.DateTime.Now.Ticks;
        m_weaponChangeTime = UnityEngine.Random.Range(minRange, maxRange);
        m_currentWeaponTime = 0;

        //Debug.Log("Weapon cycle time: " + m_weaponChangeTime);
    }

    void OnEnable()
    {
        OVRManager.HMDAcquired += OnHMDAcquired;
        OVRManager.HMDLost += OnHMDLost;

        OVRGamepadController.GPC_Initialize();

        OVRManager.DismissHSWDisplay();
    }

    void OnDisable()
    {
        OVRManager.HMDAcquired -= OnHMDAcquired;
        OVRManager.HMDLost -= OnHMDLost;

        OVRGamepadController.GPC_Destroy();
    }

    private void OnHMDLost()
    {
        m_oculusConnected = false;
    }

    private void OnHMDAcquired()
    {
        m_oculusConnected = true;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
    void Update()
    {
        shotLimit = WeaponMgr.ACTIVE_WEAPON.FireRate;

        if (!m_oculusConnected)
        {

            SetupMouseLookat();
        }

        // skip tutorial, restart etc.
        ListenForMenuButtons();
        // previous, next weapon
        ListenToWeaponCycle();

        CycleWeapons();

        // TODO: also check if 0, then reset timer
        if (Input.GetMouseButtonDown(0) || Mathf.Abs(OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.RightTrigger)) > 0 || Input.GetMouseButton(0))
		{
            
            // shoot and other stuff
            if (m_timeSinceShot <= shotLimit)
            {
                m_timeSinceShot += Time.deltaTime*1000.0f; // add time in ms
                return;
            }
            else
            {
                m_timeSinceShot = 0;
                Shoot();
            }
		}

        if (Input.GetKeyDown(KeyCode.R) || OVRGamepadController.GPC_GetButtonDown(OVRGamepadController.Button.B))
            Application.LoadLevel(Application.loadedLevelName);
    }

    void ListenToWeaponCycle()
    {
        WeaponMgr.WeaponType type = WeaponMgr.ACTIVE_WEAPON.GetWeaponType();

        if (OVRGamepadController.GPC_GetButtonDown(OVRGamepadController.Button.LeftShoulder))
        {
            Debug.Log("CYCLE LEFT WPN");
            // cycle weapon left
            if (type > 0)
            {
                WeaponMgr.ChangeWeapon(type - 1);
            }
        }
        else if (OVRGamepadController.GPC_GetButtonDown(OVRGamepadController.Button.RightShoulder))
        {
            Debug.Log("CYCLE RIGHT WPN");
            // cycle weapon right
            if (type < (WeaponMgr.WeaponType.WEAPON_COUNT - 1))
            {
                WeaponMgr.ChangeWeapon(type + 1);
            }
        }
    }
    float lastShot = 0;
    float lastHitShot = 0;
    void Shoot()
    {
        if (lastShot + playerShoot.length < Time.time)
        {
            Audio.PlaySound(playerShoot, transform.position);
            lastShot = Time.time;
        }
        
        WeaponMgr.ACTIVE_WEAPON.Shoot();
        Ray ray = new Ray(xForm.position, xForm.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(WeaponMgr.ACTIVE_WEAPON.projectile != null)
        {
            var inst = Instantiate(WeaponMgr.ACTIVE_WEAPON.projectile, projStart.position, Quaternion.identity) as GameObject;
            projTarget.transform.position = projStart.position + (xForm.forward * 20);
            var set = inst.GetComponent<EffectSettings>();
            set.Target = projTarget.gameObject;
        }
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (lastShot + playerShoot.length < Time.time)
            {
                Audio.PlaySound(playerWeaponExplosion, hit.point);
                lastHitShot = Time.time;
            }
            GameObject go = Instantiate(prefabExplosion, hit.point, Quaternion.identity) as GameObject;
            Destroy(go, 2);
            
            //spawn explosion where to hit
            Vector3 explosionPos = ray.GetPoint(hit.distance - 1.0f);
            var shit = 3;
            if(WeaponMgr.ACTIVE_WEAPON.GetWeaponType() == WeaponMgr.WeaponType.Horse)
            {
                shit = 10;
            }
            Collider[] colliders = Physics.OverlapSphere(explosionPos, shit);
            foreach (Collider c in colliders)
            {
                Rigidbody rb = c.GetComponent<Rigidbody>();
                if (rb != null && c.tag == "Enemy")
                    rb.AddExplosionForce(WeaponMgr.ACTIVE_WEAPON.m_dmg, explosionPos, shit, 0.2f);
                else if(rb!= null && WeaponMgr.ACTIVE_WEAPON.GetWeaponType() == WeaponMgr.WeaponType.Horse)
                {
                    rb.AddExplosionForce(WeaponMgr.ACTIVE_WEAPON.m_dmg, explosionPos, shit, 0.2f);
                }
            }
        }
    }

    void SetupMouseLookat()
    {
        // mouse camera
        Vector3 deltaMousePos = Input.mousePosition - prevMousePos;
        prevMousePos = Input.mousePosition;

        x = x + deltaMousePos.x * xSpeed;
        y = ClampAngle(y - deltaMousePos.y * ySpeed, -100, 100);

        xForm.rotation = Quaternion.Euler(y, x, 0);
    }

    void ListenForMenuButtons()
    {
        if (guiObject.TutorialText)
        {
            if (Input.GetKeyDown(KeyCode.T) || OVRGamepadController.GPC_GetButtonDown(OVRGamepadController.Button.A) && guiObject.TutorialText)
            {
                // start game
                guiObject.TutorialText = false;
            }
        }
        else if (guiObject.RestartText)
        {

        }

    }

    public void GameOver()
    {
        // show restart text
        //GameObject canvasObj = null;

        //if (canvasObj == null)
        //    canvasObj = GameObject.FindWithTag("Canvas");

        //GUICanvas canvas = canvasObj.GetComponent<GUICanvas>();
        //canvas.RestartText = true;

        if (!m_isDead)
        {
            m_isDead = true;
             StartCoroutine(DoSomethingInAWhile(gameObject.GetComponent<Collider>()));
        }
    }

    IEnumerator DoSomethingInAWhile(Collider other)
    {
        PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
        AudioClip deathClip = playerControl.playerDie;
        Audio.PlaySound(deathClip, playerControl.transform.position);

        ScreenFader sf = other.gameObject.GetComponent<ScreenFader>();
        sf.fadeTime = 1;
        sf.fadeIn = false;

        //Debug.Log("PLAYER IS FUCKING DEAD!");
        yield return new WaitForSeconds(2);
        //Debug.Log("RESTART LEVEL!");
        Application.LoadLevel(Application.loadedLevelName);
    }

    void CycleWeapons()
    {
        if (m_currentWeaponTime < m_weaponChangeTime)
        {
            m_currentWeaponTime += Time.deltaTime;
        }

        if (m_currentWeaponTime >= m_weaponChangeTime)
        {
            int type = -1;
            type = UnityEngine.Random.Range(0, (int)WeaponMgr.WeaponType.WEAPON_COUNT);
            if (type == (int)WeaponMgr.ACTIVE_WEAPON_TYPE)
            {
                if (type > 0)
                    type -= 1;
                else
                    type += 1;
            }

            //Debug.Log("Weapon taaaaaaip: " + type);
            WeaponMgr.ChangeWeapon((WeaponMgr.WeaponType)type);

            // re-init weapon bs
            m_currentWeaponTime = 0;
            m_weaponChangeTime = UnityEngine.Random.Range(minRange, maxRange);
            //Debug.Log("Weapon cycle time: " + m_weaponChangeTime);
        }
    }
}
