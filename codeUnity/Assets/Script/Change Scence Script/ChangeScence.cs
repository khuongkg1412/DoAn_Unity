using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScence : MonoBehaviour
{
    //Reload the scence cause you lost the game
    public bool Reloading = false;

    //Wait for reoading excutes
    private float waitToLoad;

    public void reloadScence()
    {
        // while(true)
        // {
        //     waitToLoad += Time.deltaTime;
        //     if (waitToLoad >= 5f)
        //     {
        //         SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
        //         break;
        //     }
        // }
        StartCoroutine(reloading());
    }

    IEnumerator reloading()
    {
        yield return new WaitForSeconds(2);
        SceneManager
            .LoadScene(SceneManager.GetActiveScene().name,
            LoadSceneMode.Single);
        yield return null;
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
