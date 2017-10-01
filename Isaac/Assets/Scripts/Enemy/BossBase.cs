using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : MonoBehaviour
{
    private bool isdeath;

    void Update()
    {
        if (EnemyBase.Instance.Hp==0)
            isdeath = true;
        death();        
    }
    
    private void death()
    {
        if (isdeath)
        {
            GetComponent<Animator>().SetBool("isdeath",true);
            Invoke("destroy",1f);
        }
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
