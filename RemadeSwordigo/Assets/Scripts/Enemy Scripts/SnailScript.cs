using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{

    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;

    public Transform down_Collision;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void checkCollision()
    {
        //if a ray detects a collision, it will return true. So, the ! makes it the opposite. If we don't detect collision anymore, do the following
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.5f)) 
        {
            moveLeft = !moveLeft; //if it was moving left, it will move right to prevent it from falling
            changeDirection();
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            myBody.velocity = new Vector2(-moveSpeed,myBody.velocity.y);
        }

        else //move right
        {
            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
        }

        checkCollision();
    }

    void changeDirection()
    {
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x); //another way is tempScale.x = -tempScale.x
        }
        else
        { tempScale.x = -Mathf.Abs(tempScale.x); }
      
        
        transform.localScale = tempScale;
    }


}
