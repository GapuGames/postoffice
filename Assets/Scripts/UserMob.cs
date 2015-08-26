using UnityEngine;
using System.Collections;

public class UserMob : MonoBehaviour
{
	//[SerializeField] private ControlPad2 m_controller = null;
	[SerializeField] private ControlPad m_controlPad = null;
	[SerializeField] private Weapon     m_weapon     = null;
	[SerializeField] private Character  m_character  = null;

	private void Start ()
	{
		m_weapon.Deactive();
		m_controlPad.SetControlCallback(OnMoving, OnAction);
	}

	private void OnMoving(Vector2 direction, float amount)
	{
		if (amount > 0) m_character.Move(Mathf.Atan2(direction.y, direction.x));
		else m_character.Stop();
	}

	private void OnAction()
	{
		m_weapon.Use();
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
		m_weapon.Use();
	}
}
