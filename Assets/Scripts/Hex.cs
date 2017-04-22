using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Hex : MonoBehaviour {
	
	public float colorVariation = 0.1f;
	public float heightVariation = 0.1f;
	public float heightOffset = 1f;

	[System.NonSerialized]
	public BattleCharacter occupant;
	[System.NonSerialized]
	public float height;
	public Vector3 occupantPosition { get { return new Vector3(transform.position.x, height, transform.position.z); } }

	public void Setup(Color c)
	{
		var mpb = new MaterialPropertyBlock();
		mpb.SetColor("_Color", c * (1f - colorVariation) + Random.ColorHSV(0, 1, 0.3f, 0.6f, 0, 1) * colorVariation);
		GetComponentInChildren<MeshRenderer>().SetPropertyBlock(mpb);
		height = Random.value * heightVariation + heightOffset;
		transform.GetChild(0).position = occupantPosition;
	}

	public void Reset()
	{
		occupant = null;
	}
}
