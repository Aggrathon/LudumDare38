using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class BattleCharacter : MonoBehaviour
{
	public const int ENEMY_TEAM = 2;
	public const int PLAYER_TEAM = 1;

	[NonSerialized]
	public int team;
	[NonSerialized]
	public Character stats;
	[NonSerialized]
	public List<Skill> skills;
	[NonSerialized]
	public int currentPriority;

	public BattleController controller { get; set; }

	public void ChangeHealth(int amount)
	{
		stats.health += amount;
		//TODO Animate health change
		if (stats.health <= 0)
			OnDeath();
	}

	public void Move(Hex target, bool thenProgress=false)
	{
		controller.GetCharacterTile(this).occupant = null;
		target.occupant = this;
		//TODO Animate move
		transform.position = target.occupantPosition;
		if (thenProgress)
			StartCoroutine(Utility.RunLater(0.2f, controller.Progress));
	}

	protected abstract void OnDeath();
	public abstract void TakeTurn();
}
