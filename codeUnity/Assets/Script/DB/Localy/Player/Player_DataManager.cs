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
    public List<Notification_Struct> notification_Player = new List<Notification_Struct>();

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

    public void adding_Item(ItemStruct item)
    {
        //quantity of item
        float quanity = 0;
        //Get quantity of item if it is exist
        foreach (var i in inventory_Player)
        {
            //if not exist quantity is 0
            if (i.item.ContainsKey(item.name_Item))
            {
                quanity = i.item[item.name_Item];
            }
        }
        //adding item to inventory
        Inventory_Player invent = new Inventory_Player()
        {
            ID = item.ID,
            item = new Dictionary<string, float>(){
                {item.name_Item , ++quanity}
            }
        };
        inventory_Player.Add(invent);
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
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

    public void read_Notification(Notification_Struct currentNoti)
    {
        foreach (Notification_Struct noti in notification_Player)
        {
            if (noti.ID.Equals(currentNoti.ID))
            {
                noti.isRead_Notification = true;
                break;
            }
        }
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }


}
