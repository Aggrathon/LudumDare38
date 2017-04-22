using UnityEngine;
using System.Collections;
using System;

public static class Utility
{

	public static IEnumerator RunLater(Action action)
	{
		yield return null;
		action();
	}
}