using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

	public List<Hero> heroes;
	public int gold;
	public int experience;
	public int currentScene;



	private static GameData _instance;
	public static GameData instance
	{
		get
		{
			if (_instance != null)
				return _instance;
			else
			{
				var go = new GameObject("DATA", typeof(GameData));
				DontDestroyOnLoad(go);
				_instance = go.GetComponent<GameData>();
				return _instance;
			}
		}
	}

	private void Awake()
	{
		heroes = new List<Hero>();
		_instance = this;
	}
}
