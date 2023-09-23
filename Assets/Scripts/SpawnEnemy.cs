using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Enemy enemyPrefab;
    public AudioClip hurtClip;
    public AudioClip dieClip;

    public float spawnDelay = 1f;
    public float hp;
    public float speed;
    public float damage;
    public int score;

    public Enemy.Settings setting;

    // Start is called before the first frame update
    void Start()
    {
        setting.hurtClip = hurtClip;
        setting.dieClip = dieClip;
        setting.maxHp = hp;
        setting.speed = speed;
        setting.damage = damage;
        setting.score = score;

        StartCoroutine(SpawnEnemyCoroutine());
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        while(true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void Spawn()
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.SetEnemy(setting);
    }
}
