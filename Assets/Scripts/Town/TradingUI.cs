using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingUI : MonoBehaviour {

	public Text moneyText;
	public Transform sellerList;
	public Transform buyerList;


	public void Trade(Trader trad, Hero cust)
	{
		int money = GameData.instance.gold;
		moneyText.text = money + "g";
		while (sellerList.childCount < trad.inventory.Length)
		{
			Instantiate(sellerList.GetChild(0), sellerList, false);
		}
		while (buyerList.childCount < cust.equipment.Count)
		{
			Instantiate(buyerList.GetChild(0), buyerList, false);
		}
		for (int i = 0; i < trad.inventory.Length; i++)
		{
			var item = trad.inventory[i];
			var tr = sellerList.GetChild(i);
			var b = tr.GetChild(0).GetComponent<Button>();
			int cost = item.value * 3 / 2;
			b.onClick.RemoveAllListeners();
			if (cost > money || (!item.canHasTwo && cust.equipment.Contains(item)))
			{
				b.interactable = false;
			}
			else
			{
				b.interactable = true;
				b.onClick.AddListener(() =>
				{
					GameData.instance.gold -= cost;
					cust.equipment.Add(item);
					item.Equip(cust);
					Trade(trad, cust);
				});
			}
			tr.GetChild(1).GetComponent<Text>().text =
				string.Format("{0}g \t{1} : {2}", cost, item.name, item.description);
			tr.gameObject.SetActive(true);
		}
		for (int i = 0; i < cust.equipment.Count; i++)
		{
			var item = cust.equipment[i];
			var tr = buyerList.GetChild(i);
			var b = tr.GetChild(0).GetComponent<Button>();
			int cost = item.value;
			b.onClick.RemoveAllListeners();
			b.onClick.AddListener(() =>
			{
				GameData.instance.gold += cost;
				if (cust.equipment.Remove(item))
				{
					item.Unequip(cust);
				}
				Trade(trad, cust);
			});
			tr.GetChild(1).GetComponent<Text>().text =
				string.Format("{0}g \t{1} : {2}", cost, item.name, item.description);
			tr.gameObject.SetActive(true);
		}
		for (int i = trad.inventory.Length; i < sellerList.childCount; i++)
		{
			sellerList.GetChild(i).gameObject.SetActive(false);
		}
		for (int i = cust.equipment.Count; i < buyerList.childCount; i++)
		{
			buyerList.GetChild(i).gameObject.SetActive(false);
		}
		gameObject.SetActive(true);
	}
}
