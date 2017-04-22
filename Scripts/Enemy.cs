using UnityEngine;

[CreateAssetMenu(menuName ="Data/Enemy", order = 30)]
public class Enemy : ScriptableObject
{
	public Character character;
	public GameObject prefab;
}