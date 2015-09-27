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
		GameObject model = GameObject.Instantiate(modelPrefab) as GameObject;
		Character chr = model.AddComponent<Character>();
		chr.SetInfo(this);
		return chr;
	}
}
   