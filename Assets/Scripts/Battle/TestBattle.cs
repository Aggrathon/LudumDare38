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
		bc.Battle(players.Cast<Hero>().ToList<Hero>(), enemies, name, color);
	}
}
