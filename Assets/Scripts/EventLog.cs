using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLog : MonoBehaviour {

	public static EventLog instance { get; protected set; }

	private void Awake()
	{
		instance = this;
	}

	public static void Log(string text)
	{
		var tr = instance.transform.GetChild(0);
		tr.GetComponent<Text>().text = text;
		tr.SetAsLastSibling();
	}
}
