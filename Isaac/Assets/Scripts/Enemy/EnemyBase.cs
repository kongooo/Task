using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private static EnemyBase _instance;
    public static EnemyBase Instance { get { return _instance; } }

    private Renderer renderer;
    public int Hp;

    void Start()
    {
        _instance = this;
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime * 5);
    }
    public void EnemyDamage(int damage)
    {
        if (Hp > 0)
        {
            Hp -= damage;
            renderer.material.color = Color.red;
        }
    }
}
