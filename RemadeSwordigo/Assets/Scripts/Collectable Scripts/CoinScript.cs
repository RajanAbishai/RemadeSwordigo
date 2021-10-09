using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CoinScript : MonoBehaviour

{

    

    public AudioSource coinSound;
    //public AudioClip coinSound;

    private void Awake()
    {
        //coinSound = AudioManager.instance.Play(coin_Clip);

    }

    
    public void playCoinSound()
    {
        coinSound.Play();
    }


    void OnTriggerEnter2D(Collider2D target) //script to detect collision
    {
        if (target.tag == TagManager.PLAYER_TAG)
        {
            //cannot implicitly convert void to Unity Engine audio source

            
            AudioManager.instance.CoinSound(); // plays the coin audio
            GameplayController.instance.coinCount++;

            GameplayController.instance.coinTextScore.text = "x" + GameplayController.instance.coinCount.ToString();

            

            print("Number of coins: " + GameplayController.instance.coinCount);

            gameObject.SetActive(false); //disables game object upon touching
        }
    }
}
