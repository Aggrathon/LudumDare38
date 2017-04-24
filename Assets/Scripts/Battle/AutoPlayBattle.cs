using UnityEngine;

public class AutoPlayBattle : MonoBehaviour
{
	public EnemyGroup group;
	public TestBattle battle;

	void Start()
	{
		var bc = FindObjectOfType<BattleController>();
		if (group != null && GameData.instance.heroes != null && GameData.instance.heroes.Count > 0)
		{
			bc.Battle(GameData.instance.heroes, group.enemies, group.name, group.goundColor);
		}
		else if (battle != null)
		{
			battle.Battle(bc);
		}
	}
}
