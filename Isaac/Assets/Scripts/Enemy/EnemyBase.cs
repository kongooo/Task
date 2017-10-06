using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private Renderer renderer;
    public int Hp,playerDamage;
    public float deathTime;
    public GameObject[] DeathSpecial;
    private bool isdeath,isspecial=false;
    private GameObject player;
    private int specilaNumber;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        player=GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        SetEnable();
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime * 5);
        if (Hp == 0)
            isdeath = true;
        death();
        if (gameObject.name != "boss")
            if (RoomManage.Instance.islast)
                transform.localPosition = new Vector3(
                    Mathf.Clamp(transform.localPosition.x, -1.7f, 1.7f),
                    Mathf.Clamp(transform.localPosition.y, -0.9f, 0.9f),
                    transform.localPosition.z);
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
        if (GetComponent<GRID>())
        {
            for(int i=0;i<GetComponent<GRID>().PathObject.Count;i++)
                Destroy(GetComponent<GRID>().PathObject[i]);
        }
        specilaNumber = Random.Range(0, 32);
        Instantiate(DeathSpecial[specilaNumber], transform.position, Quaternion.identity);
        Destroy(gameObject);       
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="player")
            col.gameObject.GetComponent<BasePlayer>().SufferDamage(playerDamage);
    }

    private void SetEnable()
    {
        if (Vector2.Distance(Camera.main.transform.position,transform.position)>7)
        {
            if(BossMove.Instance.countTime>0)
            if (GetComponent<BossMove>())
                GetComponent<BossMove>().enabled = false;
            if (GetComponent<GRID>())
                GetComponent<GRID>().enabled = false;
            if (GetComponent<pathLoad>())
                GetComponent<pathLoad>().enabled = false;
            if (GetComponent<LegEnemy>())
                GetComponent<LegEnemy>().enabled = false;
            if (GetComponent<Enemy2Attack>())
                GetComponent<Enemy2Attack>().enabled = false;
            if (GetComponent<FlyMove>())
                GetComponent<FlyMove>().enabled = false;                      
        }
        else
        {
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
