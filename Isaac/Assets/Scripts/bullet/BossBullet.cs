using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

    public void BossBMove()
    {
        float x1 = Random.Range(0, 10);
        float x = Random.Range(1, 8);
        float y1 = Random.Range(0, 10);
        float y = Random.Range(1, 8);
        x = x + x1 / 10;
        y = y + y1 / 10;
        GetComponent<Rigidbody>().velocity=new Vector3(x,y,(8-y)*0.1f);   
    }

    public void BossBMove2()
    {
        float x1 = Random.Range(-11, 10);
        float x = Random.Range(-9, 8);
        float y1 = Random.Range(-10, 10);
        float y = Random.Range(-9, 8);
        x = x + x1 / 10;
        y = y + y1 / 10;
        GetComponent<Rigidbody>().velocity = new Vector3(x, y, (8 - Mathf.Abs(y)) * 0.05f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "room" || col.tag == "magic")
        {
            GetComponent<Animator>().SetTrigger("break");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Invoke("de", 0.5f);
        }
    }

    void de()
    {
        Destroy(gameObject);
    }
}
