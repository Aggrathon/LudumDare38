using UnityEngine;

[CreateAssetMenu(menuName = "Data/TestBattle", order = 100)]
public class TestBattle : ScriptableObject
{
	public Hero[] players;
	public EnemyCharacter[] enemies;
	public Color color = Color.gray;
}
