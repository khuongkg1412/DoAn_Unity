using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game_Start : MonoBehaviour
{
    NumeralStruct originNumeral = new NumeralStruct();
    //Create a new object for player
    public Character Character;
    private float score;

    public bool isVictory = false, isGameOver, isStoped = false;

    // public GameObject Player;

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

    private bool timerIsRunning = false;

    public Text timeText;

    private float enemyNumber,
            enemyNumberStart,
            citizenSaveNumber,
            citizenNumberStart;

    private void Start()
    {
        originNumeral = Player_DataManager.Instance.Player.numeral;
        //Reset data
        // loadingPlayer();
        //Loading Numeral of player 
        Character = new Character(Player_DataManager.Instance.Player.numeral);

        //Scale Time is normal
        Time.timeScale = 1f;
        //Convert to landscape mode in gameplay
        Screen.orientation = ScreenOrientation.Landscape;

        // Starts the timer automatically
        timerIsRunning = true;
        /*
        Set and get number citizen follow by spawning
        */
        citizenSaveNumber = 0;
        citizenNumberStart = GameObject.Find("Spawning Citizen").GetComponent<Spawn_Citizen>().numberOfCitizen;
        enemyNumberStart = GameObject.Find("Spawning Enemy").GetComponent<Spawn_Enemy>().numberOfEnemies;
        enemyNumber = 0;
        //Update citizen in Quest pannel
        UpdateCitizen(0);
        UpdateEnemyNumber(0);
        //Set value for two variable which decide game end and victory
        isVictory = false;
        isGameOver = false;
    }

    void Update()
    {
        //Check condition victory in every frame
        ConditionToVictory();
        //Continute runing time whilke game is not oer
        if (timerIsRunning && isGameOver != true)
        {
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
                timerIsRunning = false;
            }
        }
        else
        {
            //Game end. Display result and end the gameplay
            GameOVer();
            //Reset data
            loadingPlayer();
        }

    }
    //Loading Numeral of player 
    public void loadingPlayer()
    {
        //Set the numeral from static data
        Player_DataManager.Instance.Player.numeral = originNumeral;
        //= new NumeralStruct()
        //{
        //    ATK_Numeral = 10,
        //    DEF_Numeral = 0,
        //    HP_Numeral = 50,
        //    SPD_Numeral = 300,
        //    ATKSPD_Numeral = 1
        //};
        //Player_DataManager.Instance.Player.numeral = originNumeral;
        Debug.Log("Data: " + Player_DataManager.Instance.Player.numeral.HP_Numeral);
    }
    //Method Game over
    public void GameOVer()
    {
        if (!isStoped)
        {
            //Player dead and set active for pannel result
            pannelGameover.SetActive(true);
            //Plus the time left to the score
            UpdateScore(timeRemaining);
            //Show Result Pannel
            DisplayResultPannel();
            //Update score to database
            totalScore();
            isStoped = true;
        }

    }
    //Update score to database
    void totalScore()
    {
        //Get name of scence to calculate the stage
        string scenceName = SceneManager.GetActiveScene().name;
        //Calculate the stage
        float stage = float.Parse(scenceName.Substring(5));
        //Update score to database
        Player_DataManager.Instance.finishTheStage(score, stage, isVictory);
    }
    //Update score
    public void UpdateScore(float scorePlus)
    {
        //Set score to the UI on the scence
        score += scorePlus;
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
        if (GameObject.Find("Spawning Enemy").GetComponent<Spawn_Enemy>().isBossStage)
        {
            if (enemyNumber == enemyNumberStart)
            {
                isGameOver = true;
                isVictory = true;
            }
        }
        //Check condition victory
        else if (citizenSaveNumber == citizenNumberStart)
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
