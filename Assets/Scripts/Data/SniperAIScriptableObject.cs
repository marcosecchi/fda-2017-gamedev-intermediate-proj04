using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperAIData", menuName="FDA/Create Sniper Data", order = 1)]
public class SniperAIScriptableObject : ScriptableObject {

	// Il raggio di azione dell'arma dello sniper
	public float weaponRange = 30f;

}
