using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScence : MonoBehaviour
{
    //Wait for reoading excutes
    private float waitToLoad;

    public void reloadScence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void storeOpening()
    {
        SceneManager.LoadScene("Store");
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
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainPage");
    }

    public void openProfile()
    {
        SceneManager.LoadScene("Main screen");
    }
    public void openLogin()
    {
        SceneManager.LoadScene("Login - Main theme");
    }

    public void gameplayOpening()
    {
        //PlayerStruct player = SaveSystem.LoadDataPlayer();
        if (Player_DataManager.Instance.Player.level.stage == 0)
        {
            Screen.orientation = ScreenOrientation.Landscape;
            foreach (var i in Item_DataManager.Instance.itemBuff())
            {
                if (i.name_Item.Equals("Heal"))
                {
                    Player_DataManager.Instance.playerCharacter.addBuff(i);
                    Debug.Log("Found" + i.name_Item);
                }
            }
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene("StageList");
        }
    }
}

