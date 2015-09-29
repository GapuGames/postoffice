using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class FieldUI : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	public Text       m_timeInfo     = null;
	public Text       m_monsterInfo  = null;
	public GameObject m_resultPrefab = null;
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region

	private int m_playTime = 30; //! in seconds

	private void Start ()
	{
		InvokeRepeating("UpdateTimeInfo", 0, 1);
		InvokeRepeating("UpdateMonsterInfo", 0, 1);
	}
	
	private void UpdateMonsterInfo()
	{
		int count = GameObject.FindGameObjectsWithTag("Monster").Length;
		m_monsterInfo.text = count.ToString();
		if (count == 0) OpenResult();
	}

	private void UpdateTimeInfo ()
	{
		TimeSpan time = TimeSpan.FromSeconds(--m_playTime);
		m_timeInfo.text = string.Format("{0}:{1}", time.Minutes, time.Seconds);
		if (m_playTime <= 0) OpenResult();
	}

	private void OpenResult()
	{
		CancelInvoke();
		GameObject result = GameObject.Instantiate(m_resultPrefab) as GameObject;
		Text timeInfo    = result.transform.FindComponent<Text>("Panel/TimeInfo");
		Text monsterInfo = result.transform.FindComponent<Text>("Panel/MonsterInfo");
		timeInfo.text    = m_timeInfo.text;
		monsterInfo.text = m_monsterInfo.text;
	}
	#endregion
}
