using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {

	public MapGenerator map;
	public GameObject battleUI;
	public Text battleTitle;
	public Text turnOrdertext;
	public Text actionButtonText;


	public void Battle(PlayerCharacter[] players, Enemy[] enemies, string enemyName)
	{
		Battle(players, enemies, enemyName, Color.gray * 0.5f);
	}

	public void Battle(PlayerCharacter[] players, Enemy[] enemies, string enemyName, Color color)
	{
		for (int i = 0; i < map.hexes.Count; i++)
		{
			map.hexes[i].Setup(color);
		}
		for (int i = 0; i < players.Length; i++)
		{
			map.playerStartingLocations[i].occupant = Instantiate(players[i].prefab, map.playerStartingLocations[i].occupantPosition, Quaternion.identity);
		}
		for (int i = 0; i < enemies.Length; i++)
		{
			map.enemyStartingLocations[i].occupant = Instantiate(enemies[i].prefab, map.enemyStartingLocations[i].occupantPosition, Quaternion.identity);
		}
		map.gameObject.SetActive(true);
		battleUI.SetActive(true);
		//TODO activation animation
		battleTitle.text = "Heroes vs " + enemyName;
		actionButtonText.text = "Wait";
		turnOrdertext.text = "";
	}

	public void Finish()
	{
		map.gameObject.SetActive(false);
		//TODO deactivation animation
		battleUI.SetActive(false);
	}
}
