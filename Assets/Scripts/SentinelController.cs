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

	// L'elenco dei possibili bersagli sul personaggio
	private List<Transform> _targetPoints;

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

		// Inizializzo la lista dei bersagli
		_targetPoints = new List<Transform> ();

		// Recupero tutti i gameobjects taggati "PlayerTarget"
		// all'interno del bersaglio
		Transform[] list = target.GetComponentsInChildren<Transform> ();
		foreach (Transform t in list) {
			if (t.gameObject.tag == "PlayerTarget")
				_targetPoints.Add (t);
		}
		// Se non è stato trovato nessun elemento taggato,
		// aggiungo il bersaglio, in modo da averne almeno uno
		if (_targetPoints.Count == 0)
			_targetPoints.Add (target);
		
	}
	
	void Update () {
		// Calcola i piani del frustum della camera
		_planes = GeometryUtility.CalculateFrustumPlanes (_camera);

		// Controlla che il bersaglio sia all'interno del frustum (cioè renderizzato dalla camera)
		_targetInLOS = GeometryUtility.TestPlanesAABB (_planes, _targetCollider.bounds);
		if (_targetInLOS) {
			int targetsCount = 0;
			foreach (Transform tp in _targetPoints) {
				RaycastHit hit;
				bool rayCast = Physics.Raycast (transform.position, tp.position - transform.position, out hit, Mathf.Infinity);
				if (rayCast && hit.transform.gameObject.tag == "Player") {
					targetsCount++;
				}
			}
			Debug.Log (targetsCount);
			if(((float)targetsCount / (float)_targetPoints.Count) < data.targetAcquireRatio)
				_targetInLOS = false;
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

//		if (_targetInLOS) {
//			Gizmos.color = Color.red;
//			Gizmos.DrawRay(transform.position, (target.position - transform.position) );
//		}

		int targetsCount = 0;

		foreach (Transform tp in _targetPoints) {
			Gizmos.color = new Color (1f, 0, 0, .4f);
			RaycastHit hit;
			bool rayCast = Physics.Raycast (transform.position, tp.position - transform.position, out hit, Mathf.Infinity);
			Debug.Log (hit.transform.gameObject);
			if (rayCast && hit.transform.gameObject.tag == "Player") {
				Gizmos.color = Color.red;
				targetsCount++;
			}
			Gizmos.DrawRay(transform.position, (tp.position - transform.position) );
		}

		Debug.Log ("---" + targetsCount);

	}
}
