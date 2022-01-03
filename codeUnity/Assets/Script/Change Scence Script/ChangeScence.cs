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

    public void gameplayOpening()
    {


        PlayerStruct player = SaveSystem.LoadDataPlayer();
        if (player.level_Player == 0)
        {
            Screen.orientation = ScreenOrientation.Landscape;
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene("StageList");
        }
    }

    IEnumerator addOtherCollection(string ID)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("Demo");
        doc.SetAsync(inventory_Player);

        doc = db.Collection("Player").Document(ID).Collection("SystemNotification").Document("Demo");
        doc.SetAsync(systemNotification);

        doc = db.Collection("Player").Document(ID).Collection("Friend_Player").Document("Demo");
        doc.SetAsync(friend_Player);

        doc = db.Collection("Player").Document(ID).Collection("Achievement_Player").Document("Demo");
        doc.SetAsync(achievement_Player);

        doc = db.Collection("Player").Document(ID).Collection("Notification_Player").Document("Demo");
        doc.SetAsync(notification_Player);

        yield return null;
    }
}

