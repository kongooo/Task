using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private Renderer renderer;
    public int Hp,playerDamage;
    public float deathTime;
    private bool isdeath;
    private GameObject player;
    
    void Start()
    {
        renderer = GetComponent<Renderer>();
        player=GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime * 5);
        if (Hp == 0)
            isdeath = true;
        death();
        SetEnable();
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

    private void SetEnable()
    {
        if (player.transform.parent.name!= transform.parent.name)
        {
            Debug.Log("playerParent"+ player.transform.parent.name);
            Debug.Log("myParent"+transform.parent.name);
            if(GetComponent<GRID>())
            GetComponent<GRID>().enabled = false;
            if (GetComponent<pathLoad>())
            GetComponent<pathLoad>().enabled = false;
            if (GetComponent<LegEnemy>())
                GetComponent<LegEnemy>().enabled = false;
            if (GetComponent<Enemy2Attack>())
                GetComponent<Enemy2Attack>().enabled = false;
            if (GetComponent<FlyMove>())
                GetComponent<FlyMove>().enabled = false;
            if (GetComponent<BossMove>())
                GetComponent<BossMove>().enabled = false;
        }
        else
        {
            Debug.Log("same");
            if (GetComponent<GRID>())
                GetComponent<GRID>().enabled = true;
            if (GetComponent<pathLoad>())
                GetComponent<pathLoad>().enabled = true;
            if (GetComponent<LegEnemy>())
                GetComponent<LegEnemy>().enabled = true;
            if (GetComponent<Enemy2Attack>())
                GetComponent<Enemy2Attack>().enabled = true;
            if (GetComponent<FlyMove>())
                GetComponent<FlyMove>().enabled = true;
            if (GetComponent<BossMove>())
                GetComponent<BossMove>().enabled = true;
        }
    }
}
