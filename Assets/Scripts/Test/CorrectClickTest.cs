using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CorrectClickTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBeginDrag()
	{
		Debug.Log("OnBeginDrag");
	}

	public void OnPointerClick(BaseEventData bed)
	{
		PointerEventData ped = bed as PointerEventData;
		Debug.Log("ped.delta: " + ped.delta);
		Debug.Log("ped.dragging: " + ped.dragging);
		Debug.Log("ped.scrollDelta: " + ped.scrollDelta);
		Debug.Log("OnPointerClick");
	}

	public void OnClick()
	{
		Debug.Log("OnClick");
	}
}
