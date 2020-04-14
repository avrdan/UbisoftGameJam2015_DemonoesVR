using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour 
{
	public GameObject prefab;

	public float spawnTimer = 0;
	public const float spawnTime = 10.0f;
	int groupCount = 6;
	int someOtherShittyCounter = 0;

	SpawnFormation[] spawners = new SpawnFormation[3];
	SpawnFormation currentSpawn;
	int spawnIndex = 0;

	void Awake()
	{
		spawners[0] = new TriangleSpawn (prefab, 8);
		spawners[1] = new DiamondSpawn (prefab, 8);
		spawners [2] = new TriangleSpawn(prefab, 8);

		currentSpawn = spawners [spawnIndex];
	}

	void Start()
	{

	}

	void Update()
	{
		float distance = 60.0f;
		spawnTimer -= Time.deltaTime;
		if(spawnTimer<=0)
		{
			spawnTimer = spawnTime;
			for(int i=0; i<groupCount; i++)
			{
				float someDegree = i* 360.0f/ groupCount;
				Vector3 p = new Vector3(Mathf.Cos(someDegree * Mathf.Deg2Rad)*distance,0.5f, Mathf.Sin(someDegree * Mathf.Deg2Rad)*distance);
				currentSpawn.Spawn(p, Quaternion.Euler(0,-someDegree+90,0));
			}
			someOtherShittyCounter++;
			if(someOtherShittyCounter>=4)
			{
				someOtherShittyCounter = 0;
				groupCount++;
				Enemy.speedMultiplier += 0.5f;
			}

			spawnIndex++;
			if(spawnIndex >= spawners.Length)
				spawnIndex = 0;
			currentSpawn = spawners[spawnIndex];
		}
	}


}
