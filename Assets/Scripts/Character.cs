using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// public field
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void Idle()
	{
		ChangeState(State.Idle);
	}

	public void Move(float radian)
	{
		if (ChangeState(State.Move))
		{
			Vector2 vec2 = Vector2.zero;
			vec2.x = Mathf.Cos(radian);
			vec2.y = Mathf.Sin(radian);
			m_rigid.velocity = vec2 * m_speed;
		}
	}
	
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// private field
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	private State      m_state  = State.Idle;
	private float      m_speed  = 2.0f;
	private float      m_damage = 0.0f;
	private Rigidbody  m_rigid  = null;
	//private enum MoveType { None, MoveTo, MoveBy, }
	//private MoveType m_moveType = None;
	
	private enum State
	{
		Idle,
		Move,
		Attack,
		Defence,
	}

	private void Awake()
	{
		m_rigid = GetComponent<Rigidbody>();
	}

	private bool ChangeState(State state)
	{
		bool ret = TransitState(m_state, state);
		if (ret) m_state = state;
		return ret;
	}
	
	private void FixedUpdate()
	{
		UpdateState(m_state);
	}

	private bool TransitState(State oldState, State newState)
	{
		switch (oldState)
		{
		case State.Move:
		{
			m_rigid.velocity = Vector2.zero;
		}
			break;
		}
		return true;
	}

	private void UpdateState(State state)
	{
		switch (state)
		{
		case State.Move:
		{
			Vector3 pos = transform.position;
			pos.z = pos.y;
			transform.position = pos;
		} 
			break;
		}
	}
}
