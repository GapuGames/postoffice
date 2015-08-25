using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneChangeTest : MonoBehaviour
{
	public Text m_text = null;
	public void OnButtonClicked()
	{
		StartCoroutine(LoadField());
	}

	private IEnumerator LoadField()
	{
		AsyncOperation ao = Application.LoadLevelAsync("Field");
		while (!ao.isDone)
		{
			m_text.text = ao.progress.ToString();
			Debug.Log(m_text.text);
			yield return ao;
		}
	}

}
