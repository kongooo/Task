using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
   
	
	void Update ()
	{
	    Vector3 playPos= GameObject.FindGameObjectWithTag("player").GetComponent<Transform>().position;
        
	    if (Vector3.Distance(playPos, transform.position) > 8)
	    {
	        destroyme();
        }       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string Tag;
        Tag = other.tag;
        if (Tag == "room"||Tag=="OB")
            destroyme();
    }

    public void destroyme()
    {
        GetComponent<Animator>().SetTrigger("break");
        GetComponent<Rigidbody2D>().velocity*=0.3f;       
        Invoke("destroyafter",0.5f);
    }

    private void destroyafter()
    {
        Destroy(gameObject);
    }


}
