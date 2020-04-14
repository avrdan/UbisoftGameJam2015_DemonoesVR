using UnityEngine;
using System.Collections;

public abstract class SpawnFormation
{
	public abstract int Spawn(Vector3 position, Quaternion orientation);
	
	protected GameObject mPrefab;
}

public class TriangleSpawn : SpawnFormation
{
	public TriangleSpawn(GameObject prefab, int size)
	{
		mPrefab = prefab;
		rows = size;
	}
	
	public override int Spawn (Vector3 position, Quaternion orientation)
	{
		int count = 0;
		Matrix4x4 mat = new Matrix4x4();
		mat.SetTRS(position,orientation,Vector3.one);
		for(int i=0;i<rows;i++)
		{
			int c = i+1;
			for(int j=0;j<c;j++)
			{
				Vector3 p = new Vector3((j-c/2.0f) *sideDistance,0,i*rowDistance);
				p = mat.MultiplyPoint(p);
				
				GameObject.Instantiate(mPrefab,p, orientation);
				count++;
			}
		}
		
		return count;
	}
	
	int rows = 30;
	float rowDistance = 0.7f;
	float sideDistance = 0.7f;
}

public class DiamondSpawn : SpawnFormation
{
	public DiamondSpawn(GameObject prefab, int size)
	{
		mPrefab = prefab;
		rows = size;
	}
	
	public override int Spawn (Vector3 position, Quaternion orientation)
	{
		int count = 0;
		Matrix4x4 mat = new Matrix4x4();
		mat.SetTRS(position,orientation,Vector3.one);
		for(int i=0;i<rows;i++)
		{
			int c = i > rows/2 ? (rows-i) : i+1;
			for(int j=0;j<c;j++)
			{
				Vector3 p = new Vector3((j-c/2.0f) *sideDistance,0,i*rowDistance);
				p = mat.MultiplyPoint(p);
				
				GameObject.Instantiate(mPrefab,p, orientation);
				count++;
			}
		}
		
		return count;
	}
	
	int rows = 30;
	float rowDistance = 0.7f;
	float sideDistance = 0.7f;
}

public class RectSpawn : SpawnFormation
{
	public RectSpawn(GameObject prefab)
	{
		mPrefab = prefab;
	}
	
	public override int Spawn (Vector3 position, Quaternion orientation)
	{
		int count = 0;
		Matrix4x4 mat = new Matrix4x4();
		mat.SetTRS(position,orientation,Vector3.one);
		
		for(int i=0;i<rows;i++)
		{
			int c = cols+(i%2==0 ? 1 : 0);
			for(int j=0;j<c; j++)
			{
				Vector3 p = new Vector3((j-c/2.0f) *sideDistance,0,i*rowDistance);
				p = mat.MultiplyPoint(p);
				
				GameObject.Instantiate(mPrefab,p, orientation);
				count++;
			}
		}
		return count;
	}
	
	int rows = 12;
	int cols = 12;
	float rowDistance = 0.6f;
	float sideDistance = 0.7f;
}
