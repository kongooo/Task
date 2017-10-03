using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDoor : MonoBehaviour {

	void Update ()
	{
	    if (!GameObject.FindGameObjectWithTag("Enemy"))
	        GetComponent<BoxCollider>().isTrigger = true;
	}
}
