﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BattleController : MonoBehaviour {

	public MapGenerator map;
	public GameObject battleUI;
	public Text battleTitle;
	public Text turnOrdertext;
	public Text actionButtonText;
	public GameObject selectionMarker;

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
		characters.Clear();
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
		battleUI.SetActive(true);
		//TODO activation animation
		battleTitle.text = "Heroes vs " + enemyName;
		actionButtonText.text = "Wait";
		turnOrdertext.text = "";
		Progress();
	}

	public void Finish()
	{
		selectionMarker.SetActive(false);
		map.gameObject.SetActive(false);
		//TODO deactivation animation
		battleUI.SetActive(false);
	}

	public void Progress()
	{
		UpdateTurnOrder();
		BattleCharacter currentCharacter = characters[0];
		for (int i = 1; i < characters.Count; i++)
		{
			if (characters[i].stats.health > 0 && characters[i].currentPriority > currentCharacter.currentPriority)
				currentCharacter = characters[i];
		}
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
		turnOrder.Sort((v1, v2) => v1.Key - v2.Key);
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
			if (bc.team == BattleCharacter.ENEMY_TEAM)
			{
				for (int i = 0; i < characters.Count; i++)
				{
					if (characters[i].team == BattleCharacter.ENEMY_TEAM)
						return;
				}
				//TODO Win Battle
			}
			else if (bc.team == BattleCharacter.PLAYER_TEAM)
			{
				for (int i = 0; i < characters.Count; i++)
				{
					if (characters[i].team == BattleCharacter.PLAYER_TEAM)
						return;
				}
				//TODO Loose Battle
			}
		}
	}

}
