using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : MonoBehaviour
{
    private Transform playerTrans;
    private Rigidbody rigidbody;
    public float flySpeed;
	
	void Start () {
		playerTrans=GameObject.FindGameObjectWithTag("player").transform;
	    rigidbody = GetComponent<Rigidbody>();
	}
	
	
	void Update () {
	    if (GameObject.FindGameObjectWithTag("player").transform.parent != transform.parent)
	        this.enabled = false;
	    else
	        this.enabled = true;
        rigidbody.MovePosition(Vector3.Lerp(transform.position,playerTrans.position,flySpeed*Time.deltaTime));
	}
}
