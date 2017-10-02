using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private Renderer renderer;
    public int Hp,playerDamage;
    public float deathTime;
    private bool isdeath;
    
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime * 5);
        if (Hp == 0)
            isdeath = true;
        death();
    }
    public void EnemyDamage(int damage)
    {
        if (Hp > 0)
        {
            Hp -= damage;
            renderer.material.color = Color.red;
        }
    }
    private void death()
    {
        if (isdeath)
        {
            GetComponent<Animator>().SetBool("isdeath", true);
            Invoke("destroy", deathTime);
        }
    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="player")
            col.gameObject.GetComponent<BasePlayer>().SufferDamage(playerDamage);
    }
}
