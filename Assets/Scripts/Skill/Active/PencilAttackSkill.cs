using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PencilAttackSkill : ActiveSkill
{

	//! public, protected or everything can be used outside of this
	public override void Activate(Character caster)
	{
		transform.SetParent(caster.transform, false);
		m_area.enabled = true;
	}

	public override void Deactivate()
	{
	}

	public override bool IsActivated()
	{
		return false;
	}

	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private BoxCollider2D m_area = null;
	private void Awake()
	{
		m_area = GetComponent<BoxCollider2D>();
		m_area.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Character victim = other.GetComponent
	}

	void OnTriggerExit2D(Collider2D other)
	{
	}

	#endregion
}
