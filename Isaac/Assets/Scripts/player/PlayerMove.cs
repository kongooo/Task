using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float x, y;
    private Animator animator_body;
    private Animator animator_head;
    private Rigidbody rigidbody;
    public float speed;
    public GameObject body;
    public GameObject head;

	void Start ()
	{
	    animator_body = body.GetComponent<Animator>();
	    animator_head = head.GetComponent<Animator>();
	    rigidbody = GetComponent<Rigidbody>();
	}
	
	
	void FixedUpdate ()
	{
	    Vector3 currentPos=transform.position;
	    x = Input.GetAxis("Horizontal");
	    y = Input.GetAxis("Vertical");
	    
	    animator_body.SetFloat("lf", x);
	    if ((!Input.GetKey(KeyCode.LeftArrow)) && (!Input.GetKey(KeyCode.RightArrow)) &&
	        (!Input.GetKey(KeyCode.UpArrow)) && (!Input.GetKey(KeyCode.DownArrow)))
	    {
	        animator_head.SetFloat("leftRight", x);
	        animator_head.SetFloat("backFront", -y);
        }
       	    
        if (x == 0 && y == 0&&(!Input.GetKey(KeyCode.LeftArrow))&&(!Input.GetKey(KeyCode.RightArrow))&&(!Input.GetKey(KeyCode.UpArrow))&&(!Input.GetKey(KeyCode.DownArrow)))
	    {
	        animator_body.SetTrigger("stop");
            animator_head.SetTrigger("stop");
	    }
        else if(y!=0)
        {
            animator_body.SetTrigger("ud");
        }
        
        rigidbody.MovePosition(Vector2.Lerp(currentPos, currentPos + new Vector3(x, y,0), speed * Time.deltaTime) );
	}
}
