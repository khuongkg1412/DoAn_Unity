using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using Firebase.Storage;
using UnityEngine.UI;

public class DataHandle : MonoBehaviour
{
    public Text Coin, Diamond, Life, Name;
    public Image avatar;
    float timeGetUpdate = 0f;
    private void Update()
    {
        update_Information();
    }
    private void Start()
    {
        countDownLifeTime();
    }
    float timeRemaining;
    [SerializeField] Text timeText;
    void countDownLifeTime()
    {
        timeRemaining = Player_DataManager.Instance.calculateTimeLifeCountDown();

        Debug.Log("timeRemaining_" + timeRemaining);

        DisplayTime(timeRemaining);
    }
    //Method display time
    void DisplayTime(float timeToDisplay)
    {   //Increase time by 1
        timeToDisplay += 1;
        //Convert to minut and second
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //Set to text
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void update_Information()
    {
        timeGetUpdate += Time.deltaTime;
        if (timeGetUpdate > 1f)
        {
            timeGetUpdate = 0;
            loadDataPlayerOnScence();
        }
    }
    void loadDataPlayerOnScence()
    {
        if (Player_DataManager.Instance.Player != null)
        {
            PlayerStruct player = Player_DataManager.Instance.Player;

            StartCoroutine(GetImage(player.generalInformation.avatar_Player));

            Coin.text = "" + player.concurrency.Coin;
            Diamond.text = "" + player.concurrency.Diamond;
            Life.text = "" + player.level.life + "/6";
            Name.text = "" + player.generalInformation.username_Player + "\n" + "LV." + player.level.level;
        }
    }
    IEnumerator GetImage(string dataImage)
    {

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataImage);

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 1 * 1024 * 1024;
        storageRef.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task =>
           {
               if (task.IsFaulted || task.IsCanceled)
               {
                   Debug.LogException(task.Exception);
               }
               else
               {
                   byte[] fileContents = task.Result;
                   Texture2D texture = new Texture2D(1, 1);
                   texture.LoadImage(fileContents);
                   Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                   avatar.sprite = sprite;
               }
           });
        yield return null;
    }
}