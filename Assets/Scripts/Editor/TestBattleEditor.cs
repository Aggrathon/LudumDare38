using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestBattle))]
public class TestBattleEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUILayout.Space();
		if (GUILayout.Button("Run Test Battle"))
		{
			if (EditorApplication.isPlaying)
				(target as TestBattle).Battle(FindObjectOfType<BattleController>());
			else
			{
				EditorApplication.isPlaying = true;
			}
		}
		GUILayout.Label("(Press twice if not in play mode)");
	}
}