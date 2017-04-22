using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skill/Shield")]
public class Shield : Skill
{
	public int amount;

	public Shield()
	{
		range = 1;
		target = Target.self;
	}

	public override float Activate(Hex from, Hex to)
	{
		from.occupant.ChangeHealth(amount);
		return 0.5f;
	}
}
