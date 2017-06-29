using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Animator))]
public class SentinelController : MonoBehaviour {

	public SentinelAIScriptableObject data;

	Camera _camera;

	Plane[] _planes;

	public Transform target;

	Collider _targetCollider;

	bool _targetInLOS;

	Animator _animator;

	void Start () {
		_camera = GetComponent<Camera> ();
		_animator = GetComponent<Animator> ();
		SentinelBehaviour[] behaviours = _animator.GetBehaviours<SentinelBehaviour> ();
		foreach (SentinelBehaviour behaviour in behaviours) {
			behaviour.Data = data;
		}

		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;

		_targetCollider = target.GetComponent<Collider> ();
	}
	
	void Update () {
		_planes = GeometryUtility.CalculateFrustumPlanes (_camera);
		_targetInLOS = GeometryUtility.TestPlanesAABB (_planes, _targetCollider.bounds);

		if (_targetInLOS) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), Time.deltaTime * data.rotationSpeed);
			Debug.Log ("InLOS");
		}
			
	}

	void OnDrawGizmos() {
		if (_camera == null)
			return;

		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS (transform.position, transform.rotation, Vector3.one);
			
		if (!_targetInLOS)
			Gizmos.color = Color.gray;
		else
			Gizmos.color = Color.magenta;

		Gizmos.DrawFrustum (Vector3.zero, _camera.fieldOfView, _camera.farClipPlane, _camera.nearClipPlane, _camera.aspect);

		Gizmos.matrix = temp;
	}
}
