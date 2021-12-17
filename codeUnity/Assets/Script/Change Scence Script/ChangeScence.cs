using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScence : MonoBehaviour
{
    //Wait for reoading excutes
    private float waitToLoad;

    Button replayButton;


    public void gameResultOn()
    {
        replayButton = GameObject.Find("Replay_Button").GetComponent<Button>();
        replayButton.onClick.AddListener (reloadScence);
    }

    void reloadScence()
    {
        SceneManager
            .LoadScene(SceneManager.GetActiveScene().name,
            LoadSceneMode.Single);
    }

    public void storeOpening()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void achiveOpening()
    {
        SceneManager.LoadScene("ACHIEVEMENT");
    }

    public void howToPlayOpening() //khuong
    {
        SceneManager.LoadScene("how to play");
    }

    public void leaderGlobalOpening() //khuong
    {
        SceneManager.LoadScene("Leaderboard Global");
    }

    public void leaderLocalOpening() //khuong
    {
        SceneManager.LoadScene("Leaderboard Local");
    }

    public void notificationOpening() //khuong
    {
        SceneManager.LoadScene("Notification");
    }

    public void backtoMainPage()
    {
        SceneManager.LoadScene(1);
    }

    public void openProfile()
    {
        SceneManager.LoadScene("Main screen");
    }

    public void gameplayOpening()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("gameplay");
    }
}
