using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectMono : MonoBehaviour {
	//Use to do monobehaviour stuff
	//For scriptable objects
	//Long live the scriptable objects architecture

	public Coroutine RunCoroutine (IEnumerator coroutine) {
		return StartCoroutine (coroutine);
	}

	public void EndCoroutine (Coroutine coroutine) {
		StopCoroutine (coroutine);
	}
}