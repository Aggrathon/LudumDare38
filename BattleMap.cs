using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour {

	[Header("Map Generation")]
	public GameObject hexPrefab;
	public float hexDistance = 1.05f;
	public int mapSize = 7;
	[Header("Battle Options")]
	public Color loacationColor;

	private void Start()
	{
		if (transform.childCount == 0)
			Generate();
	}

	[ContextMenu("Generate")]
	void Generate()
	{
		Clear();
		for (int i = 0; i < mapSize/2+1; i++)
		{
			int numHexesOnRow = mapSize - i;
			for (int j = 0; j < numHexesOnRow; j++)
			{
				CreateHex(j - numHexesOnRow / 2, i);
				if(i!=0)
					CreateHex(j - numHexesOnRow / 2, -i);
			}
		}
	}

	[ContextMenu("Clear")]
	void Clear()
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}

	void CreateHex(int x, int y)
	{
		float wSize = Mathf.Cos(30f * Mathf.Deg2Rad) * hexDistance;
		float hSize = (Mathf.Sin(30f * Mathf.Deg2Rad) + 1) * 0.5f * hexDistance;
		float xOffset = wSize * 0.5f;
		GameObject go = Instantiate(hexPrefab, new Vector3(y*hSize, 0, x * wSize + Mathf.Abs(y % 2) * xOffset), Quaternion.identity, transform);
		go.name = "Tile_" + x + "_" + y;
	}
}
