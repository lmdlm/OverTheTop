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
    [SerializeField] private float angle = 0.0f;

    // == private methods ==


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // add hMovement
        // if the player presses the up arrow, then move
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");
        // apply a force, get the player moving.
        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);
        //rotation left & right
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    while(angle < 1.0f)
        //    {
        //        // Clockwise
        //        transform.Rotate(0.0f, angle, 0.0f);
        //        angle += 0.2f;
        //    }
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    while (angle > -1.0f)
        //    {
        //        // Counter-clockwise
        //        transform.Rotate(0.0f, angle, 0.0f);
        //        angle -= 0.2f;
        //    }
        //}
        // keep the player on the screen
        // float xValue = Mathf.clamp(value, min, max)
        // rb.position.x 
        float yValue = Mathf.Clamp(rb.position.y, 0.0f, 7.0f);
        float xValue = Mathf.Clamp(rb.position.x, -6.5f, 6.5f);

        rb.position = new Vector2(xValue, yValue);
    }
}
