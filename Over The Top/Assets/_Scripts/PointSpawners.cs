using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

//        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);

public class PointSpawners : MonoBehaviour
{
    // == private fields ==
    private WaveConfig waveConfig;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    //[SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject enemyParent;

    //delegate method for events
    public delegate void EnemySpawned();
    public static event EnemySpawned EnemySpawnedEvent;

    //private ListUtils listUtils = new ListUtils();

    // == private methods ==
    private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent");
        if (!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }
        // get the spawn points here
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        //SpawnEnemyWaves();
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        //EnableSpawning();
    }

    //private void SpawnEnemyWaves()
    //{
    //    // create the stack of points
    //    spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
    //    //InvokeRepeating("SpawnOneEnemy", 0f, 0.25f);
    //    InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    //}

    // stack version
    private void SpawnOneEnemy()
    {
        if (spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
        //set the enemy parameters
        enemy.GetComponent<EnemyBehaviour>().SetMoveSpeed(waveConfig.GetEnemySpeed());
        enemy.GetComponent<Enemy>().ScoreValue = waveConfig.GetScoreValue();
        enemy.GetComponent<SpriteRenderer>().sprite = waveConfig.GetEnemiesSprite();
        PublishEnemySpawnedEvent();
    }


    //public methods
    public void PublishEnemySpawnedEvent()
    {
        //if event is not null, someone is listening, so tell them
        EnemySpawnedEvent?.Invoke();
    }

    public void EnableSpawning()
    {
        // InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, waveConfig.GetSpawnInterval());
    }

    public void DisableSpawning()

    {
        CancelInvoke(SPAWN_ENEMY_METHOD);
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
