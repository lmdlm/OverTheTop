using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move down when things start

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float speed = 5.0f;
    public GameObject enemyBullet;
   

    private Rigidbody2D rb;
    [SerializeField] public float timeBtwShots;
    public float startTimeBtwShots;
    private Transform player;

    // == private methods ==
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        StartCoroutine(MoveUpdate());
    }

    void Update()
    {
        //enemy sooting at players position
        if(timeBtwShots <=0)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    IEnumerator MoveUpdate()
    {
        //more random movement along x-axis
        yield return new WaitForSeconds(10.0f);
        rb.velocity = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), -2 * speed);
    }

    //public method
    public void SetMoveSpeed(float enemySpeed)
    {
        this.speed = enemySpeed;
    }
}
