using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ControlPad : MonoBehaviour
{

	[SerializeField] 
	private MovePanel   m_movePanel  = null;
	private UnityAction m_actionDown = null;
	private UnityAction m_actionUp   = null;

	public void OnActionDown()
	{
		if (m_actionDown != null) m_actionDown();
	}
	
	public void OnActionUp()
	{
		if (m_actionUp != null) m_actionUp();
	}

	public void SetControlCallback(UnityAction<Vector2, float> movingCallback, UnityAction actionDown, UnityAction actionUp)
	{
		if (m_movePanel != null) m_movePanel.m_movingCallback = movingCallback;
		m_actionDown = actionDown;
		m_actionUp   = actionUp;
	}
}
