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

    public void reloadScence(){
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
    IEnumerator reloading(){
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
        yield return null;
    }
    public void storeOpening()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void achiveOpening()
    {
        SceneManager.LoadScene(2);


    }

    public void howToPlayOpening() //khuong
    {
        SceneManager.LoadScene(3);
    }

    public void leaderGlobalOpening() //khuong
    {
        SceneManager.LoadScene(4);
    }

    public void leaderLocalOpening() //khuong
    {
        SceneManager.LoadScene(5);
    }

    public void notificationOpening() //khuong
    {
        SceneManager.LoadScene(6);
    }

    public void backtoMainPage()
    {
        SceneManager.LoadScene(0);
    }

    public void openProfile()
    {
        SceneManager.LoadScene(7);
    }
}
