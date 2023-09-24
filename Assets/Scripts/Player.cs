using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : LivingObject
{
    private AudioSource audioPlayer;
    private Animator playerAnim;
    public Animator panleAnim;
    public AudioClip hurtClip;
    public AudioClip dieClip;

    public Slider hpBar;


    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        Debug.Log(Hp);
    }

    private void Start()
    {
        Debug.Log(Hp);
        Debug.Log(Dead);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hpBar.gameObject.SetActive(true);
        hpBar.minValue = 0f;
        hpBar.maxValue = maxHp;
        hpBar.value = Hp;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        if(!Dead)
        {
            audioPlayer.PlayOneShot(hurtClip);
            panleAnim.SetTrigger("Hit");
        }

        hpBar.value = Hp;
    }

    public override void RestoreHp(float addHp)
    {
        base.RestoreHp(addHp);
        hpBar.value = Hp;
    }

    public override void Die()
    {
        base.Die();
        hpBar.gameObject.SetActive(false);
        playerAnim.SetTrigger("Dead");
        panleAnim.SetTrigger("Die");
        audioPlayer.PlayOneShot(dieClip);
        GameManager.Instance.GameOver();
    }

    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }

}
