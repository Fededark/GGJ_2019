using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class NetPlayerMovementBehaviour : NetworkBehaviour
{
    public float speed;
    public float speedInDark;

    private Animator animator;
    private Rigidbody2D rb;

    [SyncVar]
    bool isDark;

    [SyncVar]
    bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private float moveHorizontal;
    private float moveVertical;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (canMove)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else
        {
            moveHorizontal = moveVertical = 0f;
        }

        RpcMove(moveHorizontal, moveVertical);
    }

    [Command]
    public void CmdMove(float moveHorizontal, float moveVertical)
    {
        SetMovement(moveHorizontal, moveVertical);
        RpcMove(moveHorizontal, moveVertical);
    }

    [ClientRpc]
    public void RpcMove(float moveHorizontal, float moveVertical)
    {
        SetMovement(moveHorizontal, moveVertical);
    }

    public void SetMovement(float moveHorizontal, float moveVertical)
    {
        this.moveHorizontal = moveHorizontal;
        this.moveVertical = moveVertical;
    }

    void FixedUpdate()
    {
        if (Mathf.Approximately(moveHorizontal + moveVertical, 0f))
        {
            animator.SetBool("moving", false);
        }
        else
        {
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
            * (isDark ? speedInDark : speed);

    }
}
