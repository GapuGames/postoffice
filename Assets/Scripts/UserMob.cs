using UnityEngine;
using System.Collections;

public class UserMob : MonoBehaviour
{
	[SerializeField] private Controller m_controller = null;
	[SerializeField] private Weapon     m_weapon     = null;
	private Character  m_character  = null;

	private void Awake()
	{
		m_character = GetComponent<Character>();
	}

	private void Start ()
	{
		m_weapon.Deactive();
		m_controller.m_beginAct  = BeginDrag;
		m_controller.m_endAct    = EndDrag;
		m_controller.m_dragAct   = Drag;
		m_controller.m_clickAct  = Attack;
	}

	private void BeginDrag()
	{
	}

	private void EndDrag()
	{
		m_character.Stop();
	}

	private void Drag(float radian)
	{
		m_character.Move(radian);
	}

	private void Attack()
	{
		m_weapon.Attack();
	}
}
