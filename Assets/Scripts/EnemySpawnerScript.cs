using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject rangeEnemyPrefab;
    private float spawnRadius = 12.5f;

    private float rangeEnemyTimer = 60f;
    private float timer = 0f;
    private float spawnInterval = 1.5f;
    private float timer2 = 0f;
    private float moneyInterval = 10f;
    private TextMeshProUGUI money;

    private void Awake()
    {
        money = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        rangeEnemyTimer -= Time.deltaTime;
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
        spawnInterval = spawnInterval * Mathf.Pow(0.99f, Time.deltaTime / 1.5f);

        if (timer2 >= moneyInterval)
        {
            money.text = (Int32.Parse(money.text) + 10).ToString();
            timer2 = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int enemyNumber = 1;
        if(rangeEnemyTimer <= 0)
        {
            enemyNumber += UnityEngine.Random.Range(0, 5);
        }
        if(enemyNumber > 4)
        {
            Vector2 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(rangeEnemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Vector2 spawnPosition = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}