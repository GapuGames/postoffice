using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class ControlPad : MonoBehaviour
{
	public PadMoveEvent     PadMove     = new PadMoveEvent();
	public ActionDownEvent  ActionDown  = new ActionDownEvent();
	public ActionUpEvent    ActionUp    = new ActionUpEvent();

	//! private members, callbacks or anythings that you don't need to worry about
	#region
	[Serializable] public class PadMoveEvent    : UnityEvent<Vector2, float> {};
	[Serializable] public class ActionUpEvent   : UnityEvent {};
	[Serializable] public class ActionDownEvent : UnityEvent {};
	
	[SerializeField] private Transform   m_ball        = null;
	[SerializeField] private float       m_maxDistance = 25.0f;
	private Vector2    m_originPosition;

	public void OnActionDown()
	{
		ActionDown.Invoke();
	}
	
	public void OnActionUp()
	{
		ActionUp.Invoke();
	}
	
	public void OnDrag(BaseEventData bed)
	{
		PointerEventData ped = bed as PointerEventData;
		if (!ped.dragging) return;
		if (!ped.IsPointerMoving()) return;
		
		Vector2 delta    = ped.position - m_originPosition;
		Vector2 normal   = delta.normalized;
		float   distance = delta.magnitude;
		float   length   = (distance > m_maxDistance)? m_maxDistance : distance;
		
		m_ball.position  = m_originPosition + (normal * length);
		
		if (PadMove != null) PadMove.Invoke(normal, length / m_maxDistance);
	}
	
	public void OnEndDrag()
	{
		m_ball.position = m_originPosition;
		if (PadMove != null) PadMove.Invoke(Vector2.zero, 0);
	}
	
	private void Start ()
	{
		m_originPosition = m_ball.position;
	}

#if UNITY_EDITOR
	private void FixedUpdate()
	{
		bool arrowKey = false;
		Vector2 direction = Vector2.zero;
		if (Input.GetKeyDown(KeyCode.W)) {arrowKey |= true; direction += Vector2.up;}
		if (Input.GetKeyDown(KeyCode.S)) {arrowKey |= true; direction += Vector2.down;}
		if (Input.GetKeyDown(KeyCode.A)) {arrowKey |= true; direction += Vector2.left;}
		if (Input.GetKeyDown(KeyCode.D)) {arrowKey |= true; direction += Vector2.right;}

		if (arrowKey) PadMove.Invoke(direction.normalized, arrowKey? 1:0);
		if (Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.S))
		{
			PadMove.Invoke(Vector2.zero, 0);
		}
		if (Input.GetKeyDown(KeyCode.Space)) OnActionDown();
		if (Input.GetKeyUp(KeyCode.Space))   OnActionUp();
	}
#endif

	#endregion
}
