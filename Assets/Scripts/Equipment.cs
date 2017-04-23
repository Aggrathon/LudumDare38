using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Equipment")]
public class Equipment : ScriptableObject
{
	[TextArea]
	public string description;
	public int value = 60;
	public int healthDelta = 0;
	public int strengthDelta = 0;
	public int speedDelta = 0;
	public List<Skill> skills;
	public Skill defaultSkill = null;

	public void Equip(Hero hero)
	{
		hero.health += healthDelta;
		hero.strength += strengthDelta;
		hero.speed += speedDelta;
		hero.skills.AddRange(skills);
	}

	public void Unequip(Hero hero)
	{
		hero.health -= healthDelta;
		hero.strength -= strengthDelta;
		hero.speed -= speedDelta;
		for (int i = 0; i < skills.Count; i++)
		{
			hero.skills.Remove(skills[i]);
		}
	}

	public void SetupBattleCharacter(PlayerCharacter pc)
	{
		if (defaultSkill != null)
			pc.defaultSkill = defaultSkill;
	}
}