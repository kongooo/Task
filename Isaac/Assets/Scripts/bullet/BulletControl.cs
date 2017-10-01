using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

    public int damage;

    
    private void OnTriggerEnter(Collider other)
    {
        string Tag;
        Tag = other.tag;
        if (Tag == "room"||Tag=="OB"||Tag=="magic"||Tag=="Enemy")
            destroyme();
        switch (Tag)
        {
            case "Enemy":            
            other.GetComponent<EnemyBase>().EnemyDamage(damage);
            break;
                    
        }
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
