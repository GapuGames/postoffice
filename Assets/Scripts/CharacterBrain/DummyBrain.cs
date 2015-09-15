using UnityEngine;
using System.Collections;

public class DummyBrain : CharacterBrain
{
	//! public, protected or everything can be used outside of this

	public override void Born()
	{
		Debug.Log("더미 태어 났쪄여!");
	}
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	#endregion
}
