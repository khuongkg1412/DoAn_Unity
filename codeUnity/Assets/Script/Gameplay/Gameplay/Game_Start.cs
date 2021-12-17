using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Start : MonoBehaviour
{
    public float score;

    public bool isVictory = false , isGameOver;

    public GameObject Player;

    public GameObject pannelGameover;

    [SerializeField]
    private TextMeshProUGUI

            scoreResult,
            enemyKillResult,
            citizenSaveResult,
            gameplayResult,
            scoreRunning,
            enemyCount,
            citizenCount;

    float timeRemaining = 180;

    public bool timerIsRunning = false;

    public Text timeText;

    public Camera cameraMain;

    private float

            enemyNumber,
            enemyNumberStart,
            citizenNumber,
            citizenNumberStart;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        citizenNumberStart =
            GameObject
                .Find("Spawning Citizen")
                .GetComponent<Spawn_Enemy>()
                .numberOfEnemies + 1;
        citizenNumber = citizenNumberStart;
        enemyNumberStart =
            GameObject
                .Find("Spawning Enemy")
                .GetComponent<Spawn_Enemy>()
                .numberOfEnemies + 1;
        enemyNumber = enemyNumberStart;
        UpdateCitizen(0);
        UpdateEnemyNumber(0);

        isVictory = true;
        isGameOver = false;
    }

    public void GameOVer()
    {
        pannelGameover.SetActive(true);
        Player.GetComponent<Player_HP>().isDead = true;
        gameObject.GetComponent<ChangeScence>().gameResultOn();
    }

    public void UpdateScore(float scorePlus)
    {
        score += scorePlus;
        scoreRunning.text = "Score: " + score.ToString();
        scoreResult.text = score.ToString();
    }

    public void UpdateEnemyNumber(float number)
    {
        enemyNumber -= number;
        enemyCount.text =
            "Enemy: " + enemyNumber.ToString() + "/" + enemyNumberStart;

        if (enemyNumber == 0)
        {
            enemyCount.color = new Color(0, 1, 0);
        }
        else
        {
            enemyCount.color = new Color(1, 0, 0);
        }
    }

    public void UpdateCitizen(float number)
    {
        citizenNumber -= number;
        citizenCount.text =
            "Citizen: " + citizenNumber.ToString() + "/" + citizenNumberStart;
        if (citizenNumber == 0 && isVictory)
        {
            citizenCount.color = new Color(0, 1, 0);
        }
        else
        {
            citizenCount.color = new Color(1, 0, 0);
        }
    }

    void Update()
    {
        ConditionToVictory();
        if ( timerIsRunning && isGameOver != true)
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
            DisplayResultPannel();
            GameOVer();   
        }
    }

    void DisplayResultPannel(){
        if(isVictory){
            gameplayResult.text = "VICTORY";
        }else{
            gameplayResult.text = "GAMEOVER";
        }
        enemyKillResult.text = (enemyNumberStart - enemyNumber + 1).ToString();
        citizenSaveResult.text = (citizenNumberStart - citizenNumber + 1).ToString();
    }
    void ConditionToVictory()
    {
        if (enemyNumber == 0 || citizenNumber == 0)
        {
            isGameOver = true;
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
