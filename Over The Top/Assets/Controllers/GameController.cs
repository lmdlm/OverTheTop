using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//need multiple waves of enemies and means to loop through them

public class GameController : MonoBehaviour
{
    //== private fields ==
    private int playerScore = 0;

    //need text field to show score in
    //[SerializeField] private Text scoreText; //no text mesh pro
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    //for enemy waves
    //[SerializeField] private WaveConfig waveConfig;
    [SerializeField] private List<WaveConfig> waveConfigList;
    private int enemyRemainingCount;
    private int startingWave = 0;
    private int  waveNumber = 0;

    //public fields
    public GameObject gameOverUI;
    public GameObject gameWonUI;

    private void Awake()
    {
        SetupSingleton();
    }

    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateScore();
        //enemyRemainingCount = enemyCountPerWave;
        //enemyRemainingCount = waveConfig.GetEnemiesPerWave();
        //StartCoroutine(SetupNextWave());
        StartCoroutine(SpawnAllWaves());
    }

    private void SetupSingleton()
    {
        // this method gets called on creation
        // check for any other objects of the same type
        // if there is one, then use that one.
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);    // destroy the current object
        }
        else
        {
            // keep the new one
            DontDestroyOnLoad(gameObject);  // persist across scenes
        }
    }

    // == private methods ==
    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent += PointSpawners_OnEnemySpawnedEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        PointSpawners.EnemySpawnedEvent -= PointSpawners_OnEnemySpawnedEvent;
    }

    //set up a mrthod to spawn all the waves
    //make this a coroutine
    //wait for the setupnextwave cproutine
    //to run, and then go to next element in the list
    private IEnumerator SpawnAllWaves()
    {
        //loop through the list of waves
        for(int waveIndex = startingWave; waveIndex < waveConfigList.Count; waveIndex++)
        {
            var waveConfig = waveConfigList[waveIndex];
            yield return StartCoroutine(SetupNextWave(waveConfig));
        }
        FindObjectOfType<GameController>().GameWon();
        StartCoroutine(LoadScene());
    }

    private IEnumerator SetupNextWave(WaveConfig currentWave)
    {
        yield return new WaitForSeconds(5.0f);
        //play sound??
        Debug.Log($"Remaining enemies = {enemyRemainingCount}");
        FindObjectOfType<PointSpawners>().SetWaveConfig(currentWave);
        enemyRemainingCount = currentWave.GetEnemiesPerWave();
        //pass the wave config file to the point spawner
        EnableSpawning();
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(30.0f);
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    private void PointSpawners_OnEnemySpawnedEvent()
    {
        enemyRemainingCount--;
        Debug.Log($"Remaining enemies = {enemyRemainingCount}");
        if (enemyRemainingCount == 0)
        {
            //disable spawning
            DisableSpawning();
        }
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // add the score value for the enemy to the player score
        playerScore += enemy.ScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        //Debug.Log("Score: " + playerScore);
        scoreText.text = playerScore.ToString();
        if(playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            highScoreText.text = playerScore.ToString();
        }
        
    }

    private void DisableSpawning()
    {
        //get all the PointSpawners and then call their method to disable spawning
        foreach(var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.DisableSpawning();
        }
    }

    private void EnableSpawning()
    {
        //get all the PointSpawners and then call their method to enable spawning
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.EnableSpawning();
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        gameOverUI.SetActive(true);
        Invoke("Restart", 4f);
    }

    public void GameWon()
    {
        Debug.Log("Game win");
        gameWonUI.SetActive(true);
        Invoke("Restart", 4f);
    }

    public void Restart()
    {
        Destroy(gameObject);
        Debug.Log("Restarted");
        SceneManager.LoadScene("MainMenu");
    }
}
