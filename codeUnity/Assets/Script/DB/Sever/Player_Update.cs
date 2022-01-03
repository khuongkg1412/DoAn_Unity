using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class Player_Update : MonoBehaviour
{
    private void UpdatePlayer(Player_DataManager data)
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        // DocumentReference doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62");
        // doc.SetAsync(playerExample);

        // doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Inventory_Player").Document();
        // doc.SetAsync(inventory_Player);

        // doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("SystemNotification").Document();
        // doc.SetAsync(systemNotification);

        // doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Friend_Player").Document();
        // doc.SetAsync(friend_Player);

        // doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Achievement_Player").Document();
        // doc.SetAsync(achievement_Player);

        // doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Notification_Player").Document();
        // doc.SetAsync(notification_Player);

    }

}
