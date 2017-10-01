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
   
    private Vector3 dire;
    private bool isfly=false;
    private bool ismove=false, isattack1=false, isattack2=false;

    private float seed,saw1=0;
    private float i=2;

	void Start ()
	{
	    dir = playerTrans.position - transform.position;
        rigidbody = GetComponent<Rigidbody>();
	    animator = GetComponent<Animator>();
        
	    StartCoroutine("Attack");
	}
	
	
	void Update ()
	{	    
	    if (i >= 2)
	    {
	        dir = playerTrans.position - transform.position;
	        i = 0;
	        seed = Random.Range(3, 10);
	    }
	    i += Time.deltaTime;

	    if (saw1 < seed)
	    {
	        saw1 = seed*2;
	        ismove = true;
	    }
	    saw1 -= Time.deltaTime;
	}

    

    IEnumerator move( )
    {                    
         animator.SetFloat("ready", dir.x);
         yield return new WaitForSeconds(0.5f);
         animator.SetFloat("ready", 0);
         animator.SetFloat("jump", dir.x);
         if (dir.x < 0)
         {
                rigidbody.AddForce(new Vector3(-0.00004f, 0.00004f, 0));
                yield return new WaitForSeconds(0.3f);
                rigidbody.AddForce(new Vector3(0, -0.00009f, 0));
                yield return new WaitForSeconds(0.2f);
         }
         if (dir.x > 0)
         {
             rigidbody.AddForce(new Vector3(0.00004f, 0.00004f, 0));
             yield return new WaitForSeconds(0.3f);
             rigidbody.AddForce(new Vector3(0, -0.00009f, 0));
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
        animator.SetFloat("attack", dir.x);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 12; i++)
        {
            if (dir.x > 0)
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BossBullet>().BossBMove();
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("attack", 0);
        
        StartCoroutine("attack");
        StopCoroutine("Attack");

    }

    IEnumerator attack()
    {            
        animator.SetFloat("attack2", 1);
        yield return new WaitForSeconds(0.7f);
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.velocity = new Vector3(0, 30, -30);
        animator.SetFloat("fly", 1);
        animator.SetFloat("attack2", 0);
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("fly", -1);
        dire = Vector3.Normalize(playerTrans.position - transform.position);
        rigidbody.velocity = dire * 30;
        yield return new WaitForSeconds(0.5f);
        isfly = true;        
        yield return new WaitForSeconds(1.5f);
        isfly = false;
        rigidbody.isKinematic = false;
        animator.SetFloat("attack2", 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (isfly)
        {
            if (collision.gameObject)
            {
                animator.SetFloat("fly", 0);
                animator.SetFloat("attack2", -1);                
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                for(int i=0;i<20;i++)
                GameObject.Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BossBullet>().BossBMove2();                 
                rigidbody.velocity = Vector3.zero;
                rigidbody.isKinematic = true;                
            }
        }

    }
}
