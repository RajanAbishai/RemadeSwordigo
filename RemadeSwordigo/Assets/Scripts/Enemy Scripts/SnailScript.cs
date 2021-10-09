using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{

    public float moveSpeed = 1f;
    
    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;


    public Transform left_Collision, right_Collision;

    //public transform top_Collision; //with player

    private Vector3 left_Collision_Pos, right_Collision_Pos;


    public Transform down_Collision; //for movement


    //public GameObject coinCollectable;

    public bool canMove;
   
    
    // all in the damage script
    //private bool stunned;

   // public LayerMask playerLayer;
    //float pushForce= 30f;

    //bool meleeChecker;

    //private float collisionTimer;
    //private float timeBeforeCollisionIsDetected=1.3f;

    //

   




    private void Awake()
    {
        //meleeChecker = GameObject.FindObjectWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerAttack>().isMelee;


        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // This is done so that when the direction of the snail is flipped, we can also flip the direction of the left and right collision
        //detection positions


       
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
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {


        left_Collision_Pos = left_Collision.position; //assigning the position of the transform to the Vector3
        right_Collision_Pos = right_Collision.position;


       /* if (meleeChecker)
        {
            print("Yes");
        } */
       




        if (canMove)
        {

            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
                left_Collision.position = left_Collision_Pos;
                right_Collision.position = right_Collision_Pos;
            }

            else //move right
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
                left_Collision.position = right_Collision_Pos;
                right_Collision.position = left_Collision_Pos;
            }


        }

        checkCollision();
    }



    
    /* deactivated for moving
     * IEnumerator DeactivateEnemy()
    {

        //AudioManager.instance.ZombieDieSound();
        yield return new WaitForSeconds(2f); //2f original

        
        


        //Randomizing coin spawning

        if (Random.Range(0, 10) > 6)
        {
            Instantiate(coinCollectable, transform.position, Quaternion.identity);
        }



        gameObject.SetActive(false);

    }*/


  


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

   /* 
    *  disabled for moving
    * private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == TagManager.BULLET_TAG)
        {
            anim.Play("Stunned");

            StartCoroutine("DeactivateEnemy");
        }


        else if (target.gameObject.tag==TagManager.PLAYER_TAG && meleeChecker )
        {
            
            print("melee");
            anim.Play("Stunned");

            StartCoroutine("DeactivateEnemy");

            
        }


    } */


}
