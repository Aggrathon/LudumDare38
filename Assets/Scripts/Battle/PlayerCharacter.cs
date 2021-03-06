﻿using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : BattleCharacter
{

	List<Skill> skillPool;
	[System.NonSerialized] public Skill defaultSkill;

	public void SetCharacter(Hero hero)
	{
		stats = hero.Copy();
		defaultSkill = hero.defaultAttack;
		skills = new List<Skill>();
		skillPool = new List<Skill>();
		skillPool.AddRange(hero.skills);
		Utility.Shuffle<Skill>(ref skillPool);
		for (int i = 0; i < hero.startingCards-1; i++)
		{
			DrawCard();
		}
		team = PLAYER_TEAM;
		currentPriority = UnityEngine.Random.Range(0, stats.speed);
		for (int i = 0; i < hero.equipment.Count; i++)
		{
			hero.equipment[i].SetupBattleCharacter(this);
		}
	}

	public void DrawCard()
	{
		if (skillPool.Count > 0)
		{
			skills.Add(skillPool[skillPool.Count - 1]);
			skillPool.RemoveAt(skillPool.Count - 1);
		}
	}

	public bool SkillTargets(ref List<Hex> list, Skill skill)
	{
		switch (skill.target)
		{
			case Skill.Target.empty:
				return controller.GetPossibleHexes(ref list, -1, skill.range, xyPosition) > 0;
			case Skill.Target.enemy:
				return controller.GetPossibleHexes(ref list, ENEMY_TEAM, skill.range, xyPosition) > 0;
			case Skill.Target.friend:
				return controller.GetPossibleHexes(ref list, PLAYER_TEAM, skill.range, xyPosition) > 0;
			case Skill.Target.self:
				return true;
		}
		return false;
	}


	public override void TakeTurn()
	{
		DrawCard();
		controller.battleUI.SetupPlayer(this);
	}
}
