using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

	public Transform mainCamera;

	public void NextScreen(Transform screen)
	{
		StartCoroutine(Utility.Jump(mainCamera, screen.position, 1f, null, 2f));
	}

	public void LoadLevel(string level)
	{
		SceneManager.LoadScene(level);
	}
}
