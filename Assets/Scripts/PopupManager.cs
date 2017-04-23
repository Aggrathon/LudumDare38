using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour {

	public GameObject questPopup;
	public GameObject tipPopup;
	public GameObject tradeSelect;
	public GameObject tradePopup;
	public GameObject levelPopup;
	public Text levelStats;
	public GameObject levelSkill;
	public Skill[] skillsToGet;
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
		Hero luh = GameData.instance.heroes[0];
		for (int i = 1; i < GameData.instance.heroes.Count; i++)
		{
			if (luh.level > GameData.instance.heroes[i].level)
				luh = GameData.instance.heroes[i];
		}
		levelStats.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", luh.name, luh.health, luh.strength, luh.speed, luh.skills.Count, luh.equipment.Count);
		levelPopup.SetActive(true);
		EventLog.Log(luh.name + " levelled up!");
	}

	public void LevelUpReward(int index)
	{
		Hero luh = GameData.instance.heroes[0];
		for (int i = 1; i < GameData.instance.heroes.Count; i++)
		{
			if (luh.level > GameData.instance.heroes[i].level)
				luh = GameData.instance.heroes[i];
		}
		switch (index)
		{
			case 0:
				luh.health += 2;
				break;
			case 1:
				luh.strength += 1;
				break;
			case 2:
				luh.speed += 2;
				break;
			case 3:
				for (int i = 1; i < levelSkill.transform.childCount; i++)
				{
					Skill s = skillsToGet[Random.Range(0, skillsToGet.Length - 1)];
					var tr = levelSkill.transform.GetChild(i);
					var b = tr.GetComponent<Button>();
					b.onClick.RemoveAllListeners();
					b.onClick.AddListener(() => {
						luh.skills.Add(s);
						levelSkill.SetActive(false);
					});
					tr.GetComponentInChildren<Text>().text = s.name;
				}
				levelSkill.SetActive(true);
				break;
		}
	}

	public void Progress()
	{
		GameData.instance.currentScene++;
		SceneManager.LoadScene(GameData.instance.currentScene);
	}
}
