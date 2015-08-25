using UnityEngine;
using System.Collections;

public class EnemyMob : MonoBehaviour
{
	/// public methods
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void Redirect()
	{
		m_character.Move(Random.value * Mathf.PI * 2);
		CancelInvoke("Redirect");
		Invoke("Redirect", Random.Range(1.0f, 3.0f));
	}

	/// private field
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	[SerializeField] private Character m_character = null;
	private State     m_state = State.Idle;
	private enum State
	{
		Idle,
		Runaway,
		Damaged,
		Died,
	}

	private void TransitState(EnemyMob.State state)
	{
		switch (m_state)
		{
		case State.Damaged:
		case State.Runaway: if (m_state == state) return; break;
		}

		m_state = state;

		switch (m_state)
		{
		case State.Runaway:
			Redirect();
			break;
		case State.Died:
		{
			m_character.Stop();
			GameObject.Destroy(m_character.gameObject);
		} break;
		}
	}

	private void OnDamaged(uint amount)
	{
		if (m_character.ApplyDamage(amount) == 0) TransitState(State.Died);
		else TransitState(State.Damaged);
	}

	private void OnHitBoxRadar(GameObject go)
	{
		UserMob userMob = go.GetComponentInChildren<UserMob>();
		if (userMob != null) TransitState(State.Runaway);
	}
}
