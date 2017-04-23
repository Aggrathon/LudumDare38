using UnityEngine;
using System.Collections;

public class Trader : MonoBehaviour
{

	public Equipment[] inventory;

	public void Popup()
	{
		PopupManager.instance.Trade(this);
	}
}
