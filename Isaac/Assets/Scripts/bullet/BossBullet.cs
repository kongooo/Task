using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    public void BossBuMove(int m,int n,bool isAttack)
    {
        float y;
        float x1 = Random.Range(-10, 10);
        float x = Random.Range(m, n);
        float y1 = Random.Range(-10, 10);
        if(isAttack)
        y = Random.Range(Mathf.Abs(n),Mathf.Abs(m));
        else
        {
            y = Random.Range(m, n);
        }
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
