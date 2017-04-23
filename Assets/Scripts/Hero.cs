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

	public Hero()
	{
		skills = new List<Skill>();
		equipment = new List<Equipment>();
	}

	public Hero(Hero other) : base(other)
	{
		skills = new List<Skill>();
		skills.AddRange(other.skills);
		equipment = new List<Equipment>();
		equipment.AddRange(other.equipment);
		prefab = other.prefab;
		startingCards = other.startingCards;
		defaultAttack = other.defaultAttack;
	}
}
