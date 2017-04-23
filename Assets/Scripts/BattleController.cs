using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BattleController : MonoBehaviour {

	public MapGenerator map;
	public BattleUI battleUI;
	public Text battleTitle;
	public Text turnOrdertext;
	public GameObject selectionMarker;
	public WorldSinker world;
	public CameraController camera;

	private List<BattleCharacter> characters;
	private List<KeyValuePair<int, BattleCharacter>> turnOrder;
	private int speedModifier;

	private void Awake()
	{
		characters = new List<BattleCharacter>();
		turnOrder = new List<KeyValuePair<int, BattleCharacter>>();
	}


	public void Battle(Hero[] players, EnemyCharacter[] enemies, string enemyName)
	{
		Battle(players, enemies, enemyName, Color.gray * 0.5f);
	}

	public void Battle(Hero[] players, EnemyCharacter[] enemies, string enemyName, Color color)
	{
		Finish(true);
		for (int i = 0; i < map.hexes.Count; i++)
		{
			map.hexes[i].Setup(color);
		}

		//Setup Characters
		for (int i = 0; i < players.Length; i++)
		{
			var bc = Instantiate(players[i].prefab, map.playerStartingLocations[i].occupantPosition, Quaternion.identity).GetComponent<PlayerCharacter>();
			bc.SetCharacter(players[i]);
			bc.controller = this;
			map.playerStartingLocations[i].occupant = bc;
			characters.Add(bc);
		}
		for (int i = 0; i < enemies.Length; i++)
		{
			var bc = Instantiate(enemies[i].gameObject, map.enemyStartingLocations[i].occupantPosition, Quaternion.identity).GetComponent<EnemyCharacter>();
			bc.controller = this;
			map.enemyStartingLocations[i].occupant = bc;
			characters.Add(bc);
		}
		characters.Sort((bc1, bc2) => bc1.currentPriority - bc2.currentPriority);
		speedModifier = characters.Max((bc) => bc.stats.speed)*2;

		map.gameObject.SetActive(true);
		battleUI.gameObject.SetActive(true);
		world.Sink();
		camera.SetBattleMode();
		battleTitle.text = "Heroes vs " + enemyName;
		UpdateTurnOrder();
		StartCoroutine(Utility.RunLater(world.animationLength, Progress));
	}

	public void Finish(bool instant=false)
	{
		selectionMarker.SetActive(false);
		battleUI.gameObject.SetActive(false);
		if (instant)
		{
			map.gameObject.SetActive(false);
			for (int i = 0; i < characters.Count; i++)
			{
				Destroy(characters[i].gameObject);
			}
			characters.Clear();
		}
		else
		{
			float delay = world.animationLength;
			for (int i = 0; i < characters.Count; i++)
			{
				Destroy(characters[i].gameObject, delay);
			}
			characters.Clear();
			StartCoroutine(Utility.RunLater(delay, () => {
				map.gameObject.SetActive(false);
			}));
		}
		world.Raise();
		camera.UnsetBattleMode();
	}

	public void Progress()
	{
		if (characters.Count == 0)
			return;
		UpdateTurnOrder();
		BattleCharacter currentCharacter = characters[0];
		for (int i = 1; i < characters.Count; i++)
		{
			if (characters[i].stats.health > 0 && characters[i].currentPriority > currentCharacter.currentPriority)
				currentCharacter = characters[i];
		}
		selectionMarker.SetActive(true);
		selectionMarker.transform.position = currentCharacter.transform.position;
		currentCharacter.TakeTurn();
		currentCharacter.currentPriority += currentCharacter.stats.speed - speedModifier;
	}

	void UpdateTurnOrder()
	{
		turnOrder.Clear();
		for (int i = 0; i < characters.Count; i++)
		{
			var c = characters[i];
			if(c.stats.health > 0)
			{
				turnOrder.Add(new KeyValuePair<int, BattleCharacter>(c.currentPriority, c));
				turnOrder.Add(new KeyValuePair<int, BattleCharacter>(c.currentPriority+c.stats.speed-speedModifier, c));
			}
		}
		turnOrder.Sort((v1, v2) => v2.Key - v1.Key);
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < turnOrder.Count; i++)
		{
			var bc = turnOrder[i].Value;
			if (bc.team == BattleCharacter.PLAYER_TEAM)
			{
				sb.Append("<color=green>");
				sb.Append(bc.stats.name);
				sb.AppendLine("</color>");
			}
			else
			{
				sb.Append("<color=red>");
				sb.Append(bc.stats.name);
				sb.AppendLine("</color>");
			}
		}
		turnOrdertext.text = sb.ToString();
	}

	public void KillCharacter(BattleCharacter bc)
	{
		if (characters.Remove(bc))
		{
			GetCharacterTile(bc).KillOccupant();
			if (bc.team == BattleCharacter.ENEMY_TEAM)
			{
				for (int i = 0; i < characters.Count; i++)
				{
					if (characters[i].team == BattleCharacter.ENEMY_TEAM)
						return;
				}
				Finish();
				//TODO Win Battle
			}
			else if (bc.team == BattleCharacter.PLAYER_TEAM)
			{
				for (int i = 0; i < characters.Count; i++)
				{
					if (characters[i].team == BattleCharacter.PLAYER_TEAM)
						return;
				}
				Finish();
				//TODO Loose Battle
			}
		}
	}

	public int GetPossibleHexes(ref List<Hex> list, int team=-1, float range=100f, Vector3 position = new Vector3())
	{
		list.Clear();
		range = range*range*map.hexDistance*map.hexDistance*1.05f;
		for (int i = 0; i < map.hexes.Count; i++)
		{
			Hex h = map.hexes[i];
			if(Vector3.SqrMagnitude(h.transform.position-position) < range)
			{
				if (team == -1 && h.occupant == null)
					list.Add(h);
				else if (h.occupant != null && h.occupant.team == team)
					list.Add(h);
			}
		}
		return list.Count;
	}

	public Hex GetCharacterTile(BattleCharacter bc)
	{
		for (int i = 0; i < map.hexes.Count; i++)
		{
			if (map.hexes[i].occupant == bc)
				return map.hexes[i];
		}
		return map.hexes[0];
	}

}
