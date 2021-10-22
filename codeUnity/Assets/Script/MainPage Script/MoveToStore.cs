using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToStore : MonoBehaviour
{
    public void storeOpening()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void backtoMainPage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
