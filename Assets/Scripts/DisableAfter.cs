using UnityEngine;

public class DisableAfter : MonoBehaviour
{
	public float lifeTime = 1f;

	float time;

	private void OnEnable()
	{
		time = 0;
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (time > lifeTime)
			gameObject.SetActive(false);
	}
}
