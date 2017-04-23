using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Skill/Teleport")]
public class Teleport : Skill
{
	public GameObject particleEffect;

	public Teleport()
	{
		range = 100;
		target = Target.empty;
	}

	public override float Activate(Hex from, Hex to)
	{
		to.occupant = from.occupant;
		from.occupant.transform.position = to.occupantPosition;
		from.occupant = null;
		if (particleEffect != null)
			Instantiate(particleEffect, to.occupantPosition +Vector3.up, Quaternion.identity);
		return 0.5f;
	}
}
