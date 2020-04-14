using UnityEngine;
using System.Collections;
using System;

public class JetPackControl : MonoBehaviour
{

    public AudioClip jetTakeoff;
    public AudioClip jetLand;

     Vector2 limit = new Vector2(100, 100);
    public AudioSource audioSource;
    

    public float fuel = 3;
    public float fuelConsumePerSecond = 1;
    public float fuelReplenishedPerSecond= 0.1F;
    public float maxFuel = 4;

    public float moveSpeed = 3;

    public float maxHeight = 3;
    public float minHeight = 0.2F;

    public Transform playerTransform;

    public Transform cameraTransform;

    Vector3 flightDirection;

    bool flying = false;

    void Start()
    {
        fuel = 3;
        fuelConsumePerSecond = 1;
        fuelReplenishedPerSecond = 0.1F;
        maxFuel = 4;
        moveSpeed = 3;
        flying = false;
        limit = new Vector2(100, 100);
        flightDirection = cameraTransform.forward;
        flightDirection.y = 0;
    }

    void Update()
    {
        if (fuel < maxFuel)
        {
            fuel = Mathf.Clamp(fuel + Time.deltaTime * fuelReplenishedPerSecond, 0, maxFuel);
        }

        if (flying)
        {
            if ( (Input.GetKey(KeyCode.Space) || OVRGamepadController.GPC_GetButton(OVRGamepadController.Button.A)) &&  fuel > 0)
            {
                
                fuel -= fuelConsumePerSecond * Time.deltaTime;
                Vector3 moveDir = flightDirection;


                if (playerTransform.position.y < maxHeight)
                {
                    moveDir.y = 1;
                }
                playerTransform.position = playerTransform.position + moveDir * Time.deltaTime * moveSpeed;
                
            }
            else
            {
                audioSource.Stop();
                flying = false;
            }

        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Space) || OVRGamepadController.GPC_GetButtonDown(OVRGamepadController.Button.A)) && fuel > 0)
            {

                Audio.PlaySound(jetTakeoff, transform.position);
                audioSource.PlayDelayed(jetTakeoff.length);
                flying = true;
                flightDirection = cameraTransform.forward;
                flightDirection.y = 0;

            }
            else
            {
                if (playerTransform.position.y > minHeight)
                {
                    Vector3 moveDir = flightDirection;
                    moveDir.y = -1;

                    playerTransform.position = playerTransform.position + moveDir * Time.deltaTime * moveSpeed;
                    if (playerTransform.position.y <= minHeight)
                    {
                        Audio.PlaySound(jetLand, transform.position);
                    }
                }
            }
        }

        Vector3 finalPos = transform.position;
        finalPos.x = Mathf.Clamp(finalPos.x, -limit.x, limit.x);
        finalPos.z = Mathf.Clamp(finalPos.z, -limit.y, limit.y);

        playerTransform.position = finalPos;
        
    }
}
