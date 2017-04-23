using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Hex : MonoBehaviour {
	
	public float colorVariation = 0.1f;
	public float heightVariation = 0.1f;
	public float heightOffset = 1f;
	public Color bloodColor = Color.red;
	public Color selectColor = Color.green;

	private Color currentColor;

	[System.NonSerialized]
	public BattleCharacter occupant;
	[System.NonSerialized]
	public float height;
	public Vector3 occupantPosition { get { return new Vector3(transform.position.x, height, transform.position.z); } }

	public void Setup(Color c)
	{
		var mpb = new MaterialPropertyBlock();
		currentColor = c * (1f - colorVariation) + Random.ColorHSV(0, 1, 0.3f, 0.6f, 0, 1) * colorVariation;
		mpb.SetColor("_Color", currentColor);
		GetComponentInChildren<MeshRenderer>().SetPropertyBlock(mpb);
		height = Random.value * heightVariation + heightOffset;
		transform.GetChild(0).position = occupantPosition;
		occupant = null;
	}

	public void KillOccupant()
	{
		occupant = null;
		var mpb = new MaterialPropertyBlock();
		mpb.SetColor("_Color", bloodColor);
		currentColor = bloodColor;
		GetComponentInChildren<MeshRenderer>().SetPropertyBlock(mpb);
	}

	public void Reset()
	{
		occupant = null;
	}

	public void SetSelectable()
	{
		var mpb = new MaterialPropertyBlock();
		mpb.SetColor("_Color", selectColor);
		GetComponentInChildren<MeshRenderer>().SetPropertyBlock(mpb);
	}

	public void UnsetSelectable()
	{
		var mpb = new MaterialPropertyBlock();
		mpb.SetColor("_Color", currentColor);
		GetComponentInChildren<MeshRenderer>().SetPropertyBlock(mpb);
	}

	private void OnDrawGizmos()
	{
		if(occupant != null)
		{
			Gizmos.DrawWireSphere(occupantPosition + new Vector3(0, 0.5f, 0), 0.8f);
			Gizmos.DrawLine(occupantPosition + new Vector3(0, 1, 0), occupant.transform.position + new Vector3(0, 1, 0));
		}
	}
}
