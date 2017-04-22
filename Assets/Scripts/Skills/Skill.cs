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

	public abstract float Activate(Hex from, Hex to);
}
