using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;





public class DoorToExitElderHouse : MonoBehaviour
{


    //
    //private void OnCollisionEnter2D(Collision2D target)

    private void OnTriggerEnter2D(Collider2D target)

    {
       // print("In the function"); 
        if (target.tag == TagManager.PLAYER_TAG && GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerMovement>().useKey )
        {
            

            print("Use key");
            SceneManager.LoadScene(TagManager.ELDER_HOUSE_EXIT_LEVEL1);
            

        }


    }




}
