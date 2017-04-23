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
		gameObject.SetActive(false);

		EventLog.Log(string.Format("Quest '{0}' completed!\nReward: {1} gold and {2} experience", title, goldReward, expReward));
	}
}
