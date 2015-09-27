using UnityEngine;
using System.Collections;

public abstract class CharacterBrain : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	public Character character { get; set; }
	public abstract void Born();
	public virtual void CharacterEnter(Character target) {}
	public virtual void CharacterStay(Character target) {}
	public virtual void CharacterLeave(Character target) {}

	//! private, callback or anything don’t be considered to be used outside of this
	#region
	#endregion
}
