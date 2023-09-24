using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AudioClip hurtClip;
    public AudioClip dieClip;

    public float spawnDelay = 1f;
    public float hp;
    public float speed;
    public float damage;
    public int score;

    public Enemy.Settings setting;

    private bool isSpawn = false;

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

    private void Update()
    {
        if (GameManager.Instance.IsPause || GameManager.Instance.IsGameOver)
        {
            StopAllCoroutines();
            isSpawn = false;
        }
        else
        {
            if(!isSpawn)
                StartCoroutine(SpawnEnemyCoroutine());
        }

    }

    IEnumerator SpawnEnemyCoroutine()
    {
        isSpawn = true;
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().SetEnemy(setting);
        //enemy.SetEnemy(setting);
        enemy.transform.SetParent(transform);
    }
}
