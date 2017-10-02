using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{

    public GameObject buttet;
    private GameObject player;
    private Vector3 playerPos;

    private float j=0.5f;

	void Awake () {
		player=GameObject.FindGameObjectWithTag("player");
	}
	
	
	void Update () {
	    if (j >= 0.5f)
	    {
	        j = 0;
	        playerPos = player.GetComponent<Transform>().position;
	        GameObject bu= GameObject.Instantiate(buttet, transform.position, Quaternion.identity);
	        bu.GetComponent<BulletEnemy2>().BulletMove(playerPos);
	    }
	    j += Time.deltaTime;
	}
}
