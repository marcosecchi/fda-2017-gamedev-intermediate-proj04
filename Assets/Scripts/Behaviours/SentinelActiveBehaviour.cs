using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behaviour del sentinel in stato "Active"
public class SentinelActiveBehaviour : SentinelBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Active");
		// Non alla ricerca del target
		animator.SetBool (G.SEEKING, true);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		float newVal = 0;
		if (animator.GetBool (G.TARGET_IN_LOS)) {
			newVal = animator.GetFloat (G.ALERT_LEVEL) + Time.deltaTime * Data.upToAlertMultiplier;
		} else {
			newVal = animator.GetFloat (G.ALERT_LEVEL) - Time.deltaTime * Data.downToIdleMultiplier;
		}
		animator.SetFloat (G.ALERT_LEVEL, newVal);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
