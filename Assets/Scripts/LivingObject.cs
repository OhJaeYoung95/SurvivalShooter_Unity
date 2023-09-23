using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour, IDamageable
{
    protected float maxHp = 100f;
    protected float Hp { get; set; }
    public bool Dead { get; private set; }
    public event Action onDeath;
    public virtual void OnEnable()
    {
        Dead = false;
        Hp = maxHp;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Mathf.Clamp(Hp -= damage, 0, maxHp);

        if(Hp <= 0 && !Dead)
        {
            Die();
        }
    }

    public virtual void RestoreHp(float addHp)
    {
        if (Dead)
            return;
        Mathf.Clamp(Hp += addHp, 0, maxHp);
    }

    public virtual void Die()
    {
        if(onDeath != null)
            onDeath();

        Dead = true;
    }
}