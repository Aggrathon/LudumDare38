using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

	public Transform mainCamera;
	public GameObject[] lookPrefabs;
	public Hero[] followers;
	public GameObject[] equipment;
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
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				GameData.instance.gold += goldAmount;
				break;
		}
	}

	public void LoadLevel(string level)
	{
		SceneManager.LoadScene(level);
		GameData.instance.currentTown = level;
	}
}
