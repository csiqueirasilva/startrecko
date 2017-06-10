using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroiObjetos : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other)
	{
		// Destroy everything that leaves the trigger
		Destroy(other.gameObject);
	}

}
