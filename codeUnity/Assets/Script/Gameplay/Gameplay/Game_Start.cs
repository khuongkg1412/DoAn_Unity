using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Start : MonoBehaviour
{
    public float score;

    public GameObject Player;

    public GameObject pannelGameover;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    float timeRemaining = 180;

    public bool timerIsRunning = false;

    public Text timeText;

    private void Awake() {
        
    }
    private void Start()
    {
         Screen.orientation = ScreenOrientation.Portrait;
       GameObject.Find("Camera").GetComponent<Camera_Follow>().CameraSetting();
        // Starts the timer automatically
        timerIsRunning = true;

      
    }

    public void GameOVer()
    {
        pannelGameover.SetActive(true);
        Player.GetComponent<Player_HP>().isDead = true;
        gameObject.GetComponent<ChangeScence>().reloadScence();
    }

    public void UpdateScore()
    {
        Debug.Log("Score: "+ score);
        score +=100f;
        textMeshPro.text = score.ToString();
    }

    void Update()
    {
 
        //UpdateScore();
        if (timerIsRunning)
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
        // else if(Player.GetComponent<Player_HP>().isDead)
        // {
        //     GameOVer();
        // }
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
