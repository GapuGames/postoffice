using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Text;
using System.Collections;

public class CharacterInfo : ScriptableObject
{
	public string     characterName;
	public GameObject modelPrefab;
	public float      moveSpeed;
}

[CustomEditor(typeof(CharacterInfo))]
public class CharacterInfoEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUILayout.Separator();

		//! extract infomation of state machine
		StringBuilder sb = new StringBuilder();
		{
			CharacterInfo      info = target as CharacterInfo;
			Animator           anim = null;
			AnimatorController ac   = null;

			if (info.modelPrefab == null) sb.AppendLine("프리팹이 없어");
			else if ((anim = info.modelPrefab.GetComponent<Animator>()) == null)
			{
				sb.AppendLine("애니메이터가 없어");
			}
			else if ((ac = anim.runtimeAnimatorController as AnimatorController) == null)
			{
				sb.AppendLine("애니메이터 컨트롤러가 없어");
			}
			else
			{
				AnimatorControllerLayer layer        = ac.layers[0];
				AnimatorStateMachine    stateMachine = layer.stateMachine;
				foreach (ChildAnimatorState state in stateMachine.states) sb.AppendLine(state.state.name);
			}
		}
		
		//! display information of model
		EditorGUILayout.PrefixLabel("모델 정보");
		EditorGUILayout.LabelField("애니메이션:", sb.ToString(), GUILayout.ExpandHeight(true));
		EditorGUILayout.Separator();
	}
}