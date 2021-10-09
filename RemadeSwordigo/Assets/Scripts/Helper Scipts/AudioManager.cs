//using System.Collections;
//using System.Collections.Generic; 
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource
        move_Audio_Source,
        jump_Audio_Source,
        enemyDie_Audio_Source,
        melee_Audio_Source,
        rangedAttack_Audio_Source, 
        background_Audio_Source,
        playerDied_Audio_Source,
        coin_Audio_Source;

    public bool moveSoundIsPlaying;

    // Boss7Shot for ranged attacks
    //gameover1 when player dies

    // none of this is really even needed
    /*public AudioClip
        power_Up_Clip,
        die_Clip,
        //coin_Clip, //don't need a coinclip.. just a source
        game_Over_Clip;

    */

    private void Update()
    {

        
        
    }



    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        
    }


    void MakeInstance() {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null) { Destroy(gameObject); }
    
    
    }
    public void PlayerDiedMusic()
    {
        playerDied_Audio_Source.Play();
    }
    public void RangedAttackSound()
    {
        rangedAttack_Audio_Source.Play();
    }

    public void BackgroundMusic()
    {
        background_Audio_Source.Play();
    }

    public void JumpSound()
    {
        jump_Audio_Source.Play();
    }

    public void MoveSound()
    {
            move_Audio_Source.Play(); 
    }

    


    public void CoinSound() // this is working
    {
        
        
        coin_Audio_Source.Play();
        //coin_Audio_Source.PlayOneShot(coin_Clip, 1.0f);
    }

    
public void EnemyHurtSound() { }

}
