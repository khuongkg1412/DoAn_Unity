using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Pause : MonoBehaviour
{
    public GameObject

            resumeButton,
            pauseButton;

    public GameObject pausePannel;

    public void pauseGame()
    {
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        pausePannel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        pausePannel.SetActive(false);
        Time.timeScale = 1f;
    }
}
