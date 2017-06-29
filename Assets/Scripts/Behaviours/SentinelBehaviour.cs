using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelBehaviour : StateMachineBehaviour {

	protected SentinelAIScriptableObject _d;

	public SentinelAIScriptableObject Data {
		get { 
			return _d;
		}
		set { 
			_d = value;
		}
	}
}
