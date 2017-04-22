using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Hex : MonoBehaviour {

	public BattleMap map;
	public float colorVariation = 0.1f;
	public float heightVariation = 0.1f;
	public float heightOffset = 1f;

	[System.NonSerialized]
	public GameObject occupant;
	[System.NonSerialized]
	public float height;
	public Vector3 occupantPosition { get { return transform.position + new Vector3(0, height, 0); } }



	private void OnEnable()
	{
		if (map == null)
			map = GetComponentInParent<BattleMap>();
		var mpb = new MaterialPropertyBlock();
		mpb.SetColor("_Color", map.loacationColor * (1f - colorVariation) + Random.ColorHSV(0, 1, 0.3f, 0.6f, 0, 1) * colorVariation);
		GetComponentInChildren<MeshRenderer>().SetPropertyBlock(mpb);
		height = Random.value * heightVariation + heightOffset;
		transform.GetChild(0).position = occupantPosition;
		occupant = null;
	}
}
