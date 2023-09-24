using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingObject
{
    [System.Serializable]
    public struct Settings
    {
        public AudioClip hurtClip;
        public AudioClip dieClip;
        public int score;

        public float maxHp;
        public float speed;
        public float damage;
    }

    private AudioSource audioPlayer;
    private NavMeshAgent pathFinder;
    private Animator enemyAnim;
    private BoxCollider enemyCollider;
    private Rigidbody enemyRigid;

    public LivingObject target;
    public ParticleSystem hitEffect;

    public Settings setting;

    public float chaseDelay = 0.25f;
    public float dieDelay = 0.25f;

    public float timeBetAttack = 0.5f;
    public float lastAttackTime;

    private bool isChase = false;

    public bool HasTarget { get { return target != null && !target.Dead; } }

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        enemyCollider = GetComponent<BoxCollider>();
        enemyRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(ChaseTarget());
    }

    private void Update()
    {

        if (GameManager.Instance.IsPause || GameManager.Instance.IsGameOver)
        {
            isChase = false;
            pathFinder.isStopped = true;
            return;
        }

        if (!isChase)
        {
            isChase = true;
            StartCoroutine(ChaseTarget());
        }

        SetTargetAnim();


    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        Debug.Log(Hp);
        if(!Dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();
            audioPlayer.PlayOneShot(setting.hurtClip);
        }
    }

    public override void Die()
    {
        base.Die();
        enemyAnim.SetTrigger("Die");
        Destroy(gameObject, 2f);
        audioPlayer.PlayOneShot(setting.dieClip);
        GameManager.Instance.AddScore(setting.score);
        //StartCoroutine(DieCoroutine());
    }

    public void SetEnemy(Settings value)
    {
        setting = value;
        Hp = setting.maxHp;
    }

    public void SetTargetAnim()
    {
        enemyAnim.SetBool("HasTarget", HasTarget);
    }

    public void StartSinking()
    {
        enemyRigid.isKinematic = false;
        pathFinder.isStopped = true;
        pathFinder.enabled = false;
        enemyCollider.enabled = false;
    }
    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(dieDelay);
        pathFinder.isStopped = true;
        pathFinder.enabled = false;
        enemyCollider.enabled = false;
    }

    private IEnumerator ChaseTarget()
    {
        isChase = true;
        while (!Dead)
        {
            if(HasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(target.transform.position);
            }
            else
            {
                pathFinder.isStopped = true;
                var targetObj = GameObject.FindWithTag("Player").GetComponent<Player>();
                if(targetObj != null && !targetObj.Dead)
                {
                    target = targetObj;
                }
            }

            yield return new WaitForSeconds(chaseDelay);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryAttack(collision);
        }

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행
    //    if (other.CompareTag("Player"))
    //    {
    //        TryAttack(other);
    //    }
    //}

    void TryAttack(Collision collision)
    {

        if (Time.time > lastAttackTime + timeBetAttack && collision.gameObject == target.gameObject)
        {
            lastAttackTime = Time.time;
            Vector3 dir = transform.position + Vector3.up - collision.transform.position + Vector3.up;
            collision.gameObject.GetComponent<Player>().OnDamage(setting.damage, collision.transform.position, dir.normalized);
        }
    }
    //void TryAttack(Collider other)
    //{

    //    if (Time.time > lastAttackTime + timeBetAttack && other.gameObject == target.gameObject)
    //    {
    //        lastAttackTime = Time.time;
    //        Vector3 dir = transform.position + Vector3.up - other.transform.position + Vector3.up;
    //        other.GetComponent<Player>().OnDamage(setting.damage, other.transform.position, dir.normalized);
    //    }
    //}
}
