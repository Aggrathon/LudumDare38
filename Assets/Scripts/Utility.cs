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

	public static IEnumerator RunLater(float time, Action action)
	{
		yield return new WaitForSeconds(time);
		action();
	}
}