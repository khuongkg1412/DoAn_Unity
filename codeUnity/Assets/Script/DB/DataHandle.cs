using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System.IO;
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
        quantity = 0
    };
    SystemNotification systemNotification = new SystemNotification()
    {
        status_Notification = false
    };
    Friend_Player friend_Player = new Friend_Player()
    {
        accept_Friend = false,
        notificationID = false
    };
    Achievement_Player achievement_Player = new Achievement_Player()
    {
        achived_Player = true,
        progress_Player = 0
    };
    Notification_Player notification_Player = new Notification_Player()
    {
        content_Notification = "This is the content of the first notification",
        sentID_Notification = "ID sent",
        status_Notification = false,
        title_Notification = "This is title of the first notification",
        type_Notification = 0
    };

    PlayerStruct playerExample = new PlayerStruct()
    {
        avatar_Player = "PlayerAvatar/Avatar item.png",
        coin_Player = 0,
        diamond_Player = 0,
        energy_Player = 6,
        gender_Player = 0,
        level_Player = 0,
        name_Player = "Khuong Meo",
        stage_Player = 0,
        xp_Player = 0,
        numeral_Player =
                    new NumeralStruct
                    {
                        ATK_Numeral = 10,
                        DEF_Numeral = 10,
                        HP_Numeral = 50,
                        SPD_Numeral = 150
                    }
    };

    /*
     Achievement
    */
    AchievementStruct achievementStruct = new AchievementStruct()
    {
        description_Achievement = "Kill totally 10 virus.",
        goal_Achievement = 10,
        rewardType_Achievement = 0,
        reward_Achievement = 100,
        title_Achievement = "Virus Slayer 1"
    };
    AchievementStruct achievementStruct1 = new AchievementStruct()
    {
        description_Achievement = "Save 5 citizens.",
        goal_Achievement = 5,
        rewardType_Achievement = 1,
        reward_Achievement = 20,
        title_Achievement = "Hero Path"
    };
    AchievementStruct achievementStruct2 = new AchievementStruct()
    {
        description_Achievement = "Kill totally 50 virus.",
        goal_Achievement = 50,
        rewardType_Achievement = 0,
        reward_Achievement = 500,
        title_Achievement = "Virus Slayer 2"
    };
    AchievementStruct achievementStruct3 = new AchievementStruct()
    {
        description_Achievement = "Save 10 citizen.",
        goal_Achievement = 10,
        rewardType_Achievement = 1,
        reward_Achievement = 50,
        title_Achievement = "Hero Journey"
    };
    /*
        SystemNotification data
    */
    SystemNotification_Struct systemNotification1 = new SystemNotification_Struct()
    {
        content_SystemNotification = "Welcome to our new game. Wish you could enjoy happily this.",
        title_SystemNotification = "Welcome to Covid Refuse"
    };


    public static List<AchievementStruct> listAchievement = new List<AchievementStruct>();
    public static PlayerStruct playerData;

    public Text Coin, Diamond, Life, Name;
    public Image avatar;
    float timeGetUpdate = 0f;

    private void Update()
    {
        TestFireBase();
    }
    private void TestFireBase()
    {

        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        DocumentReference docRef = db.Collection("data").Document("one");
        Dictionary<string, object> docData = new Dictionary<string, object>
    {
        { "stringExample", "Hello World" },
        { "booleanExample", false },
        { "numberExample", 3.14159265 },
        { "nullExample", null },
        { "arrayExample", new List<object>() { 5, true, "Hello" } },
        { "objectExample", new Dictionary<string, object>
                {
                        { "a", 5 },
                        { "b", true },
                }
        },
    };

        docRef.SetAsync(docData);
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

        PlayerStruct player = Player_DataManager.Instance.Player;

        StartCoroutine(GetImage(player.avatar_Player));

        Coin.text = "" + player.coin_Player;
        Diamond.text = "" + player.diamond_Player;
        Life.text = "" + player.energy_Player + "/6";
        Name.text = "" + player.name_Player + "\n" + "LV." + player.level_Player;
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
                   //Populate(sprite, Name, level);
               }
           });
        yield return null;
    }

    private void UpdatePlayer()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        DocumentReference doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62");
        doc.SetAsync(playerExample);

        doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Inventory_Player").Document();
        doc.SetAsync(inventory_Player);

        doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("SystemNotification").Document();
        doc.SetAsync(systemNotification);

        doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Friend_Player").Document();
        doc.SetAsync(friend_Player);

        doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Achievement_Player").Document();
        doc.SetAsync(achievement_Player);

        doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Notification_Player").Document();
        doc.SetAsync(notification_Player);

    }

}