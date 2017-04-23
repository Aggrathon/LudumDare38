using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSinker : MonoBehaviour {

	public float upHeight = 1f;
	public float downHeight = -3f;

	public float animationLength = 2f;

	[ContextMenu("Raise")]
	public void Raise()
	{
		StartCoroutine(Utility.EaseInOut(transform, new Vector3(transform.position.x, upHeight, transform.position.z), animationLength));
	}

	[ContextMenu("Sink")]
	public void Sink()
	{
		StartCoroutine(Utility.EaseInOut(transform, new Vector3(transform.position.x, downHeight, transform.position.z), animationLength));
	}
}
