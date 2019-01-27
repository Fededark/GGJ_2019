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

        if (!isLocalPlayer)
            rb.simulated = false;
    }

    //[SyncVar]
    //private Vector3 pos;
    [SyncVar]
    private bool animMoving;
    [SyncVar]
    private int animDir;

    void Update()
    {
        //if (isLocalPlayer)
        //    pos = transform.position;
        //else
        //    transform.position = pos;
        animator.SetBool("moving", animMoving);
        animator.SetInteger("Direction", animDir);
    }

    //[Command]
    //public void CmdMove(float moveHorizontal, float moveVertical)
    //{
    //    SetMovement(moveHorizontal, moveVertical);
    //    RpcMove(moveHorizontal, moveVertical);
    //}

    //[ClientRpc]
    //public void RpcMove(float moveHorizontal, float moveVertical)
    //{
    //    if (isLocalPlayer) return;        
    //    SetMovement(moveHorizontal, moveVertical);
    //}

    //public void SetMovement(float moveHorizontal, float moveVertical)
    //{
    //    this.moveHorizontal = moveHorizontal;
    //    this.moveVertical = moveVertical;
    //}

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Mathf.Approximately(moveHorizontal + moveVertical, 0f))
        {
            animMoving = false;
        }
        else
        {
            animMoving = true;
            if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
            {
                animDir = 1;
                if ((moveHorizontal < 0f && transform.localScale.x > 0f) || (moveHorizontal > 0f && transform.localScale.x < 0f))
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                if (transform.localScale.x < 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                if (moveVertical > 0f)
                    animDir = 2;
                else
                    animDir = 0;
            }

        }

        rb.velocity = (transform.right * moveHorizontal + transform.up * moveVertical)
            * (isDark ? speedInDark : speed);

    }
}
