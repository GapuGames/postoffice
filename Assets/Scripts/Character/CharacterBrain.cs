using UnityEngine;
using System.Collections;

public abstract class CharacterBrain : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	public Character body { get; set; }
	public abstract void Born();

	//! private, callback or anything don’t be considered to be used outside of this
	#region
	#endregion
}
