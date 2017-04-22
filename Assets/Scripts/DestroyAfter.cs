using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
	public float lifeTime = 1f;

	void Start()
	{
		Destroy(gameObject, lifeTime);
	}
}
