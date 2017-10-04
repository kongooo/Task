using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManageDoor : MonoBehaviour {

    private List<Transform> EnemyList=new List<Transform>();

	void Update ()
	{
	    Transform[] gameObjects = gameObject.transform.parent.GetComponentsInChildren<Transform>();
       
	    for (int i = 0; i < gameObjects.Length; i++)
	    {	        
	        if (gameObjects[i].gameObject.tag == "Enemy")
	           EnemyList.Add(gameObjects[i]);
	    }
	    if (EnemyList.Count == 0)
	        GetComponent<BoxCollider>().isTrigger = true;
	}

    void OntriggerEnter(Collider other)
    {
        if(other.tag=="player")
            other.transform.SetParent(transform.parent);
        Debug.Log("player");
    }
}
