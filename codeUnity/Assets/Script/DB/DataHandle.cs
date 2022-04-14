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
using System.Threading.Tasks;

public class DataHandle : MonoBehaviour
{
    [SerializeField] Text Coin, Diamond, Life, Name;
    [SerializeField] RawImage avatar;
    float timeGetUpdate = 0f;
    bool isLoadingDataDone = false;
    private void Update()
    {
        if (isLoadingDataDone)
        {
            loadDataPlayerOnScence();
        }
    }
    private void Start()
    {
        StartCoroutine(startLifeTimeCount());
        Achievement_DataManager.Instance.sortTheAchievement();
    }
    IEnumerator startLifeTimeCount()
    {
        yield return new WaitUntil(() => GameObject.Find("DB_LoadingData").GetComponent<Player_Loading>().loadDataAllDone());
        isLoadingDataDone = true;
        timeRemaining = Player_DataManager.Instance.calculateTimeLifeCountDown();
        countDownLifeTime();
        yield return 0;
    }
    float timeRemaining;
    [SerializeField] Text timeText;
    async void countDownLifeTime()
    {
        while (timeRemaining >= 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            timeRemaining -= 1;
            //Increase every 60s
            if (timeRemaining % 60 == 0)
            {
                Player_DataManager.Instance.increaseLife();
            }
            DisplayTime(timeRemaining);
        }
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
    void loadDataPlayerOnScence()
    {
        if (Player_DataManager.Instance.Player != null)
        {
            PlayerStruct player = Player_DataManager.Instance.Player;
            avatar.texture = player.texture2D;
            Coin.text = "" + player.concurrency.Coin;
            Diamond.text = "" + player.concurrency.Diamond;
            Life.text = "" + player.level.life + "/6";
            Name.text = "" + player.generalInformation.username_Player + "\n" + "LV." + player.level.level;
        }
    }
}