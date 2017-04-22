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
		if(skills.Count == 0)
			skills.Add(defaultAttack);
		for (int i = 0; i < skills.Count; i++)
		{
			Skill s = skills[i];
			switch (s.target)
			{
				case Skill.Target.empty:
					if (controller.GetPossibleHexes(ref buffer, -1, s.range, xyPosition) > 0)
					{
						skills.RemoveAt(i);
						ActivateSkill(s, buffer[UnityEngine.Random.Range(0, buffer.Count - 1)]);
						return;
					}
					break;
				case Skill.Target.enemy:
					if (controller.GetPossibleHexes(ref buffer, PLAYER_TEAM, s.range, xyPosition) > 0)
					{
						skills.RemoveAt(i);
						ActivateSkill(s, buffer[UnityEngine.Random.Range(0, buffer.Count - 1)]);
						return;
					}
					break;
				case Skill.Target.friend:
					if (controller.GetPossibleHexes(ref buffer, ENEMY_TEAM, s.range, xyPosition) > 0)
					{
						skills.RemoveAt(i);
						ActivateSkill(s, buffer[UnityEngine.Random.Range(0, buffer.Count - 1)]);
						return;
					}
					break;
				case Skill.Target.self:
					skills.RemoveAt(i);
					ActivateSkill(s, null);
					return;
			}
		}
		//if not try move:
		if(controller.GetPossibleHexes(ref buffer, PLAYER_TEAM) > 0)
		{
			Hex target = buffer[0];
			for (int i = 1; i < buffer.Count; i++)
			{
				if (Vector3.SqrMagnitude(buffer[i].transform.position - transform.position) < Vector3.SqrMagnitude(target.transform.position - transform.position))
					target = buffer[i];
			}
			if (controller.GetPossibleHexes(ref buffer, -1, 1, new Vector3(transform.position.x, 0, transform.position.y)) > 0)
			{
				Hex moveTarget = null;
				float minDist = Vector2.SqrMagnitude(new Vector3(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z));
				for (int i = 0; i < buffer.Count; i++)
				{
					if (Vector3.SqrMagnitude(buffer[i].transform.position - target.transform.position) - minDist < 0.2f)
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
