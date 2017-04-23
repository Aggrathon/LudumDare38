using UnityEngine;
using System.Collections;

public class Tip : MonoBehaviour
{
	public string title;
	[TextArea]
	public string text;

	public void Popup()
	{
		PopupManager.instance.Tip(title, text);
		gameObject.SetActive(false);
	}
}
