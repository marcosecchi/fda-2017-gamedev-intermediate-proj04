using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Esempio di game object che cerca di raggiungere un bersaglio
// utilizzando una NavMesh
[RequireComponent(typeof(NavMeshAgent))]
public class MoveAI : MonoBehaviour {

	// Il bersaglio da raggiungere
	public Transform target;

	// Il componente NavMeshAgent
	private NavMeshAgent _agent;

	void Start () {
		// Se non è stato settato un bersaglio, cerco un elemento taggato
		// "Player" in scena
		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		// Recupero il componente NavMeshAgent
		_agent = GetComponent<NavMeshAgent> ();

	}
	
	void Update () {
		// Se non è stato settato un bersaglio, non faccio nulla
		if (target == null)
			return;

		// Definisco la destinazione del mesh agent.
		_agent.destination = target.position;
				
	}
}
