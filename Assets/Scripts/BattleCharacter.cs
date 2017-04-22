using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class BattleCharacter : MonoBehaviour
{
	public const int ENEMY_TEAM = 2;
	public const int PLAYER_TEAM = 1;

	public GameObject deathFX;

	[NonSerialized]
	public int team;
	[NonSerialized]
	public Character stats;
	[NonSerialized]
	public List<Skill> skills;
	[NonSerialized]
	public int currentPriority;

	public Vector3 xyPosition { get { return new Vector3(transform.position.x, 0, transform.position.y); } }
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
		if (thenProgress)
			StartCoroutine(Utility.Jump(transform, target.occupantPosition, 0.5f, controller.Progress));
		else
			StartCoroutine(Utility.Jump(transform, target.occupantPosition, 0.5f));
	}

	public void ActivateSkill(Skill skill, Hex target, bool thenProgress=true)
	{
		if (thenProgress)
			StartCoroutine(Utility.RunLater(skill.Activate(controller.GetCharacterTile(this), target), controller.Progress));
		else
			skill.Activate(controller.GetCharacterTile(this), target);
	}

	protected void OnDeath()
	{
		controller.KillCharacter(this);
		Instantiate(deathFX, transform.position, transform.rotation);
		Destroy(gameObject);
	}
	public abstract void TakeTurn();
}
