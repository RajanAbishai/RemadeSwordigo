using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour { 

     private float speed = 10f;
    private Animator anim;
    public float bulletTimer;
    public float bulletLifeTime=5.0f;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }



    void Start()
    {
       StartCoroutine(DisableBullet(5f));
    }

    
    void Update()
    {
        Move();
        //disableBullet(); //temporary disable
    }

    public void Move()
    {
        
        Vector3 temp = transform.position; // this is used to move the game object.. using transform to move it
        temp.x += speed * Time.deltaTime; //deltaTime is the time in seconds from the last frame to the current frame
        transform.position = temp;
    }


    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value; // new value is set in ShootBullet function
        }

    }
    /* disabling this as well because it diables all bullets
    void disableBullet()
    {
        bulletTimer+= Time.deltaTime;

        if (bulletTimer > bulletLifeTime)
        {
            print("Inside bullet timer > bullet life time ");
            
            gameObject.SetActive(false);
            bulletLifeTime = 0f;
            
        }
    } */

    // experimenting with new disableBullet function. So, disabling this
    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer); //wait for 5 seconds
        
        gameObject.SetActive(false); //this will deactivate the bullet if it does not hit anything.. disabling because this disabled all bullets fired after the first one
    }
    

    private void OnTriggerEnter2D(Collider2D target)
    {
        //the bullet will be disabled if it hits one of the enemies

        if (target.gameObject.tag == TagManager.BEETLE_TAG || target.gameObject.tag==TagManager.SNAIL_TAG)
        {
            anim.Play("Explode");
            StartCoroutine(DisableBullet(0.9f)); //disabling this to test out the new disable bullet code
            gameObject.SetActive(false);

        }

        else if (target.gameObject.tag == TagManager.PLAYER_TAG) //to prevent being pushed back
        {
            Destroy(gameObject);
        }

    }


}
