using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Pause : MonoBehaviour
{
    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
