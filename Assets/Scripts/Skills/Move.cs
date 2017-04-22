using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Skill/Move")]
public class Move : Skill
{

	public Move()
	{
		range = 1;
		target = Target.empty;
	}

	public override float Activate(Hex from, Hex to)
	{
		from.occupant.Move(to, false);
		return 0.5f;
	}
}
