using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class Character : MonoBehaviour {


	//! public members
	public float m_speed = 1.0f;
	public UnityAction<Character> m_arriveAction = null;
	public UnityAction<Character> m_hitAction = null;

	//! private members

	//! public methods
	public void SetCallback(UnityAction<Character> arriveAction, UnityAction<Character> hitAction)
	{
		m_arriveAction = arriveAction;
		m_hitAction    = hitAction;
	}

	public void SetDestination(Vector3 destination, float speed)
	{
		m_speed = speed;
		Vector3   distance = destination - transform.position;
		Rigidbody rigid    = GetComponent<Rigidbody>();
		float     time     = distance.magnitude / m_speed;
		rigid.velocity     = distance / time;
		Vector3 localScale = transform.localScale;
		localScale.x = (rigid.velocity.x >= 0)? 1:-1;
		transform.localScale = localScale;

		CancelInvoke("OnArrive");
		Invoke("OnArrive", time);
	}

	//! private methods
	private void OnArrive()
	{
		Rigidbody rigid    = GetComponent<Rigidbody>();
		rigid.velocity = Vector3.zero;
		if (m_arriveAction != null) m_arriveAction(this);
	}

	private void OnHit()
	{
		if (m_hitAction != null) m_hitAction(this);
	}

	private void Awake()
	{
	}

	// Use this for initialization
	private void Start ()
	{
	}
	
	// Update is called once per frame
	private void Update ()
	{
	
	}
}
