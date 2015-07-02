using UnityEngine;
using System.Collections;

public class WorldTouchTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnHit()
	{
		Debug.Log(gameObject.name + "! it's clicked1!");
		GameUI.Notice("building!", "you just activated my building!");
	}
}
