using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SentinelAIData", menuName = "FDA/Create Sentinel Data", order = 2)]
public class SentinelAIScriptableObject : ScriptableObject {

	public float rotationSpeed = .02f;

	public float targetAcquireRatio = .5f;

	public float toIdleMultiplier = 1.0f;

	public float toActiveMultiplier = 1.0f;

	public float toAlertMultiplier = 1.5f;
}
