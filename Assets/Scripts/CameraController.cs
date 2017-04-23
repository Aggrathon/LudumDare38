using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float scrollSpeed = 1f;
	public float heightVariance = 0.25f;
	public float switchSpeed = 1f;

	[Header("MapMode")]
	public float mapRange = 20f;
	public float mapAngle = -20f;
	public float mapHeight = 20f;

	[Header("BattleMode")]
	public float battleHeight = 12f;
	public float battleRange = 8f;
	public float battleAngle = -35f;


	float normHeight;
	float maxRange = 20f;

	private void Start()
	{
		normHeight = mapHeight;
		maxRange = mapRange;
		transform.eulerAngles = new Vector3(mapAngle, 0, 0);
	}

	private void Update()
	{
		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		float mpx = Input.mousePosition.x / Screen.width;
		float mpy = Input.mousePosition.y / Screen.height;
		if (mpx < 0.04f && mpx > 0)
			dir.x -= 1f;
		else if (mpx > 0.96f && mpx < 1)
			dir.x += 1f;
		if (mpy < 0.04f && mpy > 0)
			dir.z -= 1f;
		else if (mpy > 0.96f && mpy < 1)
			dir.z += 1f;
		dir = transform.position + dir * (Time.deltaTime * scrollSpeed);
		if (dir.sqrMagnitude > maxRange * maxRange)
			dir = dir.normalized*maxRange;
		transform.position = dir;

		float zoom = -Input.GetAxis("Mouse ScrollWheel");
		if (zoom != 0) {
			var tr = transform.GetChild(0).transform;
			zoom += tr.localPosition.y;
			if (zoom < normHeight && zoom < normHeight * (1 - heightVariance))
				zoom = normHeight * (1 - heightVariance);
			else if (zoom > normHeight && zoom > normHeight * (1 + heightVariance))
				zoom = normHeight * (1 + heightVariance);
			tr.localPosition = new Vector3(0, zoom, 0);
		}
	}

	public void SetBattleMode()
	{
		StartCoroutine(Utility.EaseInOut(transform, new Vector3(0,0,-2), switchSpeed));
		StartCoroutine(SwitchToBattleMode());
	}

	public void UnsetBattleMode()
	{
		StartCoroutine(SwitchToMapMode());
	}

	IEnumerator SwitchToBattleMode()
	{
		float time = 0;
		float rel_cam_height = transform.GetChild(0).transform.localPosition.y / normHeight;
		while (time < 1)
		{
			normHeight = Mathf.Lerp(mapHeight, battleHeight, time);
			transform.GetChild(0).transform.localPosition = new Vector3(0, normHeight * rel_cam_height, 0);
			maxRange = Mathf.Lerp(mapRange, battleRange, time);
			transform.eulerAngles = new Vector3(Mathf.Lerp(mapAngle, battleAngle, time), 0, 0);
			time += Time.deltaTime / switchSpeed;
			yield return null;
		}
	}

	IEnumerator SwitchToMapMode()
	{
		float time = 1;
		float rel_cam_height = transform.GetChild(0).transform.localPosition.y / normHeight;
		while (time > 0)
		{
			normHeight = Mathf.Lerp(mapHeight, battleHeight, time);
			transform.GetChild(0).transform.localPosition = new Vector3(0, normHeight * rel_cam_height, 0);
			maxRange = Mathf.Lerp(mapRange, battleRange, time);
			transform.eulerAngles = new Vector3(Mathf.Lerp(mapAngle, battleAngle, time), 0, 0);
			time -= Time.deltaTime / switchSpeed;
			yield return null;
		}
	}
}
