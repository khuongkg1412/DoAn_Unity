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

    public void adding_Item(ItemStruct item, int quantityBuy)
    {
        //quantity of item
        float quanity = 0;
        bool isFound = false;
        for (int i = 0; i < Instance.inventory_Player.Count; i++)
        {
            if (Instance.inventory_Player[i].item.ContainsKey(item.name_Item))
            {
                quanity = Instance.inventory_Player[i].item[item.name_Item];
                quanity += quantityBuy;
                Debug.Log("Find");
                Instance.inventory_Player[i].item = new Dictionary<string, float>() { { item.name_Item, quanity } };
                isFound = true;
            }
        }
        if (isFound == false)
        {
            Debug.Log("Cannot Find");
            //adding item to inventory
            Inventory_Player invent = new Inventory_Player()
            {
                ID = item.ID,
                item = new Dictionary<string, float>() { { item.name_Item, quanity + quantityBuy } }
            };
            Instance.inventory_Player.Add(invent);
        }

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

    public void sent_Notification()
    {
        // string sentID = "asd";
        // Notification_Player notification_Player = new Notification_Player()
        // {
        //     content_Notification = "This is the content of the first notification",
        //     sentID_Notification = "ID sent",
        //     status_Notification = false,
        //     title_Notification = "This is title of the first notification",
        //     type_Notification = 0
        // };

    }


}
