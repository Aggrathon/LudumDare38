using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

	public List<Hero> heroes;
	public int gold = 0;
	public int experience = 0;
	public int currentScene = 0;



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

	public static void Reset()
	{
		if (_instance != null)
			Destroy(_instance.gameObject);
		_instance = null;
		if (instance.currentScene != 0)
			Debug.LogError("GameData not reset successfully");
	}

	private void Awake()
	{
		heroes = new List<Hero>();
		_instance = this;
	}
}
