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

    private float jumpPower = 15f;

    public bool useKey;
    
    public float HoldTime=4.5f; // this will be the time needed to hold it to use
    bool startTimer;

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
        Use();

    }

    private void FixedUpdate()
    {
        PlayerWalk();
        
    }

    void accessFootStepSound() //function is attached to the animator component
    {
        AudioManager.instance.MoveSound();
    }

    
    IEnumerator HoldTimer()
    {
        
        Debug.Log("Starting timer at : " + Time.time);
        yield return new WaitForSeconds(HoldTime);
        if (!startTimer)
        {
            Debug.Log("Timer broke at : " + Time.time);
            
        }
        else
        {
            useKey = true;
            
            Debug.Log("Timer ended at : " + Time.time);
        }

    }




    void Use()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            startTimer = true;
            StartCoroutine(HoldTimer());
            
            
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            startTimer = false;
        }



       /* if (Input.GetKeyDown(KeyCode.F))
        {
            //useKey=true;
        }*/
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");



        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
            //AudioManager.instance.MoveSound(); // play the walk audio only when grounded, not used thanks to accessFootStepSound()

        }

        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
            //AudioManager.instance.MoveSound(); // Play the walk audio only when grounded,  not used thanks to accessFootStepSound()

        }


        else { // (h==0)

            myBody.velocity = new Vector2(0f, myBody.velocity.y);
            //AudioManager.instance.StopMoveSound();
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
                AudioManager.instance.JumpSound();
            }
        }
    }

}
