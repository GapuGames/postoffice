using UnityEngine;
using System.Collections;

public class StreetUI : MonoBehaviour {


	private float m_lastTouchX = 0;
	public float m_accelSlide = 0.5f;
	public Transform m_cameraTF = null;

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
		if (m_cameraTF == null) return;

		float newTouchX = Input.mousePosition.x;
		float slideDelta = newTouchX - m_lastTouchX;
		m_lastTouchX = newTouchX;

		m_cameraTF.position -= Vector3.right * (slideDelta * m_accelSlide);
	}

}
