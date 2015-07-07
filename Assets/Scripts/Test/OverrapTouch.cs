using UnityEngine;
using System.Collections;

public class OverrapTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDrag()
	{
		Debug.Log("OnDrag");
	}

	public void OnPointerEnter()
	{
		Debug.Log("OnPointerEnter");
	}
}
