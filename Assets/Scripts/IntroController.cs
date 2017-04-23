using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

	public Transform mainCamera;
	public GameObject[] lookPrefabs;
	public HeroCharacter[] followers;
	public Equipment[] equipment;
	public int goldAmount = 100;

	public void NextScreen(Transform screen)
	{
		StartCoroutine(Utility.Jump(mainCamera, screen.position, 1.2f, null, 4f));
	}

	public void SetQuest(int i)
	{
		GameData.instance.heroes.Add(new Hero());
	}

	public void SetLook(int i)
	{
		GameData.instance.heroes[0].prefab = lookPrefabs[i];
	}

	public void SetFollower(int i)
	{
		GameData.instance.heroes.Add(followers[i]);
	}

	public void SetEquipment(int i)
	{
		switch (i)
		{
			case 0:
				GameData.instance.heroes[0].equipment.Add(equipment[0]);
				equipment[0].Equip(GameData.instance.heroes[0]);
				break;
			case 1:
				GameData.instance.heroes[0].equipment.Add(equipment[1]);
				equipment[1].Equip(GameData.instance.heroes[0]);
				break;
			case 2:
				GameData.instance.heroes[0].equipment.Add(equipment[2]);
				equipment[2].Equip(GameData.instance.heroes[0]);
				break;
			case 3:
				GameData.instance.gold += goldAmount;
				break;
		}
	}

	public void NextLevel()
	{
		SceneManager.LoadScene(1);
		GameData.instance.currentScene = 1;
	}
}
