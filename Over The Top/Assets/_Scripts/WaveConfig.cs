﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    //Private  data fields
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private float enemySpeed = 3.0f;
    [SerializeField] private float spawnInterval = 0.35f;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private int damageValue = 5;
    [SerializeField] private int shootingInterval;

    //Public methods
    public Sprite GetEnemiesSprite() { return enemySprite; }
    public int GetEnemiesPerWave() { return enemiesPerWave; }
    public float GetEnemySpeed() { return enemySpeed; }
    public float GetSpawnInterval() { return spawnInterval; }
    public int GetScoreValue() { return scoreValue; }
    public int GetDamageValue() { return damageValue; }
    public int GetShootingInterval() { return shootingInterval; }

}
