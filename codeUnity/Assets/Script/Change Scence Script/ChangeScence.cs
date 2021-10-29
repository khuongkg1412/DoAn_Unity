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
        SceneManager.LoadScene(2);


    }

    public void backtoMainPage()
    {
        SceneManager.LoadScene(0);
    }

    public void bopenProfile()
    {
        SceneManager.LoadScene(7);
    }
}
