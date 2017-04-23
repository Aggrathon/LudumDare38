using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
	public enum State
	{
		wait,
		view,
		action,
		target
	}

	public Text actionButtonText;
	public GameObject actionPanel;
	public Transform cardList;
	public Skill move;
	public Skill meditate;

	State state;
	List<Hex> buffer;
	PlayerCharacter character;
	Skill skill;

	private void OnEnable()
	{
		Wait();
	}

	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Tab))
		{
			Action();
		}
		if (state == State.target)
		{
			if (skill == null)
			{
				Action();
				return;
			}
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit hit;
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
				{
					Hex h = hit.collider.GetComponentInParent<Hex>();
					if (h != null && buffer.Contains(h))
					{
						DoSkill(skill, h);
					}
				}
			}
		}
	}

	public void Action()
	{
		switch (state)
		{
			case State.wait:
				break;
			case State.view:
				SelectAction();
				break;
			case State.action:
				View();
				break;
			case State.target:
				SelectAction();
				break;
		}
	}

	public void SetupPlayer(PlayerCharacter pc)
	{
		character = pc;
		while (pc.skills.Count +3 > cardList.childCount)
		{
			Instantiate(cardList.GetChild(0), cardList, false);
		}
		SetSkillCard(meditate, 0, true);
		SetSkillCard(move, 1, true);
		SetSkillCard(pc.defaultSkill, 2, true);
		for (int i = 3; i < pc.skills.Count+3; i++)
		{
			SetSkillCard(pc.skills[i - 3], i);
		}
		for (int i = pc.skills.Count+3; i < cardList.childCount; i++)
		{
			cardList.GetChild(i).gameObject.SetActive(false);
		}
		SelectAction();
	}

	void SetSkillCard(Skill sk, int i, bool permanent=false)
	{
		Transform tr = cardList.GetChild(i);
		Button b = tr.GetComponent<Button>();
		tr.GetChild(0).GetComponent<Text>().text = sk.name;
		tr.GetChild(1).GetComponent<Image>().sprite = sk.icon;
		tr.GetChild(2).GetComponent<Text>().text = permanent ? sk.description + "\n<color=#555><i>(Permanent)</i></color>" : sk.description;
		b.interactable = true;
		b.onClick.RemoveAllListeners();
		b.onClick.AddListener(() => { SelectTarget(sk, b); });
		tr.gameObject.SetActive(true);
	}

	void View()
	{
		state = State.view;
		actionButtonText.text = "Play Card";
		actionPanel.SetActive(false);
		Unselect();
	}

	void SelectAction()
	{
		state = State.action;
		actionButtonText.text = "Hide Panel";
		actionPanel.SetActive(true);
		Unselect();
	}

	public void SelectTarget(Skill skill, Button button)
	{
		Unselect();
		if (skill.target == Skill.Target.self)
		{
			DoSkill(skill, null);
			return;
		}
		if(character.SkillTargets(ref buffer, skill))
		{
			state = State.target;
			actionButtonText.text = "Cancel Card";
			actionPanel.SetActive(false);
			for (int i = 0; i < buffer.Count; i++)
			{
				buffer[i].SetSelectable();
			}
			this.skill = skill;
		}
		else if (button != null)
		{
			button.interactable = false;
		}
	}

	void DoSkill(Skill skill, Hex target)
	{
		character.ActivateSkill(skill, target, true);
		Wait();
	}

	void Unselect()
	{
		if (buffer == null)
			buffer = new List<Hex>();
		else
		{
			for (int i = 0; i < buffer.Count; i++)
			{
				buffer[i].UnsetSelectable();
			}
			buffer.Clear();
		}
		skill = null;
	}

	void Wait()
	{
		state = State.wait;
		actionButtonText.text = "Wait";
		actionPanel.SetActive(false);
		character = null;
		Unselect();
	}
}
