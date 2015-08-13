using UnityEngine;
using System.Collections;

public class UserMob : MonoBehaviour
{
	[SerializeField]
	private Controller m_controller = null;
	private Character  m_character  = null;
	// Use this for initialization
	void Start ()
	{
		m_character = GetComponent<Character>();
		m_controller.m_beginAct  = BeginDrag;
		m_controller.m_endAct    = EndDrag;
		m_controller.m_dragAct   = Drag;
	}

	private void BeginDrag()
	{
	}

	private void EndDrag()
	{
		m_character.Idle();
	}

	private void Drag(float radian)
	{
		m_character.Move(radian);
	}
}
