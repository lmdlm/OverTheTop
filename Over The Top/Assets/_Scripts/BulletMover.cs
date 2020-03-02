using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float firingCoroutineSpeed = 0.1f;

    private GameObject bulletParent;

    private Coroutine firingCoroutine; //pointer to the coroutine - need this to stop firing

    // == private methods
    private void Start()
    {
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //set up a cooroutine to fire the bullets
            firingCoroutine = StartCoroutine(FireCoroutine());
            // FireBullet();
        }
        //stop firing
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    //coroutine must be of type IEnumerator
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            //generate bullets
            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            bullet.transform.position = transform.position;
            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            rbb.velocity = Vector2.up * bulletSpeed;
            //yield return causes the method to pause
            //sleep()
            yield return new WaitForSeconds(firingCoroutineSpeed);
        }
    }

    private void FireBullet()
    {
        // instantiate bullet
        Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
        // give it the same position as the player
        bullet.transform.position = bullet.transform.position;
        // give it velocity and move right
        Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
        //rbb.velocity = new Vector2(1 * speed, 0);
        rbb.velocity = Vector2.up * bulletSpeed;
    }
}
