using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //pick up effect
    public ParticleSystem pickUpEffect;
    private Rigidbody2D rb;
    public float speed = 15f;

    private void Start()
    {
        //rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if (collision.CompareTag("Player"))
        {
            player.playerCurrentHealth = player.playerMaxHealth;
            PickUp();
        }
    }

    void PickUp()
    {
        Instantiate(pickUpEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
