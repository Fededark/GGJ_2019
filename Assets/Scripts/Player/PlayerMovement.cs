using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;

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
            rb.velocity = new Vector2(moveHorizontal, moveVertical) * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void TeleportTo(Vector3 newPosition)
    {
        transform.position = newPosition;
        rb.velocity = Vector2.zero;
        //TODO
    }
}
