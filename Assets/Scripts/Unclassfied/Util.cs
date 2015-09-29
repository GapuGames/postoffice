using UnityEngine;
using System.Collections;

public static class Util
{
	private static char[] delimiter = new char[]{'(', ')', ','};
	public static Vector3 ParseVector3(string str)
	{
		Vector3 v = Vector3.zero;
		string[] splited = str.Split(delimiter);
		v.x = float.Parse(splited[1]);
		v.y = float.Parse(splited[2]);
		v.z = float.Parse(splited[3]);
		return v;
	}

	public static string GetPath(this Component com, string name = "")
	{
		name = string.IsNullOrEmpty(name)? com.name : string.Format("{0}/{1}", com.name, name);
		Transform parent = com.transform.parent;
		if (parent != null) return parent.GetPath( name );
		return name;
	}

	public static T FindComponent<T>(this Transform trans, string path) where T : class
	{
		Transform ret = trans.Find(path);
		return (ret != null)? ret.GetComponent<T>() : null;
	}

	public static void DestroyThis(this GameObject go)
	{
		GameObject.Destroy(go);
	}
}
