using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class CharacterInfo : ScriptableObject
{
	public string      characterName;
	public float       moveSpeed;
	public GameObject  modelPrefab;
	public ActiveSkill activeSkill;
	//public int        characteristic;
	[HideInInspector] public string facePath;
	[HideInInspector] public string hairPath;
	[HideInInspector] public string eyesPath;
	[HideInInspector] public string clothPath;
	[HideInInspector] public string weaponPath;

	public Character Create()
	{
		GameObject    shadowPrefab = Resources.Load<GameObject>("Prefab/Unclassfied/Shadow");
		GameObject    model  = GameObject.Instantiate(modelPrefab) as GameObject;
		Character     chr    = model.AddComponent<Character>();
		BoxCollider2D box    = model.GetComponent<BoxCollider2D>();
		GameObject    shadow = GameObject.Instantiate(shadowPrefab) as GameObject;
		{
			Canvas        canvas    = shadow.GetComponent<Canvas>();
			RectTransform transform = shadow.GetComponent<RectTransform>();
			transform.SetParent(model.transform, false);
			transform.sizeDelta = box.size;
			canvas.overrideSorting = true;
		}

		chr.SetInfo(this);
		return chr;
	}
}
   