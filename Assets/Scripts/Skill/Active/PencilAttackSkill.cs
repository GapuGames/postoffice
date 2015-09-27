using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PencilAttackSkill : ActiveSkill
{
	//! public, protected or everything can be used outside of this
	public override void Activate(Character caster)
	{
		if ((m_caster = caster) != null)
		{
			transform.position = caster.transform.position;
			transform.localScale = new Vector3(caster.Flip? -1 : 1, 1, 1);
		}
		m_area.enabled = true;

		CancelInvoke("Deactivate");
		Invoke("Deactivate", 0.1f);
	}

	public override void Deactivate()
	{
		m_area.enabled = false;
	}

	public override bool IsActivated()
	{
		return false;
	}

	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private BoxCollider2D m_area   = null;
	private Character     m_caster = null;
	private void Awake()
	{
		m_area = GetComponent<BoxCollider2D>();
		m_area.enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Character target = other.GetComponent<Character>();
		if (target != null && m_caster != target) target.ApplyDamage(1);
	}

	void OnTriggerExit2D(Collider2D other)
	{
	}

	#endregion
}
