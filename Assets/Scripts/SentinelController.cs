using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Animator))]
public class SentinelController : MonoBehaviour {

	// Il bersaglio del sentinel
	public Transform target;

	// Il collider del bersaglio, per capire se si trova all'interno del frustum
	Collider _targetCollider;

	// I dati di configurazione del sentinel
	public SentinelAIScriptableObject data;

	// La camera associata a questo sentinel
	Camera _camera;

	// I piani del frustum della camera
	Plane[] _planes;

	// Indica se il bersaglio è visibile dalla camera
	bool _targetInLOS;

	// L'animator utilizzato come macchina a stati finiti della AI
	Animator _animator;

	void Start () {
		// Recupera la camera
		_camera = GetComponent<Camera> ();
		// Recupera l'animator
		_animator = GetComponent<Animator> ();

		// Recupera la lista di behaviour di tipo "SentinelBehaviour" dall'animator...
		SentinelBehaviour[] behaviours = _animator.GetBehaviours<SentinelBehaviour> ();
		foreach (SentinelBehaviour behaviour in behaviours) {
			// ...e gli assegna i dati dello scriptable object
			behaviour.Data = data;
		}

		// Se il bersaglio non è stato assegnato, lo cerca in scena
		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;

		// Recupera il collider del bersaglio
		_targetCollider = target.GetComponent<Collider> ();
	}
	
	void Update () {
		// Calcola i piani del frustum della camera
		_planes = GeometryUtility.CalculateFrustumPlanes (_camera);

		// Controlla che il bersaglio sia all'interno del frustum (cioè renderizzato dalla camera)
		_targetInLOS = GeometryUtility.TestPlanesAABB (_planes, _targetCollider.bounds);
		if (_targetInLOS) {
			RaycastHit hit;
			bool rayCast = Physics.Raycast (transform.position, target.position - transform.position, out hit, _camera.farClipPlane * 2);
			if (rayCast) {
				// Controllo che l'elemento colpito sia taggato "Player"
				Debug.Log(hit.transform.gameObject);
				_targetInLOS = hit.transform.gameObject.tag == "Player";
			}
		}

		// Se il target è in linea di vista,
		// muove la camera per centrarlo su di esso
		if (_targetInLOS) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), Time.deltaTime * data.rotationSpeed);
			Debug.Log ("InLOS");
		}
			
	}

	void OnDrawGizmos() {
		if (_camera == null)
			return;

		// disegna il frustum della camera, in modo che sia sempre visibile e colorato a seconda che
		// il bersaglio sia visibile o no
		Matrix4x4 temp = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS (transform.position, transform.rotation, Vector3.one);
			
		if (!_targetInLOS)
			Gizmos.color = Color.gray;
		else
			Gizmos.color = Color.magenta;

		Gizmos.DrawFrustum (Vector3.zero, _camera.fieldOfView, _camera.farClipPlane, _camera.nearClipPlane, _camera.aspect);
		Gizmos.matrix = temp;

		if (_targetInLOS) {
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, (target.position - transform.position) );
		}

	}
}
