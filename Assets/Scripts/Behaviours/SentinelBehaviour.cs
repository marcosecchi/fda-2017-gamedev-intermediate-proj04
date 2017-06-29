using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behaviour che permette di immagazzinare i dati per il
// comportamento del sentinel
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
