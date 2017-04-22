using UnityEngine;
using System.Collections;

public class AutoPlayTest : MonoBehaviour
{

	public TestBattle battle;


	void Start()
	{
		if (battle != null)
		{
			StartCoroutine(Utility.RunLater(0.1f, () => {
				var bc = FindObjectOfType<BattleController>();
				if (bc != null)
					bc.Battle(battle.players, battle.enemies, battle.name, battle.color);
			}));
		}
	}
}
