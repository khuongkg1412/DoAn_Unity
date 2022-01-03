using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DataManager : MonoBehaviour
{
    public static Player_DataManager Instance { get; private set; }

    public PlayerStruct Player;
    public List<Inventory_Player> inventory_Player = new List<Inventory_Player>();
    public List<SystemNotification> systemNotification = new List<SystemNotification>();
    public List<Friend_Player> friend_Player = new List<Friend_Player>();
    public List<Achievement_Player> achievement_Player = new List<Achievement_Player>();
    public List<Notification_Player> notification_Player = new List<Notification_Player>();

    // void SettingData(PlayerStruct Player, Inventory_Player inventory_Player, SystemNotification systemNotification, Friend_Player friend_Player, Achievement_Player achievement_Player, Notification_Player notification_Player)
    // {
    //     Instance.Player = Player;
    //     Instance.inventory_Player = inventory_Player;
    //     Instance.systemNotification = systemNotification;
    //     Instance.friend_Player = friend_Player;
    //     Instance.achievement_Player = achievement_Player;
    //     Instance.notification_Player = notification_Player;
    //}
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

    public void player_LevelUP()
    {
        Player.level_Player += 1;
    }


}
