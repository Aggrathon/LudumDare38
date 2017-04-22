using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : BattleCharacter
{

	List<Skill> skillPool;
	Skill defaultSkill;

	public void SetCharacter(Hero hero)
	{
		stats = hero.Copy();
		defaultSkill = hero.defaultAttack;
		skills = new List<Skill>();
		skillPool = new List<Skill>();
		skillPool.AddRange(hero.skills);
		Utility.Shuffle<Skill>(ref skillPool);
		for (int i = 0; i < hero.startingCards; i++)
		{
			DrawCard();
		}
		team = PLAYER_TEAM;
		currentPriority = UnityEngine.Random.Range(0, stats.speed);
	}

	void DrawCard()
	{
		if (skillPool.Count > 0)
		{
			skills.Add(skillPool[skillPool.Count - 1]);
			skillPool.RemoveAt(skillPool.Count - 1);
		}
		else
		{
			skills.Add(defaultSkill);
		}
	}


	public override void TakeTurn()
	{
		//TODO Implement UI
		DrawCard();
		StartCoroutine(Utility.RunLater(0.2f, controller.Progress));
	}

	protected override void OnDeath()
	{
		//TODO Death animation
		controller.KillCharacter(this);
		Destroy(gameObject);
	}
}
