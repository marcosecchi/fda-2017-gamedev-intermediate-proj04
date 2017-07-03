using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {

	public delegate void AlertGuardAction();
	public static AlertGuardAction OnInfantryAlerted;
	public static AlertGuardAction OnSniperAlerted;
}
