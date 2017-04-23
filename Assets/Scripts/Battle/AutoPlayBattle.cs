using UnityEngine;

public class AutoPlayBattle : MonoBehaviour
{
	public EnemyGroup group;
	public TestBattle battle;


	void Start()
	{
		if(group != null && GameData.instance.heroes != null && GameData.instance.heroes.Count > 0)
		{
			StartCoroutine(Utility.RunLater(0.1f, () => {
				var bc = FindObjectOfType<BattleController>();
				if (bc != null)
					bc.Battle(GameData.instance.heroes, group.enemies, group.name, group.goundColor);
			}));
		}
		else if (battle != null)
		{
			StartCoroutine(Utility.RunLater(0.1f, () => {
				var bc = FindObjectOfType<BattleController>();
				if (bc != null)
					battle.Battle(bc);
			}));
		}
	}
}
