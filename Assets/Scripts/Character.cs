using System;
using UnityEngine;

[Serializable]
public class Character
{
	public string name = "character";
	public int health = 10;
	public int strength = 2;
	public int speed = 10;

	public Character()
	{

	}

	public Character(Character other)
	{
		name = other.name;
		health = other.health;
		strength = other.strength;
		speed = other.speed;
	}

	public Character Copy()
	{
		return new Character(this);
	}
}
