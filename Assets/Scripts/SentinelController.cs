using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SentinelController : MonoBehaviour {

	public SentinelAIScriptableObject data;

	Camera _camera;

	Plane[] _planes;

	public Transform target;

	Collider _targetCollider;

	bool _targetInLOS;

	void Start () {
		_camera = GetComponent<Camera> ();
		_planes = GeometryUtility.CalculateFrustumPlanes (_camera);

		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;

		_targetCollider = target.GetComponent<Collider> ();
	}
	
	void Update () {
		_targetInLOS = GeometryUtility.TestPlanesAABB (_planes, _targetCollider.bounds);

		if (_targetInLOS) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), Time.deltaTime);
			Debug.Log ("InLOS");
		}
			
	}
}
