using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to manage collisions

public class Enemy : MonoBehaviour
{
    // == set this up to publish an event to the system
    // == when killed

    //death effect
    public ParticleSystem explosionParticles;

    // == public fields ==
    // used from GameController enemy.ScoreValue
    public int ScoreValue {
        set { scoreValue = value; }
        get { return scoreValue; } }

    // delegate type to use for event
    public delegate void EnemyKilled(Enemy enemy);

    [SerializeField] private int scoreValue = 10;

    // create static method to be implemented in the listener
    public static EnemyKilled EnemyKilledEvent;

    // == private methods ==
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // if the player hit, then destroy the player
        // and the current enemy rectangle

        var player = whatHitMe.GetComponent<PlayerController>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        Debug.Log("Hit Something");

        if (player)  // if (player != null)
        {
            Debug.Log("It was the player");
            // play crash sound here
            //PlaySound(crashSound);
            // destroy the player and the rectangle
            // give the player points/coins
            //Destroy(player.gameObject);
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);            
        }

        if (bullet)
        {
            Debug.Log("It was the bullet");
            // play die sound
            //PlaySound(dieSound);
            // destroy bulllet
            Destroy(bullet.gameObject);
            // publish the event to the system to give the player points
            PublishEnemyKilledEvent();
            //show the explosion
            // destroy this game object
            //Destroy(explosion, explosionDuration);
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void PublishEnemyKilledEvent()
    {
        // make sure somebody is listening
        if (EnemyKilledEvent != null)
        {
            EnemyKilledEvent(this);
        }
    }
}