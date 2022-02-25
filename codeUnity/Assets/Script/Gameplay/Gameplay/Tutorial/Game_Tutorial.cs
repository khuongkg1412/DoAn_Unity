using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Tutorial : MonoBehaviour
{
    private float score;

    public bool isGameOver;

    public GameObject Player;

    public GameObject pannelGameover;

    [SerializeField]
    private TextMeshProUGUI scoreResult, enemyKillResult, citizenSaveResult, gameplayResult, scoreRunning, enemyCount,
            citizenCount;

    private float enemyNumber,
            enemyNumberStart,
            citizenSaveNumber,
            citizenNumberStart;

    private void Start()
    {
        Time.timeScale = 1f;
        //Convert to landscape mode in gameplay
        Screen.orientation = ScreenOrientation.Landscape;
        /*
        Set and get number citizen follow by spawning
        */
        citizenSaveNumber = 0;
        citizenNumberStart = 1;
        enemyNumberStart = 2;
        enemyNumber = 0;
        //Update citizen in Quest pannel
        UpdateCitizen(0);
        UpdateEnemyNumber(0);
        //Set value for two variable which decide game end and victory
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver == true)
        {
            //Game end. Display result and end the gameplay
            GameOVer();
        }
    }
    //Method Game over
    public void GameOVer()
    {
        //Player dead and set active for pannel result
        pannelGameover.SetActive(true);
        DisplayResultPannel();
    }
    //Update score
    public void UpdateScore(float scorePlus)
    {
        //Set score
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
        if (enemyNumber == enemyNumberStart)
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
        if (citizenSaveNumber == citizenNumberStart)
        {
            citizenCount.color = new Color(0, 1, 0);
        }
    }
    //Display resukt
    void DisplayResultPannel()
    {
        gameplayResult.text = "TUTORIAL COMPLETED";
        gameplayResult.fontSize = 27;
        Time.timeScale = 0f;
        Button nextBtn = GameObject.Find("Next_Button").GetComponent<Button>();
        nextBtn.interactable = true;
        if (Player.GetComponent<Player_Controller>().Character.isPlayerDead() == true || citizenSaveNumber == 0)
        {
            gameplayResult.text = "TUTORIAL FAILED";
            nextBtn.interactable = false;
        }
        enemyKillResult.text = enemyNumber.ToString();
        citizenSaveResult.text = citizenSaveNumber.ToString();
    }
    //Return score public method
    public float returnScore()
    {
        return score;
    }
}
