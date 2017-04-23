using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Skill/SpeedBuff")]
public class SpeedBuff : Skill
{
	public GameObject particleEffect;
	public float multiplier = 1.5f;

	public SpeedBuff()
	{
		range = 1;
		target = Target.friend;
	}

	public override float Activate(Hex from, Hex to)
	{
		to.occupant.stats.speed = (int)((float)to.occupant.stats.speed * multiplier);
		if (particleEffect != null)
			Instantiate(particleEffect, to.occupantPosition + Vector3.up, Quaternion.identity);
		return 0.5f;
	}
}
