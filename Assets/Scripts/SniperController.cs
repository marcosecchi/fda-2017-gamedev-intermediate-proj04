using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Il controller dello sniper
public class SniperController : MonoBehaviour {

	public SniperAIScriptableObject data;

	// Il bersaglio dello sniper
	public Transform target;

	// Le posizione degli occhi del cecchino
	public Transform sniperEyes;

	// L'elenco dei possibili bersagli sul personaggio
	private List<Transform> _targetPoints;

	// Il bersaglio selezionato
	private Transform _currentTargetPoint;

	// Indica se il bersaglio corrente è in linea di vista
	bool _targetInLOS = false;

	// Inizializza il bersaglio
	void Start () {
		// Se non è stato definito nessun bersaglio, vado a cercare un oggetto taggato
		// "Player" in scena
		if (target == null)
			target = GameObject.FindGameObjectWithTag ("Player").transform;

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

		// Acquisisco un bersaglio
		AcquireTargetPoint ();
	}


	void Update () {
		RaycastHit hit;
		_targetInLOS = false;
		// Traccio un raycast dagli occhi del cecchino, in direzione del bersaglio selezionato
		bool rayCast = Physics.Raycast (sniperEyes.position, _currentTargetPoint.position - sniperEyes.position, out hit, data.weaponRange);
		if (rayCast) {
			// Se l'elemento colpito è taggato "Player", setto a "true"
			// la flag
			if (hit.transform.gameObject.tag == "Player") {
				Debug.Log ("Target Acquired");
				_targetInLOS = true;

//				float losAngle = Vector3.Angle (sniperEyes.forward, _currentTargetPoint.position - sniperEyes.position);
//				if (losAngle > data.losAngle)
//					_targetInLOS = false;
//				Debug.Log (losAngle + " - " + data.losAngle);
//				Debug.Log (">>>" + (losAngle - data.losAngle));
			}
		}

		float distance = Vector3.Distance (sniperEyes.position, _currentTargetPoint.position);
		if (distance > data.weaponRange)
			_targetInLOS = false;


		// Se non è stato possibile raggiungere il bersaglio,
		// lo cambio
		if (!_targetInLOS)
			AcquireTargetPoint ();

	}

	// Definisce casualmente un bersaglio dalla lista
	void AcquireTargetPoint() {
		_currentTargetPoint = _targetPoints[Random.Range(0, _targetPoints.Count)];
	}

	void OnDrawGizmos() {
		if (_currentTargetPoint == null || sniperEyes == null)
			return;

		// Disegno un raggio dagli occhi dello sniper al bersaglio da acquisire
		Gizmos.color = new Color(1f, 1f, 1f, .5f);
		Vector3 direction = _currentTargetPoint.position - sniperEyes.position;
		Gizmos.DrawRay(sniperEyes.position, direction);

		// Disegno la distanza massima dell'arma dello sniper
		float distance = data.weaponRange / Vector3.Distance (sniperEyes.position, _currentTargetPoint.position);
		if (!_targetInLOS)
			Gizmos.color = new Color(1f, 0, 0, .4f);
		else
			Gizmos.color = Color.red;
		Gizmos.DrawRay(sniperEyes.position, direction * distance);


		// Disegno un raggio dagli occhi dello sniper in avanti
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay (sniperEyes.position, sniperEyes.forward);
	}
}
