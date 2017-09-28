using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

	public virtual void Attack() { }
    public virtual void Damage() { }
    public virtual void Move() { }
    public virtual void Death() { }
}
