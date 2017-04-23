using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public abstract class BattleCharacter : MonoBehaviour
{
	public const int ENEMY_TEAM = 2;
	public const int PLAYER_TEAM = 1;

	public GameObject deathFX;
	public Text healthText;

	[NonSerialized]
	public int team;
	[NonSerialized]
	public Character stats;
	[NonSerialized]
	public List<Skill> skills;
	[NonSerialized]
	public int currentPriority;

	public Vector3 xyPosition { get { return new Vector3(transform.position.x, 0, transform.position.z); } }
	public BattleController controller { get; set; }

	private void Start()
	{
		if (stats != null)
			healthText.text = stats.health.ToString();
		else
			healthText.text = "??";
	}

	public void ChangeHealth(int amount)
	{
		if (amount > 0)
			healthText.text = string.Format("<size=88><b><color=green>+{0}</color></b></size>", amount);
		else
			healthText.text = string.Format("<size=88><b><color=red>{0}</color></b></size>", amount);
		StartCoroutine(Utility.RunLater(2f, () => healthText.text = stats.health.ToString()));
		stats.health += amount;
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
		skills.Remove(skill);
		if (thenProgress)
			StartCoroutine(Utility.RunLater(skill.Activate(controller.GetCharacterTile(this), target), controller.Progress));
		else
			skill.Activate(controller.GetCharacterTile(this), target);
	}

	protected void OnDeath()
	{
		EventLog.Log(string.Format("{0} was defeated!", name));
		controller.KillCharacter(this);
		Instantiate(deathFX, transform.position, transform.rotation);
		Destroy(gameObject);
	}
	public abstract void TakeTurn();
}
