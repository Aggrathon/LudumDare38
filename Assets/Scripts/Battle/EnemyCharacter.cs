using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyCharacter : BattleCharacter
{
	[SerializeField] Skill defaultAttack;
	[SerializeField] Character character;
	[SerializeField] Skill[] abilities;

	List<Hex> buffer;

	private void Awake()
	{
		team = ENEMY_TEAM;
		stats = character;
		skills = new List<Skill>();
		if (abilities != null)
			skills.AddRange(abilities);
		skills.Add(defaultAttack);
		currentPriority = UnityEngine.Random.Range(0, stats.speed);
		buffer = new List<Hex>();
	}

	public override void TakeTurn()
	{
		//Try skills
		if (skills.Count == 0 || skills[skills.Count - 1] != defaultAttack)
			skills.Add(defaultAttack);
		for (int i = 0; i < skills.Count; i++)
		{
			Skill s = skills[i];
			switch (s.target)
			{
				case Skill.Target.empty:
					if (controller.GetPossibleHexes(ref buffer, -1, s.range, xyPosition) > 0)
					{
						EventLog.Log(string.Format("{0} used '{1}'", name, s.name));
						ActivateSkill(s, buffer[UnityEngine.Random.Range(0, buffer.Count - 1)]);
						return;
					}
					break;
				case Skill.Target.enemy:
					if (controller.GetPossibleHexes(ref buffer, PLAYER_TEAM, s.range, xyPosition) > 0)
					{
						EventLog.Log(string.Format("{0} used '{1}'", name, s.name));
						ActivateSkill(s, buffer[UnityEngine.Random.Range(0, buffer.Count - 1)]);
						return;
					}
					break;
				case Skill.Target.friend:
					if (controller.GetPossibleHexes(ref buffer, ENEMY_TEAM, s.range, xyPosition) > 0)
					{
						EventLog.Log(string.Format("{0} used '{1}'", name, s.name));
						ActivateSkill(s, buffer[UnityEngine.Random.Range(0, buffer.Count - 1)]);
						return;
					}
					break;
				case Skill.Target.self:
					ActivateSkill(s, null);
					return;
			}
		}
		//if not try move:
		if (controller.GetPossibleHexes(ref buffer, PLAYER_TEAM, 100, xyPosition) > 0)
		{
			Hex target = buffer[0];
			for (int i = 1; i < buffer.Count; i++)
			{
				if (Vector3.SqrMagnitude(buffer[i].transform.position - xyPosition) < Vector3.SqrMagnitude(target.transform.position - xyPosition))
					target = buffer[i];
			}
			if (controller.GetPossibleHexes(ref buffer, -1, 1, xyPosition) > 0)
			{
				Hex moveTarget = null;
				float minDist = Vector3.SqrMagnitude(target.transform.position - xyPosition)+1.5f;
				for (int i = 0; i < buffer.Count; i++)
				{
					if (Vector3.SqrMagnitude(buffer[i].transform.position - target.transform.position) < minDist)
					{
						moveTarget = buffer[i];
						minDist = Vector3.SqrMagnitude(buffer[i].transform.position - target.transform.position);
					}
				}
				if (moveTarget != null)
				{
					Move(moveTarget, true);
					return;
				}
			}
		}
		//else do nothing
		StartCoroutine(Utility.RunLater(0.2f, controller.Progress));
	}
}
