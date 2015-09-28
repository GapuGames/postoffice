using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReturnCounter : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private int m_count = 3;
	private Text m_text = null;
	private void Start ()
	{
		m_text = GetComponentInChildren<Text>();
		StartCoroutine(CheckReturn());
	}

	private IEnumerator CheckReturn()
	{
		WaitForSeconds waitASecond = new WaitForSeconds(1);

		while (GameObject.FindGameObjectsWithTag("Monster").Length != 0) yield return waitASecond;

		m_text.text = "마을로 이동합니다.";

		yield return waitASecond;

		while (m_count >= 0)
		{
			m_text.text = m_count.ToString();

			yield return waitASecond;

			m_count -= 1;
		}

		Application.LoadLevel("Village");
	}
	#endregion
}
