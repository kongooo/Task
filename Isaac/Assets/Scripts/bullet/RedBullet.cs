using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    
    public void RedMove()
    {
        float x = Random.Range(-3, 3);
        float y = Random.Range(1, 3);
        GetComponent<Rigidbody>().velocity = new Vector3(x, y,0.1f*(3-y));
    }

    

    void OnTriggerEnter(Collider col )
    {
        if (col.tag == "room" || col.tag == "magic")
        {
            GetComponent<Animator>().SetTrigger("break");
            
            Debug.Log("wall");
            Invoke("de",0.3f);
        }
            
    }

    void de()
    {
        Destroy(gameObject);
    }

    
}
