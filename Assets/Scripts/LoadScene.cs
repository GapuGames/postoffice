using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScene : MonoBehaviour
{
	public Text   m_text;
	public string m_sceneName;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(LoadAsycScene());
	}

	private IEnumerator LoadAsycScene()
	{
		AsyncOperation ao = Application.LoadLevelAsync(m_sceneName);
		while (!ao.isDone)
		{
			m_text.text = ao.progress.ToString("P");
			yield return ao;
		}
	}

}
