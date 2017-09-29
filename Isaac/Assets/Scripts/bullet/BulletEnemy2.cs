using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy2 : MonoBehaviour
{

    public float speed;
    public Transform enemy2Trans;

	
    public void BulletMove(Vector3 player)
    {
        Vector3 posDif = Vector3.Normalize(player - transform.position);
        GetComponent<Rigidbody>().velocity = posDif*speed;
        if(Vector3.Distance(enemy2Trans.position,transform.position)>6)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="room"||col.tag=="player")
            Destroy(gameObject);
    }
}
