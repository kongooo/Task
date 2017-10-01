using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMove : MonoBehaviour
{
    public Transform playerTrans;
    public GameObject bullet;

    private Rigidbody rigidbody;
    private Animator animator;
    private Vector3 dir;
    private Vector3 direction;
    private Vector3 dire;
    private bool isfly=false;

    private int seed,saw;
    private float i=2;

	void Start ()
	{
	    dir = playerTrans.position - transform.position;
        rigidbody = GetComponent<Rigidbody>();
	    animator = GetComponent<Animator>();
        
	    StartCoroutine("attack");
	}
	
	
	void Update ()
	{
	    direction = playerTrans.position - transform.position;
	    if (i >= 2)
	    {
	        dir = playerTrans.position - transform.position;
	        i = 0;
	    }
	    i += Time.deltaTime;
	    seed = Random.Range(0, 3);
	    
	}

    

    IEnumerator move( )
    {       
        animator.SetFloat("ready",dir.x);        
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("ready",0);       
        animator.SetFloat("jump",dir.x);
        if (dir.x < 0)
        {
            rigidbody.AddForce(new Vector3(-0.00007f, 0.00012f, 0));
            yield return new WaitForSeconds(0.3f);
            rigidbody.AddForce(new Vector3(0, -0.00025f, 0));
            yield return new WaitForSeconds(0.2f);
        }
        if (dir.x > 0)
        {
            rigidbody.AddForce(new Vector3(0.00007f, 0.00012f, 0));
            yield return new WaitForSeconds(0.3f);
            rigidbody.AddForce(new Vector3(0, -0.00025f, 0));
            yield return new WaitForSeconds(0.2f);
        }
        animator.SetFloat("jump", 0);
        animator.SetFloat("land", dir.x);
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.zero);                
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("land", 0);
        yield return move();
    }

    IEnumerator Attack()
    {
        animator.SetFloat("attack",direction.x);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 12; i++)
        {
            if(direction.x>0)
            
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BossBullet>().BossBMove();
            
        }
        yield return new WaitForSeconds(2);
        animator.SetFloat("attack",0);
    }

    IEnumerator attack()
    {
        animator.SetFloat("attack2",1);
        yield return new WaitForSeconds(0.7f);
        GetComponent<Rigidbody>().velocity=new Vector3(0,30,-30);
        animator.SetFloat("fly", 1);
        animator.SetFloat("attack2", 0);
        yield return new WaitForSeconds(0.5f);
        
        
        
        dire = Vector3.Normalize(playerTrans.position - transform.position);
        GetComponent<Rigidbody>().velocity = dire * 30;
        isfly = true;
        animator.SetFloat("fly",-1);
        yield return new WaitForSeconds(2f);
        isfly = false;
        GetComponent<Rigidbody>().isKinematic = false;
        animator.SetFloat("attack2",0);
        Debug.Log("end");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (isfly)
        {
            if (collision.gameObject)
            {
                transform.position=new Vector3(transform.position.x,transform.position.y,-0.2f);
                animator.SetFloat("attack2", -1);
                animator.SetFloat("fly", 0);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().isKinematic = true;

            }
        }

    }
}
