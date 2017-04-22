using UnityEngine;
using System.Collections.Generic;
using System;

public class BattleCharacter : MonoBehaviour
{
	[NonSerialized]
	public int team;
	[NonSerialized]
	public Character stats;
	[NonSerialized]
	public List<Skill> skills;
	[NonSerialized]
	public int currentPriority;

	public void SetCharacter(Character character, List<Skill> skills, int team)
	{
		if (character != null)
			stats = character.Copy();
		else
			stats = new Character();
		if (this.skills != null)
			this.skills.Clear();
		else
			this.skills = new List<Skill>();
		if (skills != null)
			this.skills.AddRange(skills);
		this.team = team;
		currentPriority = UnityEngine.Random.Range(0, stats.speed);
	}

	public void ChangeHealth(int amount)
	{
		stats.health += amount;
		//TODO Animate and check death
	}
}
