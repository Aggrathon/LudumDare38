using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerCharacter : BattleCharacter
{

	public void SetCharacter(Hero hero)
	{
		stats = hero.Copy();
		if (skills != null)
			skills.Clear();
		else
			skills = new List<Skill>();
		skills.AddRange(hero.skills);
		team = PLAYER_TEAM;
		currentPriority = UnityEngine.Random.Range(0, stats.speed);
	}


	public override void TakeTurn()
	{
		//TODO Implement UI
	}

	protected override void OnDeath()
	{
		//TODO Death animation
		controller.KillCharacter(this);
		Destroy(gameObject, 1f);
	}
}
