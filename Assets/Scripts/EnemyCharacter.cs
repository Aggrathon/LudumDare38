using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyCharacter : BattleCharacter
{
	[SerializeField] Character character;
	[SerializeField] Skill[] abilities;

	private void Awake()
	{
		team = ENEMY_TEAM;
		stats = character;
		skills = new List<Skill>();
		if (abilities != null)
			skills.AddRange(abilities);
		currentPriority = UnityEngine.Random.Range(0, stats.speed);
	}

	public override void TakeTurn()
	{
		//TODO Implement AI
	}

	protected override void OnDeath()
	{
		controller.KillCharacter(this);
		//TODO Death animation
		Destroy(gameObject, 1f);
	}
}
