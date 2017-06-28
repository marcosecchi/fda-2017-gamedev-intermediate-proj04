using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperController : MonoBehaviour {

	public Transform target;

	public Transform sniperEyes;

	private List<Transform> _targets;

	private Transform _currentTarget;

	bool _targetAcquired = false;

	// Use this for initialization
	void Start () {
		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		_targets = new List<Transform> ();
		Transform[] list = target.GetComponentsInChildren<Transform> ();
		foreach (Transform t in list) {
			if (t.gameObject.tag == "PlayerTarget")
				_targets.Add (t);
		}
		if (_targets.Count == 0)
			_targets.Add (target);

		AcquireTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		_targetAcquired = false;
		if (
			Physics.Raycast (
				sniperEyes.position,
				_currentTarget.position - sniperEyes.position,
				out hit,
				30f)) {
			if (hit.transform.gameObject.tag == "Player") {
				Debug.Log ("Target Acquired");
				_targetAcquired = true;
			}
		}
		if (!_targetAcquired)
			AcquireTarget ();

	}

	void AcquireTarget() {
		_currentTarget = _targets[Random.Range(0, _targets.Count)];
	}

	void OnDrawGizmos() {
		if (target == null)
			return;
		
		Gizmos.color = Color.red;
		Gizmos.DrawRay(sniperEyes.position, _currentTarget.position - sniperEyes.position);
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay (sniperEyes.position, transform.forward);
	}
}
