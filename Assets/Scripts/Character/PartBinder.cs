using UnityEngine;
using UnityEditor;
using System.Collections;

public class PartBinder : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	//public GameObject 
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private void Start ()
	{
	
	}
	
	// Update is called once per frame
	private void Update ()
	{
	
	}
	#endregion
}

[ExecuteInEditMode]
public class PartBinderEditor : Editor
{

}