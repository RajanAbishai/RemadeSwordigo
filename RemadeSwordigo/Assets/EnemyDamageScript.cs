using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

   


    public Transform left_Collision, right_Collision, down_Collision, top_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;

    private Rigidbody2D myBodyForDamage;

    public GameObject coinCollectable;

    private bool stunned;

    public LayerMask playerLayer;

    float pushForce = 30f;


    private bool movementPossible;


    bool meleeChecker;

    private float collisionTimer;
    private float timeBeforeCollisionIsDetected=1.3f;

    private int damageTakenFromBullet = 15;





    private Animator animForDamage; //damage


    private void Awake()
    {
        movementPossible = GetComponent<SnailScript>().canMove;
        myBodyForDamage = GetComponent<Rigidbody2D>();
        animForDamage = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth; 
    }


    private void Update()
    {
        left_Collision_Pos = left_Collision.position; //assigning the position of the transform to the Vector3
        right_Collision_Pos = right_Collision.position;
    }

    // we need to call this function from another script, so, public.


    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        // play hurt animation (stun?)

        if (currentHealth <= 0) {

            animForDamage.Play("Stunned");

            if (tag == TagManager.SNAIL_TAG) { StartCoroutine(DeactivateEnemy(3.0f)); }

            else if (tag == TagManager.BEETLE_TAG){ StartCoroutine(DeactivateEnemy(1.5f)); }
            




            
            //StartCoroutine("DeactivateEnemy");



        }
    }




    void collisionsWithPlayer() //moved from the other class
    {

        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.2f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.2f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer); //Overlap circle is the same as raycast but uses a circle

        if (topHit != null)
        {
            if (topHit.gameObject.tag == TagManager.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f); //bounce it in the air. 7f will the y bounce velocity

                    movementPossible = false;
                    myBodyForDamage.velocity = new Vector2(0, 0);



                    //animForDamage.Play("Stunned");
                    //stunned = true;


                    //beetle code here

                    /*if (tag == TagManager.BEETLE_TAG)
                    {
                        animForDamage.Play("Stunned");
                    }*/



                }

            }

        }

        if (leftHit)
        {

            collisionTimer += Time.deltaTime;




            if (leftHit.collider.gameObject.tag == TagManager.PLAYER_TAG && collisionTimer > timeBeforeCollisionIsDetected)   /*gameObject.GetComponent<PlayerAttack>().isMelee*/ //meleeChecker==true  
            {
                print("bump on left");

                // wait for a second before collision can be detected


                if (!stunned)
                {
                    //Apply damage to player
                }

                //print("Melee attack");

                else if (stunned)
                {
                    myBodyForDamage.velocity = new Vector2(pushForce, myBodyForDamage.velocity.y); //float pushForce= 15f;
                    //StartCoroutine("DeactivateEnemy");
                }


            }

        }

        if (rightHit)
        {


            if (rightHit.collider.gameObject.tag == TagManager.PLAYER_TAG && collisionTimer > timeBeforeCollisionIsDetected)
            {

                print("bump on right");

                if (!stunned)
                {
                    //Apply damage to player
                }

                //print("Melee attack");

                else if (stunned)
                {
                    myBodyForDamage.velocity = new Vector2(-pushForce, myBodyForDamage.velocity.y);
                    //StartCoroutine("DeactivateEnemy");
                }


            }






        }


    }





    IEnumerator DeactivateEnemy(float timer)
    {

        //AudioManager.instance.ZombieDieSound();
        yield return new WaitForSeconds(timer); //2f original





        //Randomizing coin spawning

       // if (Random.Range(0, 10) > 6)
        //{
            Instantiate(coinCollectable, transform.position, Quaternion.identity);
        //}



        gameObject.SetActive(false);

    }





    

    private void OnCollisionEnter2D(Collision2D target)
    {
            if (target.gameObject.tag == TagManager.BULLET_TAG)
            {
                    //collisionsWithPlayer(); //moved from the other class
                     takeDamage(damageTakenFromBullet);

                //StartCoroutine("DeactivateEnemy");
            } 


        else if (target.gameObject.tag == TagManager.PLAYER_TAG)
        {

            collisionsWithPlayer(); //moved from the other class


            //print("melee");
            //animForDamage.Play("Stunned");

           // StartCoroutine("DeactivateEnemy");


        }


    } 






   



}
