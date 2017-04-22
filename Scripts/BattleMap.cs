using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour {

	[Header("Map Generation")]
	public GameObject hexPrefab;
	public float hexDistance = 1.05f;
	public int mapSize = 7;
	public ParticleSystem clouds;
	public List<Hex> playerStartingLocations;
	public List<Hex> enemyStartingLocations;

	[Header("Battle Options")]
	public Color loacationColor;

	private void Start()
	{
		if (transform.childCount < 2)
			Generate();
	}

	private void OnEnable()
	{
		clouds.Play(true);
	}

	public void Battle(PlayerCharacter[] players, Enemy[] enemies)
	{
		Debug.Log("Battling");
		for (int i = 0; i < players.Length; i++)
		{
			playerStartingLocations[i].occupant = Instantiate(players[i].prefab, playerStartingLocations[i].occupantPosition, Quaternion.identity);
		}
		for (int i = 0; i < enemies.Length; i++)
		{
			enemyStartingLocations[i].occupant = Instantiate(enemies[i].prefab, enemyStartingLocations[i].occupantPosition, Quaternion.identity);
		}
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
				CreateHex(j - numHexesOnRow / 2, i, false, i > mapSize / 2 - 2);
				if(i!=0)
					CreateHex(j - numHexesOnRow / 2, -i, i > mapSize / 2 - 2, false);
			}
		}
		playerStartingLocations.Sort(StartingHexComparer);
		enemyStartingLocations.Sort(StartingHexComparer);
	}

	int StartingHexComparer(Hex h1, Hex h2)
	{
		int x_sqr = (int)(h1.transform.position.z * h1.transform.position.z - h2.transform.position.z * h2.transform.position.z);
		int y_sqr = (int)(h1.transform.position.x * h1.transform.position.x - h2.transform.position.x * h2.transform.position.x);
		return x_sqr*2+y_sqr;
	}

	[ContextMenu("Clear")]
	void Clear()
	{
		for (int i = transform.childCount - 1; i > 0; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
		playerStartingLocations.Clear();
		enemyStartingLocations.Clear();
	}

	void CreateHex(int x, int y, bool playerStart = false, bool enemyStart = false)
	{
		float wSize = Mathf.Cos(30f * Mathf.Deg2Rad) * hexDistance;
		float hSize = (Mathf.Sin(30f * Mathf.Deg2Rad) + 1) * 0.5f * hexDistance;
		float xOffset = wSize * 0.5f;
		GameObject go = Instantiate(hexPrefab, new Vector3(y*hSize, 0, x * wSize + Mathf.Abs(y % 2) * xOffset), Quaternion.identity, transform);
		go.name = "Tile_" + x + "_" + y;
		if (playerStart)
			playerStartingLocations.Add(go.GetComponent<Hex>());
		if (enemyStart)
			enemyStartingLocations.Add(go.GetComponent<Hex>());
	}
}
