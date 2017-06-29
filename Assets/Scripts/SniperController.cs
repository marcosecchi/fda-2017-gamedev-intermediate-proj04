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
			}
		}
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
		if (target == null)
			return;
		// Disegno un raggio dagli occhi dello sniper al bersaglio selezionato

		Gizmos.color = new Color(1f, 1f, 1f, .5f);
		Gizmos.DrawRay(sniperEyes.position, (_currentTargetPoint.position - sniperEyes.position) );

		if (_targetInLOS)
			Gizmos.color = Color.red;
		else
			Gizmos.color = new Color (1f, 0, 0, .4f);
		
		float distance = data.weaponRange / Vector3.Distance (sniperEyes.position, _currentTargetPoint.position);
		Gizmos.DrawRay(sniperEyes.position, (_currentTargetPoint.position - sniperEyes.position) * distance );


//		float distance = 
		// Disegno un raggio dagli occhi dello sniper in avanti
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay (sniperEyes.position, sniperEyes.transform.forward);
	}
}
