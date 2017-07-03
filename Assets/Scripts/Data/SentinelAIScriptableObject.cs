using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SentinelAIData", menuName = "FDA/Create Sentinel Data", order = 2)]
public class SentinelAIScriptableObject : ScriptableObject {

	// Velocità di rotazione della camera
	public float rotationSpeed = .02f;

	// Indica se utilizzare anche i raycast sui target points
	public bool useTargetPoints = true;

	// Percentuale di target points necessari per acquisire il bersaglio
	public float targetPointsAcquireRatio = .5f;

	public GuardType guardType;

	// BEHAVIOURS

	// Modificatore al tempo per arrivare allo statio 'Idle'
	public float downToIdleMultiplier = 1.0f;

	// Modificatore al tempo per arrivare allo statio 'Active'
	public float upToActiveMultiplier = 1.0f;

	// Modificatore al tempo per arrivare allo statio 'Alert'
	public float upToAlertMultiplier = 1.5f;
}

public enum GuardType {
	Infantry,
	Sniper
}
