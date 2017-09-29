using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegEnemy : MonoBehaviour
{
    private Animator animator;
    
    private float x, y;
    public float speed;
    private Vector3 newPos;
    private bool iscor=true;
    public LayerMask ObjectLayer;
    private float j = 1;

    public GameObject Redbullet;

	void Start ()
	{	    
	    animator = GetComponent<Animator>();	    
    }
	
	
	void Update ()
	{
        if (j >= 1)
        {
            j = 0;
            x = Random.Range(-2, 2);
            y = Random.Range(-2, 2);
            RaycastHit[] ray = Physics.RaycastAll(transform.position, new Vector2(x, y), 1, ObjectLayer);
            if (ray.Length > 0)
            {
                for (int i = 0; i < ray.Length; i++)
                {
                    Debug.Log(ray[i].collider.tag);
                }
                x = -x;
                y = -y;
            }
            animator.SetFloat("lf", x);
            GameObject.Instantiate(Redbullet, transform.position, Quaternion.identity).GetComponent<RedBullet>().RedMove();           
        }
	    j += Time.deltaTime;
	    GetComponent<Rigidbody>().MovePosition(Vector2.Lerp(transform.position,
	        transform.position + new Vector3(x, y, 0), speed * Time.deltaTime));
	}

    
    
}
