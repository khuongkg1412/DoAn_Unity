using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Start : MonoBehaviour
{
    public float score;

    public bool isVictory = false;

    public GameObject Player;

    public GameObject pannelGameover;

    [SerializeField]
    private TextMeshProUGUI

            scoreResult,
            scoreRunning;

    float timeRemaining = 180;

    public bool timerIsRunning = false;

    public Text timeText;

    public Camera cameraMain;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    public void GameOVer()
    {
        pannelGameover.SetActive(true);
        Player.GetComponent<Player_HP>().isDead = true;
        gameObject.GetComponent<ChangeScence>().reloadScence();
    }

    public void UpdateScore(float scorePlus)
    {
        score += scorePlus;
        scoreRunning.text = "Score: " + score.ToString();
        scoreResult.text = score.ToString();
    }

    void Update()
    {
        //transform.position =new Vector3(cameraMain.transform.position.x,    cameraMain.transform.position.y, 10) ;
        //UpdateScore();
        if (timerIsRunning || !isVictory)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime (timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        else
        {
            GameOVer();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
