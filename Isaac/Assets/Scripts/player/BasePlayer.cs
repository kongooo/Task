using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BasePlayer : MonoBehaviour
{
    private static BasePlayer _instance;
    private int change = 5;
    public static BasePlayer Instance { get { return _instance; } }

    private int HP;
    public GameObject deathBody,head,body,soul;
    public Slider hpslider;
    public Text hpText;
    public int maxhp = 6;

    private bool isdeath=false;
    private Renderer renderer1,renderer2;
	void Awake ()
	{
	    HP = maxhp;
        transform.position=new Vector3(10,1,-0.2f);
	    _instance = this;
        setTransparency(soul,0);
	    setTransparency(deathBody,0);
	    renderer1 = head.GetComponent<Renderer>();
	    renderer2 = body.GetComponent<Renderer>();
	}

    void Update()
    {
        renderer1.material.color = Color.Lerp(renderer1.material.color, Color.white, Time.deltaTime * change);
        renderer2.material.color = Color.Lerp(renderer2.material.color, Color.white, Time.deltaTime * change);
        hpslider.value = (float) HP / maxhp;
        hpText.text = HP + "/" + maxhp;
        SetDeath();
    }

    public void SufferDamage(int reduce)
    {
        if (HP > 0)
        {
            HP -= reduce;
            renderer1.material.color=Color.red;
            renderer2.material.color = Color.red; 
        }        
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
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void setTransparency(GameObject go,float change)
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
