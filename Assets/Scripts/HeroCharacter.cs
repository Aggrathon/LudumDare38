using UnityEngine;

[CreateAssetMenu(menuName ="Data/Hero Character", order =90)]
public class HeroCharacter : ScriptableObject
{
	[SerializeField] private Hero hero;

	public Hero GetHero()
	{
		Hero h = new Hero(hero);
		for (int i = 0; i < h.equipment.Count; i++)
		{
			h.equipment[i].Equip(h);
		}
		return h;
	}

	public static implicit operator Hero(HeroCharacter hc)
	{
		return hc.GetHero();
	}
}