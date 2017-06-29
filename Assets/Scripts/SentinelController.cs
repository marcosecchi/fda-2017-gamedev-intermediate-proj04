using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Animator))]
public class SentinelController : MonoBehaviour {

	public Transform target;

	Collider _targetCollider;

	Camera _camera;

	Plane[] _planes;

	Animator _animator;

	bool _targetInLOS;

	public SentinelCameraAIScriptableObject data;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();

		_camera = GetComponent<Camera> ();
		_planes = GeometryUtility.CalculateFrustumPlanes (_camera);

		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		_targetCollider = target.GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		_targetInLOS = GeometryUtility.TestPlanesAABB (_planes, _targetCollider.bounds);
		if (_targetInLOS) {
			_animator.SetBool ("TargetInLOS", true);
			if (_animator.GetBool("Seeking")) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position), Time.deltaTime * data.rotationSpeed);			
			}
		} else {
			_animator.SetBool ("TargetInLOS", false);
		}
	}

	void OnDrawGizmos() {
		if (_camera == null)
			return;
		
		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

		if(!_targetInLOS)
			Gizmos.color = Color.gray;
		else
			Gizmos.color = Color.magenta;

		Gizmos.DrawFrustum (Vector3.zero, _camera.fieldOfView, _camera.nearClipPlane, _camera.farClipPlane, _camera.aspect);
		Gizmos.matrix = temp;
	}
}
