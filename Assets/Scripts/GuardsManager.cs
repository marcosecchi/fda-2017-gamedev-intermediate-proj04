using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardsManager : MonoBehaviour {

	public GameObject guardPrefab;

	public Transform[] spawnPoints;

	private void OnEnable () {
		EventManager.OnInfantryAlerted += SpawnInfantry;
	}

	private void SpawnInfantry() {
		GameObject go = Instantiate (guardPrefab);
		go.transform.position = spawnPoints [Random.Range (0, spawnPoints.Length)].position;

	}

	private void OnDisable() {
		EventManager.OnInfantryAlerted -= SpawnInfantry;
	}
	
}
