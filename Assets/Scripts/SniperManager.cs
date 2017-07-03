using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperManager : GuardsManager {

	private void OnEnable () {
		EventManager.OnSniperAlerted += SpawnSniper;
	}

	private void SpawnSniper() {
		GameObject go = Instantiate (guardPrefab);
		go.transform.position = spawnPoints [Random.Range (0, spawnPoints.Length)].position;

	}
	private void OnDisable() {
		EventManager.OnInfantryAlerted -= SpawnSniper;
	}
}
