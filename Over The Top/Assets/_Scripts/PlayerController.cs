using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // == public fields ==

    // == private fields ==
    private Rigidbody2D rb;

    private Camera gameCamera;

    [SerializeField] private float speed = 7.0f;
    public int playerMaxHealth = 100;
    public int playerCurrentHealth;

    public HealthBar healthBar;

    // == private methods ==

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(playerMaxHealth);
        playerCurrentHealth = playerMaxHealth;
    }

    void Update()
    {
        // add hMovement
        // if the player presses the up arrow, then move
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");
        // apply a force, get the player moving.
        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);

        float yValue = Mathf.Clamp(rb.position.y, -4.0f, 10.0f);
        float xValue = Mathf.Clamp(rb.position.x, -10f, 10f);

        rb.position = new Vector2(xValue, yValue);
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // if the player hit, then destroy the player
        // and the current enemy rectangle

        var enemy = whatHitMe.GetComponent<Enemy>();
        var enemyBullet = whatHitMe.GetComponent<EnemyBullet>();
        var powerUp = whatHitMe.GetComponent<PowerUp>();

        Debug.Log("Hit Something");

        if (enemy)  // if (enemy != null)
        {
            Debug.Log("It was the enemy");
            // play crash sound here
            //PlaySound(crashSound);
            // destroy the player and the rectangle
            // give the player points/coins
            //Destroy(player.gameObject);
            if (playerCurrentHealth < 21)
            {
                Destroy(gameObject);
                Destroy(healthBar.gameObject);
                FindObjectOfType<GameController>().EndGame();

            }
            else
            {
                Destroy(enemy.gameObject);
                playerCurrentHealth -= 20;
            }
            healthBar.SetHealth(playerCurrentHealth);
        }
        if(enemyBullet)
        {
            Debug.Log("It was the enemy bullet");
            // play crash sound here
            //PlaySound(crashSound);
            // destroy the player and the rectangle
            //Destroy(player.gameObject);
            if (playerCurrentHealth < 11)
            {
                Destroy(gameObject);
                Destroy(healthBar.gameObject);
                FindObjectOfType<GameController>().EndGame();

            }
            else
            {
                Destroy(enemyBullet.gameObject);
                playerCurrentHealth -= 10;
            }
            healthBar.SetHealth(playerCurrentHealth);
        }

        if(powerUp)
        {
            Destroy(powerUp.gameObject);
            playerCurrentHealth = playerMaxHealth;
            healthBar.SetHealth(playerCurrentHealth);
        }
    }
}