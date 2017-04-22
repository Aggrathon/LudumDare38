﻿using System;
using UnityEngine;

[Serializable]
public class Character
{
	public string name = "character";
	public int health = 10;
	public int strength = 2;
	public int speed = 10;

	public Character Copy()
	{
		return JsonUtility.FromJson<Character>(JsonUtility.ToJson(this));
	}
}