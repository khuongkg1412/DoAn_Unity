using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_DataManager : MonoBehaviour
{
    public static Achievement_DataManager Instance { get; private set; }
    public List<AchievementStruct> Achievement = new List<AchievementStruct>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
     Consider the achievement is reach or not. The action is achieved by comparing Achievement goal and the static of player
    */
    public void Achievement_TrueOrFalse()
    {
        //If not null then do action
        if (Achievement_DataManager.Instance.Achievement != null)
        {
            for (int i = 0; i < Achievement.Count; i++)
            {
                // Call Method base on APICaLL.Mehtod 
                switch (Achievement[i].APICall.APIMethod)
                {
                    case "Save_CitizenMethod":
                        Save_CitizenMethod(Achievement[i], i);
                        break;
                    case "Kill_VirusMethod":
                        Kill_VirusMethod(Achievement[i], i);
                        break;
                    default:
                        Debug.Log("Default case Achievement data");
                        break;
                }
            }
        }
        else
        {
            Debug.LogError("The Achievement list is null");
        }
    }

    void Save_CitizenMethod(AchievementStruct item, int index)
    {
        //get goal that need to unlock achievement
        float goal = item.APICall.goal;
        //Total citizen that player save
        float saveCitizen = Player_DataManager.Instance.Player.statistic["Citizen_Saved"];
        //Percentage of complete achievement
        float percentage = saveCitizen / goal;
        //If player is reach the goal then unlock the achievement
        Achievement[index].percentage = percentage;
    }
    void Kill_VirusMethod(AchievementStruct item, int index)
    {
        //get goal that need to unlock achievement
        float goal = item.APICall.goal;
        //Get player object
        PlayerStruct player = Player_DataManager.Instance.Player;
        //Total virus that player killed
        //        float killedVirus = player.statistic["VirusA_Killed"] + player.statistic["VirusB_Killed"] + player.statistic["VirusC_Killed"] + player.statistic["VirusD_Killed"];
        float killedVirus = player.statistic["Virus_Kill"];
        //Percentage of complete achievement
        float percentage = killedVirus / goal;
        //If player is reach the goal then unlock the achievement
        Achievement[index].percentage = percentage;
    }
}

