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
    public List<Notification_Player> notification_Player = new List<Notification_Player>();

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

    public void player_LevelUP(float xpGet)
    {
        Player.level.currentXP += xpGet;

        if (Player.level.currentXP >= Player.level.reachXP)
        {
            Player.level.currentXP = 0;
            Player.level.reachXP = 100 * Player.level.level * (float)1.3;
            Debug.Log("Level Need to Reach " + Player.level.reachXP);
        }
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }



}
