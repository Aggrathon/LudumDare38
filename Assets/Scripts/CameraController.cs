using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float scrollSpeed = 1f;
	public float maxRange = 20f;
	public float heightVariance = 0.25f;

	float normHeight;

	private void Start()
	{
		normHeight = transform.GetChild(0).transform.position.y;
	}

	private void Update()
	{
		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		float mpx = Input.mousePosition.x / Screen.width;
		float mpy = Input.mousePosition.y / Screen.height;
		if (mpx < 0.05f)
			dir.x -= 1f;
		else if (mpx > 0.95f)
			dir.x += 1f;
		if (mpy < 0.05f)
			dir.z -= 1f;
		else if (mpy > 0.95f)
			dir.z += 1f;
		dir = transform.position + dir * (Time.deltaTime * scrollSpeed);
		if (dir.sqrMagnitude > maxRange * maxRange)
			dir = dir.normalized*maxRange;
		transform.position = dir;

		float zoom = -Input.GetAxis("Mouse ScrollWheel");
		if (zoom != 0f)
		{
			var tr = transform.GetChild(0).transform;
			Vector3 pos = tr.position;
			zoom += pos.y;
			if (zoom < normHeight && zoom < normHeight * (1 - heightVariance))
				zoom = normHeight * (1 - heightVariance);
			else if (zoom > normHeight && zoom > normHeight * (1 + heightVariance))
				zoom = normHeight * (1 + heightVariance);
			tr.position = new Vector3(pos.x, zoom, pos.z);
		}
	}
}
