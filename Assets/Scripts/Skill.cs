using System;
using UnityEngine;


public abstract class Skill : ScriptableObject
{
	public enum Target
	{
		empty,
		enemy,
		friend,
		self
	}

	public int range = 1;
	public Target target;

	public abstract void Activate(Hex from, Hex to);
}

[CreateAssetMenu(menuName ="Data/Skill/Teleport")]
public class Teleport : Skill
{
	public GameObject particleEffect;

	public Teleport()
	{
		range = 100;
		target = Target.empty;
	}

	public override void Activate(Hex from, Hex to)
	{
		to.occupant = from.occupant;
		from.occupant.transform.position = to.occupantPosition;
		from.occupant = null;
		if (particleEffect != null)
			Instantiate(particleEffect, to.occupantPosition, Quaternion.identity);
	}
}

[CreateAssetMenu(menuName = "Data/Skill/Shoot")]
public class Shoot : Skill
{
	public int damage;

	public Shoot()
	{
		range = 100;
		target = Target.enemy;
	}

	public override void Activate(Hex from, Hex to)
	{
		to.occupant.ChangeHealth(damage);
	}
}

[CreateAssetMenu(menuName = "Data/Skill/Attack")]
public class Attack : Skill
{

	public Attack()
	{
		range = 1;
		target = Target.enemy;
	}

	public override void Activate(Hex from, Hex to)
	{
		to.occupant.ChangeHealth(-from.occupant.stats.strength);
	}
}

[CreateAssetMenu(menuName = "Data/Skill/Shield")]
public class Shield : Skill
{
	public int amount;

	public Shield()
	{
		range = 1;
		target = Target.self;
	}

	public override void Activate(Hex from, Hex to)
	{
		from.occupant.ChangeHealth(amount);
	}
}
