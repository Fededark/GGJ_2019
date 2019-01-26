using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float darkSpeed;
    public bool IsDark { get; set; }

    public bool canMove = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            rb.velocity = (transform.right * moveHorizontal + transform.up * moveVertical) 
                * (IsDark ? darkSpeed : speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

}
