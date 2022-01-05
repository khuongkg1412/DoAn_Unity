using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_DataManager : MonoBehaviour
{
    public static Achievement_DataManager Instance { get; private set; }

    public Dictionary<string, AchievementStruct> Achievement = new Dictionary<string, AchievementStruct>();

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
}
