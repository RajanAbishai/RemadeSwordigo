using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float speed = 5f;
    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition;

    public LayerMask groundLayer;

    private bool IsGrounded;
    private bool jumped;

    private float jumpPower = 12f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

     void Start()
    {

    }

     void Update()
    {
        CheckIfGrounded();
        PlayerJump();
        
                
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");



        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }

        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }


        else { // (h==0)

            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));


        



    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;

        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        IsGrounded = Physics2D.Raycast(groundCheckPosition.position,Vector2.down,0.1f,groundLayer);

        if (IsGrounded)
        {
            //if we are on the ground and we jumped before .. we are gonna say, now jump is false and go back from the jump animation to idle or walk
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }
   

    void PlayerJump()
    {
        if (IsGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower); //leaving x as is because jump only goes in y direction
                anim.SetBool("Jump", true);

            }
        }
    }

}
