using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Skill/Meditate")]
public class Meditate : Skill
{

	public Meditate()
	{
		range = 1;
		target = Target.self;
	}

	public override float Activate(Hex from, Hex to)
	{
		(from.occupant as PlayerCharacter).DrawCard();
		return 0.1f;
	}
}
