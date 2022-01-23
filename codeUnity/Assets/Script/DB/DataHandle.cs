using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using Firebase.Storage;
using UnityEngine.UI;

public class DataHandle : MonoBehaviour
{
    /*
    Player
    */
    Inventory_Player inventory_Player = new Inventory_Player()
    {
        item = new Dictionary<string, float>
        {
            {"item", 0 }
        }
    };
    SystemNotification systemNotification = new SystemNotification()
    {
        status_Notification = false
    };
    Friend_Player friend_Player = new Friend_Player()
    {
        accept_Friend = false,
        friendID = "MINSaf0TpgEPzy5JtEUM"
    };

    PlayerStruct newPlayer = new PlayerStruct()
    {
        generalInformation = new GeneralInformation_Player
        {
            username_Player = "Hai Long",
            avatar_Player = "PlayerAvatar/Avatar item.png",
            gender_Player = 0
        },
        concurrency = new Concurrency
        {
            Coin = 0,
            Diamond = 0
        },
        numeral = new NumeralStruct
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 10,
            SPD_Numeral = 300
        },
        level = new Level
        {
            currentXP = 0,
            reachXP = 130,
            level = 0,
            stage = 0,
            life = 6
        },
        statistic = new Dictionary<string, float>
        {
            {"VirusA_Killed",0},
            {"VirusB_Killed",0},
            {"VirusC_Killed",0},
            {"VirusD_Killed",0},
            {"Citizen_Saved",0}
        }
    };

    /*
     Achievement
    */
    AchievementStruct achievementStruct = new AchievementStruct()
    {
        title_Achievement = "Kills 10 Virus",
        APICall = new APICall_Achievement()
        {
            APIMethod = "Kill_VirusMethod",
            goal = 10
        },
        concurrency = new Concurrency
        {
            Coin = 0,
            Diamond = 30
        }
    };
    AchievementStruct achievementStruct1 = new AchievementStruct()
    {
        title_Achievement = "Save 5 citizens.",
        APICall = new APICall_Achievement
        {
            APIMethod = "Save_CitizenMethod",
            goal = 5
        },
        concurrency = new Concurrency()
        {
            Coin = 0,
            Diamond = 20
        }
    };
    AchievementStruct achievementStruct2 = new AchievementStruct()
    {
        title_Achievement = "Kills 50 Virus",
        APICall = new APICall_Achievement
        {
            APIMethod = "Kill_VirusMethod",
            goal = 50
        },
        concurrency = new Concurrency()
        {
            Coin = 0,
            Diamond = 30
        }
    };
    AchievementStruct achievementStruct3 = new AchievementStruct()
    {
        title_Achievement = "Save 10 citizen.",
        APICall = new APICall_Achievement
        {
            APIMethod = "Save_CitizenMethod",
            goal = 10

        },
        concurrency = new Concurrency()
        {
            Coin = 0,
            Diamond = 50
        }
    };
    /*
        SystemNotification data
    */
    Notification_Struct notificationSystem = new Notification_Struct()
    {
        content_Notification = "Welcome to our new game. Wish you could enjoy happily this.",
        title_Notification = "Welcome to Covid Refuse",
        isRead_Notification = false,
        receivedID_Notification = "7xv28G3fCIf2UoO0rV2SFV5tTr62",
        sentID_Notification = "System_Notification",
        type_Notification = (int)Notification.SystemUpdate_Notification
    };
    Notification_Struct notificationSocial = new Notification_Struct()
    {
        content_Notification = "Welcome to our new game. Wish you could enjoy happily this.",
        title_Notification = "Welcome to Covid Refuse",
        isRead_Notification = false,
        receivedID_Notification = "MINSaf0TpgEPzy5JtEUM",
        sentID_Notification = "7xv28G3fCIf2UoO0rV2SFV5tTr62",
        type_Notification = (int)Notification.Social_Notifacation
    };
    Notification_Struct notificationFriend = new Notification_Struct()
    {
        content_Notification = "Welcome to our new game. Wish you could enjoy happily this.",
        title_Notification = "Welcome to Covid Refuse",
        isRead_Notification = false,
        receivedID_Notification = "zDIDUJhwrIgHvXuohXdN",
        sentID_Notification = "7xv28G3fCIf2UoO0rV2SFV5tTr62",
        type_Notification = (int)Notification.Friend_Notification
    };

    public static List<AchievementStruct> listAchievement = new List<AchievementStruct>();
    public static PlayerStruct playerData;

    public Text Coin, Diamond, Life, Name;
    public Image avatar;
    float timeGetUpdate = 0f;
    private void Start()
    {
        //TestFireBase();
    }

    private void Update()
    {
        update_Information();
    }
    void update_Information()
    {
        timeGetUpdate += Time.deltaTime;
        if (timeGetUpdate > 1f)
        {
            timeGetUpdate = 0;
            loadDataPlayerOnScence();
        }
    }
    void loadDataPlayerOnScence()
    {
        if (Player_DataManager.Instance.Player != null)
        {
            PlayerStruct player = Player_DataManager.Instance.Player;

            StartCoroutine(GetImage(player.generalInformation.avatar_Player));

            Coin.text = "" + player.concurrency.Coin;
            Diamond.text = "" + player.concurrency.Diamond;
            Life.text = "" + player.level.life + "/6";
            Name.text = "" + player.generalInformation.username_Player + "\n" + "LV." + player.level.level;
        }
    }
    IEnumerator GetImage(string dataImage)
    {

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataImage);

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 1 * 1024 * 1024;
        storageRef.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task =>
           {
               if (task.IsFaulted || task.IsCanceled)
               {
                   Debug.LogException(task.Exception);
               }
               else
               {
                   byte[] fileContents = task.Result;
                   Texture2D texture = new Texture2D(1, 1);
                   texture.LoadImage(fileContents);
                   Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                   avatar.sprite = sprite;
               }
           });
        yield return null;
    }

    public void AdddingItem()
    {
        // Player_DataManager.Instance.adding_Item(Item_DataManager.Instance.Item[0]);
        // Debug.Log("Adding Item");
    }
}