using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

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
        avatar_Player = "",
        coin_Player = 0,
        diamond_Player = 0,
        energy_Player = 5,
        gender_Player = 0,
        level_Player = 0,
        name_Player = "User01",
        stage_Player = 0,
        xp_Player = 0
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
    // Start is called before the first frame update
    void Start()
    {
        loadDataPlayer();
    }

    private void AddDataPlayer()
    {


        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        // DocumentReference doc = db.Collection("Player").Document();
        // doc.SetAsync(playerExample);

        // DocumentReference doc = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol").Collection("Inventory_Player").Document();
        // doc.SetAsync(inventory_Player);
        DocumentReference doc = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol").Collection("SystemNotification").Document();
        doc.SetAsync(systemNotification);

        doc = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol").Collection("Friend_Player").Document();
        doc.SetAsync(friend_Player);

        doc = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol").Collection("Achievement_Player").Document();
        doc.SetAsync(achievement_Player);

        doc = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol").Collection("Notification_Player").Document();
        doc.SetAsync(notification_Player);

    }
    private void AddDataAchievement()
    {

        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        db.Collection("Achievement").AddAsync(achievementStruct);
        db.Collection("Achievement").AddAsync(achievementStruct1);
        db.Collection("Achievement").AddAsync(achievementStruct2);
        db.Collection("Achievement").AddAsync(achievementStruct3);
    }
    private void AddDataSystemNotification()
    {

        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        db.Collection("SystemNotification").AddAsync(systemNotification1);
    }
    private void loadDataPlayer()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        // Query itemDailyQuery = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol");
        DocumentReference doc = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol");
        doc.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                var snapshot = task.Result;
                if (snapshot.Exists)
                {
                    PlayerStruct playerStruct = snapshot.ConvertTo<PlayerStruct>();
                }
            });
    }
    private void loadDataAchiement()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        // Query itemDailyQuery = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol");
        DocumentReference doc = db.Collection("Player").Document("nZ1TJd9hBrL6Kb9MEQol");
        doc.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                var snapshot = task.Result;
                if (snapshot.Exists)
                {
                    PlayerStruct playerStruct = snapshot.ConvertTo<PlayerStruct>();
                }
            });
    }
}