using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MinMap : MonoBehaviour
{

    public GameObject player;
	void Start () {
		
	}
	
	
	void Update ()
	{
	    transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
	}
}
