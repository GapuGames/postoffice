using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Text;
using System.Collections;

public class CharacterInfo : ScriptableObject
{
	public string     characterName;
	public float      moveSpeed;
	public GameObject modelPrefab;
	[HideInInspector] public string facePath;
	[HideInInspector] public string hairPath;
	[HideInInspector] public string eyesPath;
	[HideInInspector] public string clothPath;
	[HideInInspector] public string weaponPath;
}

[CustomEditor(typeof(CharacterInfo))]
public class CharacterInfoEditor : Editor
{
	private Vector2 scrolPos1 = Vector2.zero;
	private Vector2 scrolPos2 = Vector2.zero;
	public override void OnInspectorGUI()
	{
		ProcessModel();
	}

	private void ProcessModel()
	{
		ProcessData();
		ProcessDisplay();
	}

	private void ProcessData()
	{
		serializedObject.Update();

		CharacterInfo info  = target as CharacterInfo;
		info.facePath   = FindPathByTag("Face");
		info.hairPath   = FindPathByTag("Hair");
		info.eyesPath   = FindPathByTag("Eyes");
		info.clothPath  = FindPathByTag("Cloth");
		info.weaponPath = FindPathByTag("Weapon");

		serializedObject.ApplyModifiedProperties();
	}

	private void ProcessDisplay()
	{
		base.OnInspectorGUI();
		EditorGUILayout.Separator();
		
		//! check model validation
		CharacterInfo      info  = target as CharacterInfo;
		GameObject         model = (info != null)? info.modelPrefab : null;
		Animator           anim  = (model != null)? model.GetComponent<Animator>() : null;
		AnimatorController ac    = (anim != null)? (anim.runtimeAnimatorController as AnimatorController) : null;
		{
			string message = "알수 없는 이유";
			if (model == null)     message = ("프리팹이 없어");
			else if (anim == null) message = ("애니메이터가 없어");
			else if (ac == null)   message = ("애니메이터 컨트롤러가 없어");
			
			if (ac == null)
			{
				EditorGUILayout.LabelField("모델 정보 오류",message);
				return;
			}
		}

		
		//! make string builder for message
		StringBuilder stateSB = new StringBuilder();
		StringBuilder partSB = new StringBuilder();
		
		//! extract infomation of state machine
		{
			AnimatorControllerLayer layer        = ac.layers[0];
			AnimatorStateMachine    stateMachine = layer.stateMachine;
			foreach (ChildAnimatorState state in stateMachine.states) stateSB.AppendLine(state.state.name);
		}
		
		//! extract part information
		{
			System.Func<string, string> msgFilter = (msg) => { return string.IsNullOrEmpty(msg)? "부위가 없어" : msg; };
			partSB.AppendFormat("얼굴\t: ").AppendLine(msgFilter(info.facePath));
			partSB.AppendFormat("눈\t: ").AppendLine(msgFilter(info.eyesPath));
			partSB.AppendFormat("헤어\t: ").AppendLine(msgFilter(info.hairPath));
			partSB.AppendFormat("옷\t: ").AppendLine(msgFilter(info.clothPath));
			partSB.AppendFormat("무기\t: ").AppendLine(msgFilter(info.weaponPath));
		}
		
		//! display information of model
		scrolPos1 = EditorGUILayout.BeginScrollView(scrolPos1, GUILayout.ExpandHeight(false));
		EditorGUILayout.PrefixLabel("애니메이션");
		EditorGUILayout.TextArea(stateSB.ToString(), GUILayout.ExpandHeight(false));
		EditorGUILayout.EndScrollView();
		
		scrolPos2 = EditorGUILayout.BeginScrollView(scrolPos2, GUILayout.ExpandHeight(false));
		EditorGUILayout.PrefixLabel("부위 정보");
		EditorGUILayout.TextArea(partSB.ToString(), GUILayout.ExpandHeight(false));
		EditorGUILayout.EndScrollView();
	}
	
	private string FindPathByTag(string tagName)
	{
		StringBuilder pathToPart = new StringBuilder();
		System.Func<Transform, bool> tagFinder = null;
		tagFinder = (tf) =>
		{
			bool ret = false;
			if ((tf.tag == tagName)) ret = true;
			else foreach (Transform i in tf) if (ret = tagFinder(i)) break;
			
			if (ret) pathToPart.Insert(0, string.Format("/{0}", tf.name));
			
			return ret;
		};
		
		CharacterInfo info   = target as CharacterInfo;
		GameObject     model = (info != null)? info.modelPrefab : null;
		if (model != null) foreach (Transform tf in model.transform) if (tagFinder(tf)) break;
		
		return pathToPart.ToString();
	}
}