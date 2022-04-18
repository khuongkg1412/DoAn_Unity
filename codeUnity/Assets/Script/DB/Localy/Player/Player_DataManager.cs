using System.Collections.Generic;
using System;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UI;

public class Player_DataManager : MonoBehaviour
{
    public static Player_DataManager Instance { get; private set; }

    public PlayerStruct Player = new PlayerStruct();

    public NumeralStruct stats = new NumeralStruct();

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
    public void timeofLastPlay()
    {
        //Savee the current system time as a string in the player prefs class
        PlayerPrefs.SetString("lastTimePlay", System.DateTime.Now.ToBinary().ToString());
    }
    public float calculateTimeLifeCountDown()
    {
        //Grab the old time from the player prefs as a long
        long temp = Convert.ToInt64(PlayerPrefs.GetString("lastTimePlay"));

        //Convert the old time from binary to a DataTime variable
        DateTime lastTime = DateTime.FromBinary(temp);
        DateTime currentTime = System.DateTime.Now;
        //float timeCount = currentTime - lastTime;
        TimeSpan different = currentTime.Subtract(lastTime);
        float timeHasBeenCounted = Mathf.Round(float.Parse(different.TotalSeconds.ToString()));
        float totalTimeWait = (6 - Player.level.life) * 60;
        if (totalTimeWait - timeHasBeenCounted >= 0)
        {
            calculateLifeRestored(timeHasBeenCounted);
            return totalTimeWait - timeHasBeenCounted;
        }
        else
        {
            fullyRestoreLife();
        }
        return 0;
    }
    void calculateLifeRestored(float timeHasBeenCounted)
    {
        float numberLifeRestored = timeHasBeenCounted / 60;
        Player.level.life += Mathf.RoundToInt(numberLifeRestored);
        Debug.Log("Restore number_" + Mathf.RoundToInt(numberLifeRestored));
    }
    public void updateBuffInInventory(ItemStruct itemBuff, int quanity)
    {
        //update number of buff after playing game
        foreach (var i in inventory_Player)
        {
            if (i.ID == itemBuff.ID)
            {
                if (quanity == 0)
                {
                    //delete crafted piece out player inventory
                    Instance.inventory_Player.Remove(i);
                    Player_Update.deleteItem(Player.ID, i.ID);
                }
                else
                {
                    i.quantiy = quanity;
                }
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

    public void updateStatPlayer()
    {
        ItemStruct suit = Item_DataManager.Instance.Item.Find(r => r.ID == Instance.Player.currentOutfit.currentSuit);
        ItemStruct accessory = Item_DataManager.Instance.Item.Find(r => r.ID == Instance.Player.currentOutfit.currentAccesory);
        ItemStruct gun = Item_DataManager.Instance.Item.Find(r => r.ID == Instance.Player.currentOutfit.currentGun);

        stats = new NumeralStruct()
        {
            ATK_Numeral = suit.numeral_Item.ATK_Numeral + accessory.numeral_Item.ATK_Numeral + gun.numeral_Item.ATK_Numeral,
            DEF_Numeral = suit.numeral_Item.DEF_Numeral + accessory.numeral_Item.DEF_Numeral + gun.numeral_Item.DEF_Numeral,
            HP_Numeral = suit.numeral_Item.HP_Numeral + accessory.numeral_Item.HP_Numeral + gun.numeral_Item.HP_Numeral,
            SPD_Numeral = suit.numeral_Item.SPD_Numeral + accessory.numeral_Item.SPD_Numeral + gun.numeral_Item.SPD_Numeral,
            ATKSPD_Numeral = suit.numeral_Item.ATKSPD_Numeral + accessory.numeral_Item.ATKSPD_Numeral + gun.numeral_Item.ATKSPD_Numeral,

        };
    }

    public void updateCoinConcurrency(float amountUpdate)
    {
        Player.concurrency.Coin += Mathf.Round(amountUpdate);
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }
    public void updateDiamondConcurrency(float amountUpdate)
    {
        Player.concurrency.Diamond += Mathf.Round(amountUpdate);
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }


    public void adding_Item(ItemStruct item, int quantityBuy)
    {
        bool isFound = false;
        //Find Item in the invent player
        foreach (var i in Instance.inventory_Player)
        {
            if (item.ID.Equals(i.ID))
            {
                i.quantiy += quantityBuy;
                isFound = true;
            }
        }
        //In case cannot found in exist invent, create new 
        if (isFound == false)
        {
            //adding item to inventory
            Inventory_Player invent = new Inventory_Player()
            {
                ID = item.ID,
                quantiy = quantityBuy
            };
            //Add to Invent
            Instance.inventory_Player.Add(invent);
        }
        //Call to update the information off Player
        Player_Update.UpdatePlayer();
    }
    public void finishTheStage(float coin, float diamond, int stage)
    {
        //Up the stage if the stage has been complete is greather
        if (stage == Player.level.stage)
        {
            Player.level.stage += 1;
        }
        //Calculate the exp point by the score the player get
        player_LevelUP(Mathf.Round(coin));
        //Plus the coin by the score the player get
        updateCoinConcurrency(Mathf.Round(coin));
        //Plus the Diamond by the score the player get
        updateDiamondConcurrency(Mathf.Round(diamond));
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

    public void changeOutfit(int typeOfOutfit, string ID)
    {
        if (typeOfOutfit == 0) Player.currentOutfit.currentSuit = ID;
        else if (typeOfOutfit == 1) Player.currentOutfit.currentAccesory = ID;
        else if (typeOfOutfit == 2) Player.currentOutfit.currentGun = ID;

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

    public void addFriend(PlayerStruct friend)
    {
        Friend_Player newfriend_Player = new Friend_Player()
        {
            accept_Friend = true,
            friendID = friend.ID
        };
        Friend_Player i = Instance.friend_Player.Find(r => r.friendID == friend.ID);
        if (i == null) Instance.friend_Player.Add(newfriend_Player);
        Player_Update.UpdatePlayer();
    }

    public void CraftItem(string outfitID, Inventory_Player piece)
    {
        Inventory_Player i = Instance.inventory_Player.Find(r => r.ID == piece.ID);
        if (i != null)
        {
            if (i.quantiy == 3)
            {
                //delete crafted piece out player inventory
                Instance.inventory_Player.Remove(piece);
                Player_Update.deleteItem(Player.ID, piece.ID);
            }
            else
            {
                i.quantiy -= 3;
            }

            //adding item to inventory
            Inventory_Player invent = new Inventory_Player()
            {
                ID = outfitID,
                quantiy = 1
            };

            //Add to Invent
            Instance.inventory_Player.Add(invent);
        }

        Player_Update.UpdatePlayer();
    }
    public void decreaseLife()
    {
        Player.level.life -= 1;
        if (Player.level.life <= 0)
        {
            Player.level.life = 0;
        }
        Player_Update.UpdatePlayer();
    }
    public void increaseLife()
    {
        Player.level.life += 1;
        if (Player.level.life >= 6)
        {
            Player.level.life = 6;
        }
        Player_Update.UpdatePlayer();
    }
    public void fullyRestoreLife()
    {
        Player.level.life = 6;
        Player_Update.UpdatePlayer();
    }
}
