using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Data/TestBattle", order = 100)]
public class TestBattle : ScriptableObject
{
	public List<HeroCharacter> players;
	public EnemyCharacter[] enemies;
	public Color color = Color.gray;

	public void Battle(BattleController bc)
	{
		List<Hero> list = new List<Hero>();
		foreach (var item in players)
		{
			list.Add(item);
		}
		bc.Battle(list, enemies, name, color);
	}
}
