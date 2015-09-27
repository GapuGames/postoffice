using UnityEngine;
using System.Collections;

public abstract class ActiveSkill : MonoBehaviour
{
	//! public, protected or everything can be used outside of this

	public abstract void Activate(Character caster);
	public abstract void Deactivate();
	public abstract bool IsActivated();
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	#endregion
}
