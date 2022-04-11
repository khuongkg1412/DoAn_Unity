using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game_Start : MonoBehaviour
{
    private float score;

    public bool isVictory = false, isGameOver, isStoped = false;
    public GameObject pannelGameover;

    [SerializeField]
    private TextMeshProUGUI scoreResult,
            enemyKillResult,
            citizenSaveResult,
            gameplayResult,
            scoreRunning,
            enemyCount,
            citizenCount;

    public float timeRemaining = 600;

    public Text timeText;

    private float enemyNumber,
            enemyNumberStart,
            citizenSaveNumber,
            citizenNumberStart;

    private void Start()
    {
        //Scale Time is normal
        Time.timeScale = 1f;
        //Convert to landscape mode in gameplay
        Screen.orientation = ScreenOrientation.Landscape;
        /*
        Set and get number citizen follow by spawning
        */
        citizenSaveNumber = 0;
        enemyNumber = 0;
        if (GameObject.Find("Spawning Citizen"))
        {
            citizenNumberStart = GameObject.Find("Spawning Citizen").GetComponent<Spawn_Citizen>().numberOfCitizen;
        }
        if (GameObject.Find("Spawning Enemy"))
        {
            enemyNumberStart = GameObject.Find("Spawning Enemy").GetComponent<Spawn_Enemy>().numberOfEnemies;
        }
        //Update citizen in Quest pannel
        UpdateCitizen(0);
        UpdateEnemyNumber(0);
        //Set value for two variable which decide game end and victory
        isVictory = false;
        isGameOver = false;
    }

    void Update()
    {
        if (!isStoped)
        {
            //Continute runing time whilke game is not oer
            if (isGameOver == false)
            {
                //Update the score from Character object
                UpdateScore();
                //Check condition victory in every frame
                ConditionToVictory();
                //Time is not end
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    //Time's up
                    timeRemaining = 0;
                }
            }
            else
            {
                //Game end. Display result and end the gameplay
                GameOVer();
            }
        }
    }
    //Method Game over
    public void GameOVer()
    {
        isStoped = true;
        //Player dead and set active for pannel result
        pannelGameover.SetActive(true);
        //Plus the time left to the score
        score += Mathf.Round(timeRemaining * 6 / 100);
        //Show Result Pannel
        DisplayResultPannel();
        //Update statistic to database
        Player_DataManager.Instance.updateStatistic(citizenSaveNumber, enemyNumber);
    }
    //Update score to database
    void updateTotalScore()
    {
        //Get name of scence to calculate the stage
        string scenceName = SceneManager.GetActiveScene().name;
        //Calculate the stage
        int stage = int.Parse(scenceName.Substring(5));
        //Update score and statistic to database
        Player_DataManager.Instance.finishTheStage(score, 20f, stage);
    }
    //Update score every frame by the score that is hold by object Character
    public void UpdateScore()
    {
        float currentPlayerScore = GameObject.FindWithTag("Player").GetComponent<Player_Controller>().Character.returnScroe();
        //Set score to the UI on the scence
        score = Mathf.Round(currentPlayerScore);
        scoreRunning.text = score.ToString();
        scoreResult.text = score.ToString();
    }
    //Update enemy
    public void UpdateEnemyNumber(float number)
    {
        //Set enemy number
        enemyNumber += number;
        enemyCount.text = "Enemy: " + enemyNumber.ToString() + "/" + enemyNumberStart;
        //Turn green color when completed task
        if (enemyNumber == 0)
        {
            enemyCount.color = new Color(0, 1, 0);
        }
    }
    //Update citizen
    public void UpdateCitizen(float number)
    {
        //Set citizen number
        citizenSaveNumber += number;
        citizenCount.text = "Citizen: " + citizenSaveNumber.ToString() + "/" + citizenNumberStart;
        //Turn green color when completed task
        if (citizenSaveNumber == citizenNumberStart && isVictory)
        {
            citizenCount.color = new Color(0, 1, 0);
        }
    }
    //Display resukt
    void DisplayResultPannel()
    {
        //Check victory
        if (isVictory)
        {
            //Update score to database
            updateTotalScore();
            gameplayResult.text = "VICTORY";
            Button nextBtn = GameObject.Find("Next_Button").GetComponent<Button>();
            nextBtn.interactable = true;
        }
        else
        {
            gameplayResult.text = "GAMEOVER";
        }
        enemyKillResult.text = enemyNumber.ToString();
        citizenSaveResult.text = citizenSaveNumber.ToString();

    }
    //Condition to victory
    void ConditionToVictory()
    {
        //Check condition victory
        if (citizenSaveNumber == citizenNumberStart)
        {
            isGameOver = true;
            isVictory = true;
        }
    }
    //Method display time
    void DisplayTime(float timeToDisplay)
    {   //Increase time by 1
        timeToDisplay += 1;
        //Convert to minut and second
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //Set to text
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
