using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Skill/Shoot")]
public class Shoot : Skill
{
	public int damage;

	public Shoot()
	{
		range = 100;
		target = Target.enemy;
	}

	public override float Activate(Hex from, Hex to)
	{
		to.occupant.ChangeHealth(damage);
		return 0.5f;
	}
}
