using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingObject
{
    private AudioSource audioPlayer;
    public AudioClip hurtClip;
    public AudioClip dieClip;



    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        if(!Dead)
            audioPlayer.PlayOneShot(hurtClip);

    }

    public override void RestoreHp(float addHp)
    {
        base.RestoreHp(addHp);
    }

    public override void Die()
    {
        base.Die();
        audioPlayer.PlayOneShot(dieClip);
    }
}
