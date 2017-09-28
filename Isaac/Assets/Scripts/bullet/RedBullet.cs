using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{	
    public void RedMove()
    {        
        GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,3), 2);
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("body").transform.position) > 3)
            Destroy(gameObject);
    }
}
