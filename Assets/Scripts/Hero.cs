using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Hero : Character
{
	public GameObject prefab;
	public List<Skill> skills;
	public int startingCards = 3;
	public List<Equipment> equipment;
	public Skill defaultAttack;
}
