using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class Player_DataManager : MonoBehaviour
{
    public static Player_DataManager Instance { get; private set; }

    public PlayerStruct Player = new PlayerStruct();

    public Character playerCharacter;
    public List<Inventory_Player> inventory_Player = new List<Inventory_Player>();
    public List<SystemNotification> systemNotification = new List<SystemNotification>();
    public List<Friend_Player> friend_Player = new List<Friend_Player>();
    public List<Notification_Struct> notification_Player = new List<Notification_Struct>();
    public List<AchievementStruct> achivementReceived_Player = new List<AchievementStruct>();

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
    public void updateBuffInInventory(ItemStruct itemBuff, float quanity)
    {
        //update number of buff after playing game
        foreach (var i in inventory_Player)
        {
            if (i.ID == itemBuff.ID)
            {
                Debug.Log("Item Edit is " + itemBuff.name_Item + " with quantiy " + quanity);
                i.item = new Dictionary<string, float>{
                    {itemBuff.name_Item,quanity}
                };
                // i.quantiy = quanity;
            }
        }
        //inventory_Player.Find(x => x.ID == itemBuff.ID).item[itemBuff.name_Item] = quanity;
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }
    public void settingCharacter(NumeralStruct numeralStruct)
    {
        this.playerCharacter = new Character(numeralStruct);
    }

    public void updateCoinConcurrency(float amountUpdate)
    {
        Player.concurrency.Coin += amountUpdate;
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }
    public void updateDiamondConcurrency(float amountUpdate)
    {
        Player.concurrency.Diamond += amountUpdate;
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
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
                Instance.inventory_Player[i].item = new Dictionary<string, float>() { { item.name_Item, quanity } };
                isFound = true;
            }
        }
        if (isFound == false)
        {
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
    public void finishTheStage(float score, float stage, bool isCompleted)
    {
        //Up the stage if the stage has been complete is greather
        if (stage == Player.level.stage & isCompleted)
        {
            Player.level.stage += 1;
        }
        //Calculate the exp point by the score the player get
        player_LevelUP(score);
        //Plus the coin by the score the player get
        updateCoinConcurrency(score);
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }
    public void player_LevelUP(float xpGet)
    {
        float totalExp = xpGet + Player.level.currentXP;
        while (totalExp - Player.level.reachXP > 0)
        {
            float expNeeded = Player.level.reachXP - Player.level.currentXP;
            totalExp -= expNeeded;

            //Increase level
            Player.level.level += 1;
            //Calculate the new reach Xp
            Player.level.reachXP = 100 * Player.level.level * (float)1.3;
            //Set currento 0 after level up
            Player.level.currentXP = 0;
        }
        Player.level.currentXP += totalExp;



        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }


    public void player_GetRewardAchievement(AchievementStruct achieve)
    {
        //Add concurrency by checking which concurrency is more than 0 to add into concurrency
        Concurrency rewardReceived = achieve.concurrency;
        if (rewardReceived.Coin > 0 && rewardReceived.Diamond <= 0)
        {
            updateCoinConcurrency(rewardReceived.Coin);
        }
        else
        {
            updateDiamondConcurrency(rewardReceived.Diamond);
        }
        // Add to received achievement of player
        achivementReceived_Player.Add(achieve);
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

    public void changeAvatar(string path)
    {
        Player.generalInformation.avatar_Player = path;
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }
    public void changeName(string newName)
    {
        Player.generalInformation.username_Player = newName;
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }

    public void updateStatistic(float citizenSaved, float virusKilled)
    {
        //Citizen Saved and virus killed
        citizenSaved += Player.statistic["Citizen_Saved"];
        virusKilled += Player.statistic["Virus_Kill"];

        Player.statistic = new Dictionary<string, float>()
        {
            {"Citizen_Saved" ,citizenSaved},
             {"Virus_Kill" ,virusKilled}
        };
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }
    public void SendLiferequest(string FriendId)
    {
        Notification_Struct lifeRequest = new Notification_Struct()
        {
            content_Notification = "Help, I'm running out of energy.",
            title_Notification = "Help me",
            isRead_Notification = false,
            receivedID_Notification = FriendId,
            sentID_Notification = Player.ID,
            type_Notification = (int)Notification.Friend_Notification
        };

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        DocumentReference doc = db.Collection("Notifcation").Document();
        doc.SetAsync(lifeRequest);
    }


}
