using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScence : MonoBehaviour
{
    public void storeOpening()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }


    public void achiveOpening()
    {
        SceneManager.LoadScene(5);


    }

    public void howToPlayOpening() //khuong
    {
        SceneManager.LoadScene(6);
    }

    public void leaderGlobalOpening() //khuong
    {
        SceneManager.LoadScene(7);
    }

    public void leaderLocalOpening() //khuong
    {
        SceneManager.LoadScene(8);
    }

    public void notificationOpening() //khuong
    {
        SceneManager.LoadScene(6);
    }

    public void backtoMainPage()
    {
        SceneManager.LoadScene(3);
    }

    public void bopenProfile()
    {
        SceneManager.LoadScene(10);
    }
}
