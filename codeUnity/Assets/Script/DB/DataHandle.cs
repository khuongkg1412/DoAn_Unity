using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

public class DataHandle : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        loadData();
    }

    private void AddData()
    {
        Debug.Log("Database Added");

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

    private void loadData()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Query itemDailyQuery =
            db.Collection("KhongCoTonTai");
        itemDailyQuery
            .GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Debug.Log("Task: " + task.Result);
                QuerySnapshot allItemQuerySnapshot = task.Result;
                foreach (DocumentSnapshot
                    documentSnapshot
                    in
                    allItemQuerySnapshot.Documents
                )
                {

                }
            });
    }
}
