using UnityEngine;

[CreateAssetMenu(menuName ="Data/Enemy Group", order=100)]
public class EnemyGroup : ScriptableObject
{
	public Color goundColor = Color.gray * 0.5f;
	public EnemyCharacter[] enemies;
}