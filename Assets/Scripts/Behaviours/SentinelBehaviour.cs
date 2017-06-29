using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelBehaviour : StateMachineBehaviour {

	protected SentinelCameraAIScriptableObject _d;

	public SentinelCameraAIScriptableObject Data {
		get {
			return _d;
		}
		set { 
			_d = value;
		}
	}

}
