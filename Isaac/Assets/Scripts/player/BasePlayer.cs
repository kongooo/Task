using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePlayer : MonoBehaviour
{
    private static BasePlayer _instance;
    public static BasePlayer Instance { get { return _instance; } }

    public int HP;
    public GameObject deathBody,head,body,soul;
    private const int maxhp = 6;

    private bool isdeath=false;

	void Awake ()
	{
	    _instance = this;
        setTransparency(soul,0);
	    setTransparency(deathBody,0);
	}

    void Update()
    {
        SetDeath();
    }

    public void SufferDamage(int reduce)
    {
        if(HP>0)
        HP -= reduce;
    }

    public void AddHP(int hp)
    {
        if(HP<maxhp)
        HP += hp;
    }
    //检测何时死亡
    private void SetDeath()
    {
        if (HP == 0)
        {
            head.SetActive(false);
            body.SetActive(false);
            setTransparency(deathBody,1);
            deathBody.GetComponent<Animator>().SetTrigger("death");          
            Invoke("setGravity",0.8f);
            HP = -1;
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
        }
    }

    private void setTransparency(GameObject go,float change)
    {
        Color color1 = go.GetComponent<SpriteRenderer>().color;
        color1.a = change;
        go.GetComponent<SpriteRenderer>().color = color1;
    }

    private void setGravity()
    {
        soul.GetComponent<SpriteRenderer>().DOFade(1, 1f);
        soul.GetComponent<Rigidbody2D>().gravityScale = -1;
    }
}
