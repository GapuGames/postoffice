using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	//! public, protected or everything can be used outside of this

	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private void Start ()
	{
	
	}
	
	// Update is called once per frame
	private void Update ()
	{
	
	}

	public void OnClick()
	{
		Debug.Log("test");
	}
	#endregion
}
