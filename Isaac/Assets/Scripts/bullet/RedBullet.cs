using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    
    public void RedMove()
    {
        float x = Random.Range(-3, 3);
        float y = Random.Range(2, 4);
        GetComponent<Rigidbody>().velocity = new Vector3(x, y,0.1f*(4-y));
    }

    

    void OnTriggerEnter(Collider col )
    {
        if (col.tag == "room" || col.tag == "magic")
        {
            GetComponent<Animator>().SetTrigger("break");  
            GetComponent<Rigidbody>().velocity=Vector3.zero;
            Invoke("de",0.5f);
        }
            
    }

    void de()
    {
        Destroy(gameObject);
    }

    
}
