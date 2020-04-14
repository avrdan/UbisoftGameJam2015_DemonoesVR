using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour
{
    public Spawner spawner;

    public GameObject player;

    void Start()
    {
    }

    public void OnPlayerGotKilled()
    {
        player.SetActive(false);
    }

    public void OnHorseGotKilled()
    {
        player.SetActive(false);
    }
}
