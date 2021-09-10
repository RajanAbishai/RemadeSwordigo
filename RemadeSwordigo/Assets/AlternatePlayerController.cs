using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatePlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D theRB;

    public Transform groundCheckPoint, groundCheckPoint2;
    public LayerMask WhatIsGround;
    private bool isGrounded;

    public Animator anim;
    public SpriteRenderer playerSR;


    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //move horizontally
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal")* moveSpeed, theRB.velocity.y);

        //check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, WhatIsGround)|| Physics2D.OverlapCircle(groundCheckPoint2.position, 0.1f, WhatIsGround);

        //Jump in the air

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if(Input.GetButtonUp("Jump") && theRB.velocity.y>0) //if we are moving upwards and we release the button
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y *0.5f);
        }

        //Flip the player
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            playerSR.flipX = false;
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSR.flipX = true;
        }
    }
}
