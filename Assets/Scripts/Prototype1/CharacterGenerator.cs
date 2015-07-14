using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class CharacterGenerator : MonoBehaviour
{

	//! public members
	public GameObject m_characterPrefab = null;

	//! private members
	private uint m_charCount = 0;

	// Use this for initialization
	private void Start ()
	{
		StartCoroutine(GenCharacter(5,1));
	}

	private void OnCharArrive(Character character)
	{
		Ground    ground = PlayScene.MainGround;
		Transform first = ground.transform.GetChild(0);
		Transform last  = ground.transform.GetChild(ground.transform.childCount-1);
		float     posX  = Random.Range(first.position.x, last.position.x);
		float     posZ  = Random.Range(0.0f, 15.0f);
		float     speed = Random.Range(2.0f, 5.0f);

		character.SetDestination(new Vector3(posX, 0, posZ), speed);
	}

	private void OnCharHit(Character character)
	{
		GameObject.Destroy(character.gameObject);
		if (--m_charCount == 0)
		{
			StartCoroutine(GenCharacter(5,1));
		}
	}

	private IEnumerator GenCharacter(uint count, float interval)
	{
		m_charCount += count;
		while (count-- > 0)
		{
			GameObject instance  = GameObject.Instantiate(m_characterPrefab) as GameObject;
			Character  character = instance.GetComponent<Character>();
			
			character.transform.SetParent(transform, false);
			character.SetCallback(OnCharArrive, OnCharHit);
			OnCharArrive(character);
			yield return new WaitForSeconds(interval);
		}
	}
}
