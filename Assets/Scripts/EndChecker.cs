using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndChecker : MonoBehaviour {

	public BattleController bc;

	private void Update()
	{
		if(bc.lastWinningTeam != -1)
		{
			if(bc.lastWinningTeam == BattleCharacter.PLAYER_TEAM)
			{
				transform.GetChild(0).gameObject.SetActive(true);
			}
			else
			{
				transform.GetChild(1).gameObject.SetActive(true);
			}
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
