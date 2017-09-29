using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    void Update()
    {
        Debug.Log(transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        string Tag;
        Tag = other.tag;
        if (Tag == "room"||Tag=="OB"||Tag=="magic")
            destroyme();
    }

    public void destroyme()
    {
        GetComponent<Animator>().SetTrigger("break");
        GetComponent<Rigidbody>().velocity=Vector3.zero;       
        Invoke("destroyafter",0.5f);
    }

    private void destroyafter()
    {
        Destroy(gameObject);
    }


}
