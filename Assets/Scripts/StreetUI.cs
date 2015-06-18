using UnityEngine;
using System.Collections;

public class StreetUI : MonoBehaviour {


	private float m_lastTouchX = 0;
	public float m_accelSlide = 0.5f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnBeginDrag()
	{
		m_lastTouchX = Input.mousePosition.x;
	}

	public void OnDrag()
	{
		float newTouchX = Input.mousePosition.x;
		float slideDelta = newTouchX - m_lastTouchX;
		m_lastTouchX = newTouchX;

		GameObject cameraGO = GameObject.Find("Street/Camera");
		cameraGO.transform.position -= Vector3.right * slideDelta;

		Debug.Log(slideDelta);
	}

}
