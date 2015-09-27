using UnityEngine;
using System.Collections;

public class DummyBrain : CharacterBrain
{
	//! public, protected or everything can be used outside of this

	enum State
	{
		Idle,
		Runaway,
		Runaround,
	}

	public override void Born()
	{
		Debug.Log("더미 태어 났쪄여!");
		ForceState(State.Idle);
	}

	public override void CharacterEnter (Character target)
	{
		if (target.tag == "Player") m_target = target;
		ForceState(State.Runaway);
	}

	public override void CharacterLeave (Character target)
	{
		if (m_target == target)
		{
			m_target = null;
			ForceState(State.Idle);
		}
	}

	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private Character m_target = null;
	private State     m_state  = State.Idle;
	private Coroutine m_stateRoutine = null;

	private void ForceState(State newState)
	{
		m_state = newState;
		if (m_stateRoutine != null) StopCoroutine(m_stateRoutine);
		m_stateRoutine = StartCoroutine(StateRoutine());
	}

	private IEnumerator StateRoutine()
	{
		while (true)
		{
			yield return null;
			switch (m_state)
			{
			case State.Idle:
			{
				StopRunning();
				yield return new WaitForSeconds(Random.Range(1.0f,3.0f));
				m_state = State.Runaround;
			} break;
				
			case State.Runaround:
			{
				float angle = Random.value * Mathf.PI;
				Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
				character.MoveBy(direction, 1);
				yield return new WaitForSeconds(Random.Range(1.0f,3.0f));
				m_state = State.Idle;
			} break;
				
			case State.Runaway:
			{
				if (m_target != null)
				{
					Vector3 direction = (character.transform.position - m_target.transform.position).normalized;
					character.MoveBy(direction, 1);
				}
			} break;
			}
		}
	}

	private void StopRunning()
	{
		character.MoveBy(Vector2.zero, 0);
	}
	#endregion
}
