﻿using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Skill/Shoot")]
public class Shoot : Skill
{
	public int damage = 3;
	public GameObject particleEffect;

	public Shoot()
	{
		range = 100;
		target = Target.enemy;
	}

	public override float Activate(Hex from, Hex to)
	{
		to.occupant.ChangeHealth(-damage);
		if (particleEffect != null)
			Instantiate(particleEffect, to.occupantPosition + Vector3.up, Quaternion.identity);
		return 0.5f;
	}
}
