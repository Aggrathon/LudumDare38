using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skill/Shield")]
public class Shield : Skill
{
	public int amount;
	public GameObject particleEffect;

	public Shield()
	{
		range = 1;
		target = Target.self;
	}

	public override float Activate(Hex from, Hex to)
	{
		from.occupant.ChangeHealth(amount);
		if (particleEffect != null)
			Instantiate(particleEffect, to.occupantPosition + Vector3.up, Quaternion.identity);
		return 0.5f;
	}
}
