using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SentinelAIData", menuName = "FDA/Create Sentinel AI Data", order = 1)]
public class SentinelCameraAIScriptableObject : ScriptableObject {

	public float rotationSpeed = 10f;

	public float toIdleMultiplier = 1f;

	public float toActiveMultiplier = 1f;

	public float toAlertMultiplier = 1.5f;

	public float alertGuardsLevel = 10f;

}
