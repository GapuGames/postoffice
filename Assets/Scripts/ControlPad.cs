using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class ControlPad : MonoBehaviour
{

	[SerializeField] private Transform   m_ball        = null;
	[SerializeField] private float       m_maxDistance = 25.0f;
	[SerializeField] private PadMove     m_padMove     = new PadMove();
	[SerializeField] private ActionDown  m_actionDown  = new ActionDown();
	[SerializeField] private ActionUp    m_actionUp    = new ActionUp();
	private Vector2    m_originPosition;

	public void OnActionDown()
	{
		m_actionDown.Invoke();
	}
	
	public void OnActionUp()
	{
		m_actionUp.Invoke();
	}

	public void SetControlCallback(UnityAction<Vector2, float> movingCallback, UnityAction actionDown, UnityAction actionUp)
	{
		m_padMove.AddListener(movingCallback);
		m_actionDown.AddListener(actionUp);
		m_actionUp.AddListener(actionUp);
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
		
		if (m_padMove != null) m_padMove.Invoke(normal, length / m_maxDistance);
	}
	
	public void OnEndDrag()
	{
		m_ball.position = m_originPosition;
		if (m_padMove != null) m_padMove.Invoke(Vector2.zero, 0);
	}
	
	private void Start ()
	{
		m_originPosition = m_ball.position;
	}
	
	[Serializable] public class PadMove    : UnityEvent<Vector2, float> {};
	[Serializable] public class ActionUp   : UnityEvent {};
	[Serializable] public class ActionDown : UnityEvent {};
}
