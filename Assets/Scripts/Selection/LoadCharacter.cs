using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
	public GameObject[] characterPrefabs;
	public Transform spawnPoint;
	private GameObject clone;
	//public TMP_Text label;

	void Start()
	{
		int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
		GameObject prefab = characterPrefabs[selectedCharacter];
		clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
		//label.text = prefab.name;
	}
}
