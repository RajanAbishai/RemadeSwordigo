using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

    }


    void LateUpdate()
    {


        if (playerTransform) //if we have the player and did not destroy the player using destroy, follow the player
        {
            Vector3 temp = transform.position;
            temp.x = playerTransform.position.x; //player is only being followed on the x axis
            temp.y = playerTransform.position.y;
            transform.position = temp;

        }




    }


}

