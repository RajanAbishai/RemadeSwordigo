using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;
    //public Text scoreText, bestScoreText, diamondScoreText, totalDiamondScoreText;

    //private int count_Score, count_Diamond;

    public Text coinTextScore;
    private Text lifeText;

    public int coinCount=0;

    public AudioSource audioManager;

    private void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }




    void Awake()
    {
        MakeInstance();
        audioManager = GetComponent<AudioSource>();


    }


    void Update()
    {

    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisplayScore()
    {
        //audioManager.Play();


        //print("Coin count is : " + coinCount);



        // audioManager.Play();
    }

}
