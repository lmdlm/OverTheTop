using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    public ParticleSystem bulletExplosion;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x +10 == target.x+10 && transform.position.y +10 == target.y + 10)
        {
            DestroyEnemyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
            DestroyEnemyBullet();
        }
    }

    private void DestroyEnemyBullet()
    {
        Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
