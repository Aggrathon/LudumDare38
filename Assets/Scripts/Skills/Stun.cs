using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Skill/Stun")]
public class Stun : Skill
{

	public int damage = 1;
	public int minusSpeed = 20;

	public Stun()
	{
		range = 1;
		target = Target.enemy;
	}

	public override float Activate(Hex from, Hex to)
	{
		if (damage != 0)
			to.occupant.ChangeHealth(-damage);
		to.occupant.currentPriority -= minusSpeed;
		return 0.5f;
	}
}
