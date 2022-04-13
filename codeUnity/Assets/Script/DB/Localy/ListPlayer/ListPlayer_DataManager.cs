using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPlayer_DataManager : MonoBehaviour
{
    public static ListPlayer_DataManager Instance { get; private set; }

    public List<PlayerStruct> listPlayer = new List<PlayerStruct>();


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
    public bool checkContainsInListPlayer(PlayerStruct item)
    {
        bool isContain = false;
        listPlayer.ForEach(x =>
        {
            if (x.ID.Equals(item.ID))
            {
                isContain = true;
            }
        });
        return isContain;
    }
    public List<PlayerStruct> returnListPlayerSortByLevel()
    {
        //Sort by order of Citizen saved descending
        listPlayer.Sort((p1, p2) => p1.level.level.CompareTo(p2.level.level));
        listPlayer.Reverse();
        return listPlayer;
    }
    public List<PlayerStruct> returnListPlayerSortBySavedCitizen()
    {
        //Sort by order of Citizen saved descending
        listPlayer.Sort((p1, p2) => p1.statistic["Citizen_Saved"].CompareTo(p2.statistic["Citizen_Saved"]));
        listPlayer.Reverse();
        return listPlayer;
    }

}
