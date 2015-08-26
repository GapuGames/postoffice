using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ControlPad : MonoBehaviour
{

	[SerializeField] 
	private ControlBall m_ctrlBall       = null;
	private UnityAction m_actionCallback = null;

	public void OnActionClicked()
	{
		if (m_actionCallback != null) m_actionCallback();
	}

	public void SetControlCallback(UnityAction<Vector2, float> movingCallback, UnityAction actionCallback)
	{
		if (m_ctrlBall != null) m_ctrlBall.m_movingCallback = movingCallback;
		m_actionCallback = actionCallback;
	}
}
