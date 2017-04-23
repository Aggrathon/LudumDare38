using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour {

	public GameObject questPopup;
	public GameObject tipPopup;
	public GameObject tradeSelect;
	public GameObject tradePopup;
	public BattleController battle;

	public static PopupManager instance { get; protected set; }

	private void Awake()
	{
		instance = this;
	}

	public void Quest(Quest quest)
	{
		questPopup.transform.GetChild(0).GetComponent<Text>().text = quest.title;
		questPopup.transform.GetChild(1).GetComponent<Text>().text = quest.text;
		Button b = questPopup.transform.GetChild(questPopup.transform.childCount - 1).GetComponent<Button>();
		b.onClick.RemoveAllListeners();
		b.onClick.AddListener(() =>
		{
			battle.Battle(GameData.instance.heroes, quest.enemies.enemies, quest.enemies.name, quest.enemies.goundColor, quest.OnComplete);
			questPopup.SetActive(false);
		});
		questPopup.SetActive(true);
	}

	public void Tip(string title, string text)
	{
		tipPopup.transform.GetChild(0).GetComponent<Text>().text = title;
		tipPopup.transform.GetChild(1).GetComponent<Text>().text = text;
		tipPopup.SetActive(true);
	}

	public void Trade(Object trader)
	{
		var tr = tradeSelect.transform;
		while (tr.childCount < GameData.instance.heroes.Count + 2)
			Instantiate(tr.GetChild(tr.childCount - 1).gameObject, tr, false);
		for (int i = 0; i < GameData.instance.heroes.Count; i++)
		{
			Button b = tr.GetChild(i + 1).GetComponent<Button>();
			Hero h = GameData.instance.heroes[i];
			b.onClick.RemoveAllListeners();
			b.onClick.AddListener(() => {
				TradeWith(h, trader);
				tradeSelect.SetActive(false);
			});
			b.GetComponentInChildren<Text>().text = h.name;
			b.gameObject.SetActive(true);
		}
		for (int i = GameData.instance.heroes.Count+1; i < tr.childCount-1; i++)
		{
			tr.GetChild(i).gameObject.SetActive(false);
		}
		tradeSelect.SetActive(true);
	}

	public void TradeWith(Hero hero, Object trader)
	{
		//TODO trade window
		Debug.Log("Trading");
	}

	public void LevelUp()
	{

	}

	public void Progress()
	{
		GameData.instance.currentScene++;
		SceneManager.LoadScene(GameData.instance.currentScene);
	}
}
