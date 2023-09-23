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

    public Settings setting;

    public LivingObject target;
    private NavMeshAgent pathFinder;
    private Animator enemyAnim;

    public float chaseDelay = 0.25f;

    public bool HasTarget { get { return target != null && !target.Dead; } }

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        Hp = setting.maxHp;

    }

    private void Start()
    {
        StartCoroutine(ChaseTarget());
    }

    private void Update()
    {
        SetTargetAnim();
    }

    public void SetEnemy(Settings value)
    {
        setting = value;
    }

    public void SetTargetAnim()
    {
        enemyAnim.SetBool("HasTarget", HasTarget);
    }

    private IEnumerator ChaseTarget()
    {
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

}
