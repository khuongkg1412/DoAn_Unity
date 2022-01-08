using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_DataManager : MonoBehaviour
{
    public static Achievement_DataManager Instance { get; private set; }

    public Dictionary<AchievementStruct, bool> Achievement = new Dictionary<AchievementStruct, bool>();

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
            foreach (var item in Achievement_DataManager.Instance.Achievement)
            {
                Debug.Log("Running");
                // Call Method base on APICaLL.Mehtod 
                switch (item.Key.APICall.APIMethod)
                {
                    case "Save_CitizenMethod":
                        Save_CitizenMethod(item.Key);
                        break;
                    case "Kill_VirusMethod":
                        Kill_VirusMethod(item.Key);
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

    void Save_CitizenMethod(AchievementStruct item)
    {
        Debug.Log("Save_CitizenMethod");
        //get goal that need to unlock achievement
        float goal = item.APICall.goal;
        //Total citizen that player save
        float saveCitizen = Player_DataManager.Instance.Player.statistic["Citizen_Saved"];
        //If player is reach the goal then unlock the achievement
        if (saveCitizen >= goal)
        {
            Achievement.Remove(item);
            Achievement.Add(item, true);
        }
    }
    void Kill_VirusMethod(AchievementStruct item)
    {
        Debug.Log("Kill_VirusMethod");
        //get goal that need to unlock achievement
        float goal = item.APICall.goal;
        //Get player object
        PlayerStruct player = Player_DataManager.Instance.Player;
        //Total virus that player killed
        float killedVirus = player.statistic["VirusA_Killed"] + player.statistic["VirusB_Killed"] + player.statistic["VirusC_Killed"] + player.statistic["VirusD_Killed"];
        //If player is reach the goal then unlock the achievement
        if (killedVirus >= goal)
        {
            Achievement.Remove(item);
            Achievement.Add(item, true);
        }
    }
}

