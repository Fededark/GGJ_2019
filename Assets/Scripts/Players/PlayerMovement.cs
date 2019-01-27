using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour
{
    public float speed;
    public float darkSpeed;
    private Animator animator;
    private AudioSource audio;

    public RuntimeAnimatorController[] animations;

    public bool IsDark { get; set; }

    public bool canMove = true;

    private Rigidbody2D rb;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animations[Random.Range(0, animations.Length)];
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (Mathf.Approximately(moveHorizontal + moveVertical, 0f))
            {
                animator.SetBool("moving", false);
                //audio.Stop();
            }
            else
            {
                //audio.Play();
                animator.SetBool("moving", true);
                if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
                {
                    animator.SetInteger("Direction", 1);
                    if ((moveHorizontal < 0f && transform.localScale.x > 0f) || (moveHorizontal > 0f && transform.localScale.x < 0f))
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    if (transform.localScale.x < 0f)
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    if (moveVertical > 0f)
                        animator.SetInteger("Direction", 2);
                    else
                        animator.SetInteger("Direction", 0);
                }

            }

            rb.velocity = (transform.right * moveHorizontal + transform.up * moveVertical) 
                * (IsDark ? darkSpeed : speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

}
