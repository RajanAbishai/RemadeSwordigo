using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject fireBullet;
    private Animator anim;
    //public GameObject spawnPoint; temporarily
    //public GameObject projectileHolder; //this shall be the spawn point?

    [SerializeField]
    private Transform spawnPoint;

    


	//for melee attacks
	public Transform attackPoint;
	public float attackRange = 0.75f; //radius of the attack
	public LayerMask enemyLayers;

	//We assign all enemies to a certain layer
    


    //public bool canAttack=true;

    //private float timerMeleeAttack; //using this variable to make the melee attacks work only every 45 seconds
    private float timerMeleeAttack = 1.0f;
    private float timerRangedAttack = 1.0f;
    private float attackTimer;
    public bool isMelee; //used on the snail or enemy script to detect if it has been meleed


    //public bool jumpCheck = GameObject.FindObjectWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerMovement>().jumped;

    //extras

    private bool projectileSpawned;

    [SerializeField]
    private List<GameObject> projectilePool = new List<GameObject>();


    int meleeDamageAmount = 20; //damage player does to enemy



    //extras.
    /*
    private bool canShoot;
    [SerializeField]
    private bool isEnemy;
    [SerializeField]
    private GameObject projectileHolder;
    */


    /*

    void GetObjectFromPoolOrSpawnANewOne()
    {
        print("in the pooling function");

        for (int i = 0; i < projectilePool.Count; i++) //firebullet assigned
        {

            if (!projectilePool[i].activeInHierarchy)
            {
                print("in the active in hierarchy");
                projectilePool[i].transform.position = spawnPoint.position; //renamed
                projectilePool[i].SetActive(true);

                projectileSpawned = true; //temporarily reenabled

                break;

            }
            else { projectileSpawned = false; }

        } */
    /*

        if (!projectileSpawned)
        {
            print("In projectile not spawned");
            //testing new smartpool
            GameObject bullet = Instantiate(fireBullet, spawnPoint.transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x; //to make it go to the left or the right side because it will either be positive or negative
            projectilePool.Add(bullet); //add it to the pool so that we can reuse it

            bullet.transform.SetParent(spawnPoint.transform); //projectileholder.position is replaced with spawn point
            projectileSpawned = true;




            /*   GameObject newProjectile = Instantiate(fireBullet, spawnPoint.position,
                   Quaternion.identity);

               //projectilePool.Add(newProjectile); temp disable

               newProjectile.transform.SetParent(projectileHolder.transform);

           */



    /*temporary disable of this code

   //spawn point was added
   GameObject bullet = Instantiate(fireBullet,spawnPoint.transform.position,Quaternion.identity);
   bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x; //to make it go to the left or the right side because it will either be positive or negative
   projectilePool.Add(Bullet);
   bullet.transform.SetParent(projectileHolder.transform);

   print("Ranged with timer");
   anim.SetTrigger(TagManager.RANGED_ATTACK_PARAMETER);
   attackTimer = 0f;
   } //ns
   } //ns

*/




    private void Awake()
         {
        
            anim = GetComponent<Animator>();

    /*
        if (isEnemy)
        {
            projectileHolder = GameObject.FindGameObjectWithTag("");
        }

        else { projectileHolder = GameObject.FindGameObjectWithTag(""); }
        */
    }




    void shootBullet()


    {
        attackTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J) && attackTimer> timerRangedAttack) //to make ranged attacks possible every 1 second
        {
            print("In the shoot bullet function");
            AudioManager.instance.RangedAttackSound();

            //GetObjectFromPoolOrSpawnANewOne();

            GameObject bullet = Instantiate(fireBullet, spawnPoint.transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x; //to make it go to the left or the right side because it will either be positive or negative


            //print("Ranged with timer");
            anim.SetTrigger(TagManager.RANGED_ATTACK_PARAMETER);
            attackTimer = 0f;

            /*temporary disable of this code

            //spawn point was added
            GameObject bullet = Instantiate(fireBullet,spawnPoint.transform.position,Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x; //to make it go to the left or the right side because it will either be positive or negative

            print("Ranged with timer");
            anim.SetTrigger(TagManager.RANGED_ATTACK_PARAMETER);
            attackTimer = 0f;
            
         */
        }

    }


    void melee()
    {

		
            attackTimer += Time.deltaTime;

        //Detect enemies in range of attack
        Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); //creates a circle from the point with specific radius

       
      
        if (Input.GetKey(KeyCode.K) && attackTimer > timerMeleeAttack) //play the animation every 0.45 seconds during the attack, not continuously
        {
            isMelee = true;
            print("Melee with timer");
            
            anim.SetTrigger(TagManager.MELEE_ATTACK_PARAMETER);
            attackTimer = 0f;


            if (isMelee)
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit an enemy: " + enemy);

                   enemy.GetComponent<EnemyDamageScript>().takeDamage(meleeDamageAmount);

                }
            }

        }




        //else if (Input.GetKey(KeyCode.K) && attackTimer > timerMeleeAttack ) { print("jump attack"); }





    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint != null) 
         { Gizmos.DrawWireSphere(attackPoint.position, attackRange); }
    }
    


    void Start()
    {
        
    }


    void Update()
    {
        
        melee();
        shootBullet();
    }
}
