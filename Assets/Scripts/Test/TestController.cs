using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour
{
	public Character  m_character;

	void OnPadMove(Vector2 direction, float power)
	{
		m_character.MoveBy(direction, power*5.0f);
	}
	
}
