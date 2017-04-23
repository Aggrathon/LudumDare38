using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Quest : MonoBehaviour
{
	public string title;
	[TextArea]
	public string text;

	public EnemyGroup enemies;

	public int goldReward;
	public int expReward;
	public Equipment[] itemReward;
	public UnityEvent onComplete;

	public void Popup()
	{
		PopupManager.instance.Quest(this);
	}

	public void OnComplete()
	{
		GameData.instance.gold += goldReward;
		if((int)Mathf.Log(GameData.instance.experience, 2) != (int)Mathf.Log(GameData.instance.experience+expReward, 2)) {
			PopupManager.instance.LevelUp();
		}
		GameData.instance.experience += expReward;
		onComplete.Invoke();
		//TODO popup reward
		gameObject.SetActive(false);
	}
}
