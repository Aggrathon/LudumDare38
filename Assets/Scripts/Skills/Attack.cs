using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Skill/Attack")]
public class Attack : Skill
{

	public int damage = 0;

	public Attack()
	{
		range = 1;
		target = Target.enemy;
	}

	public override float Activate(Hex from, Hex to)
	{
		to.occupant.ChangeHealth(-from.occupant.stats.strength-damage);
		return 0.5f;
	}
}
