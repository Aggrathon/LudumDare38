using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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

	public static void Shuffle<T>(ref List<T> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			T tmp = list[i];
			int rnd = UnityEngine.Random.Range(i, list.Count - 1);
			list[i] = list[rnd];
			list[rnd] = tmp;
		}
	}

	public static IEnumerator Jump(Transform jumper, Vector3 destination, float time = 0.5f, Action action = null, float jumpHeight = 0.5f)
	{
		Vector3 start = jumper.position;
		float t = 0f;
		while (t < 1f)
		{
			Vector3 pos = Vector3.Lerp(start, destination, t) + new Vector3(0, (1 - (2 * t - 1) * (2 * t - 1)) * jumpHeight, 0);
			jumper.position = pos;
			t += Time.deltaTime / time;
			yield return null;
		}
		jumper.position = destination;
		if (action != null)
			action();
	}

	public static IEnumerator EaseInOut(Transform transform, Vector3 destination, float time = 0.5f, Action action = null)
	{
		Vector3 start = transform.position;
		float t = 0f;
		while (t < 1f)
		{
			if (t < 0.5)
				transform.position = Vector3.Lerp(start, destination, t * t * 2);
			else
			{
				float nt = t - 1;
				transform.position = Vector3.Lerp(start, destination, 1 - nt * nt * 2);
			}
			t += Time.deltaTime / time;
			yield return null;
		}
		transform.position = destination;
		if (action != null)
			action();
	}
}