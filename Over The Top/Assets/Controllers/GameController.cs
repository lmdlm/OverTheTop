using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    //== private fields ==
    private int playerScore = 0;

    //need text field to show score in
    //[SerializeField] private Text scoreText; //no text mesh pro
    [SerializeField] private TextMeshProUGUI scoreText;

    // == private methods ==
    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
    }
    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // add the score value for the enemy to the player score
        playerScore += enemy.ScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        Debug.Log("222");
        //Debug.Log("Score: " + playerScore);
        scoreText.text = playerScore.ToString();
    }
}
