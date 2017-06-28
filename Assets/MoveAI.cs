using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAI : MonoBehaviour {

	public Transform target;
	private NavMeshAgent _agent;

	void Start () {
		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		_agent = GetComponent<NavMeshAgent> ();

	}
	
	void Update () {
		_agent.destination = target.position;
				
	}
}
