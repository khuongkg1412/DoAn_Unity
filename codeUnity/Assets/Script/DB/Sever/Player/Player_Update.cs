using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class Player_Update
{
    public static void UpdatePlayer()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        DocumentReference doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62");
        doc.SetAsync(Player_DataManager.Instance.Player);

        foreach (var i in Player_DataManager.Instance.inventory_Player)
        {
            doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Inventory_Player").Document(i.ID);
            doc.SetAsync(i);
        }
        foreach (var i in Player_DataManager.Instance.systemNotification)
        {
            doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("SystemNotification").Document();
            doc.SetAsync(i);
        }
        foreach (var i in Player_DataManager.Instance.friend_Player)
        {
            doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Friend_Player").Document();
            doc.SetAsync(i);
        }
        // foreach (var i in Player_DataManager.Instance.notification_Player)
        // {
        //     doc = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Notification_Player").Document();
        //     doc.SetAsync(i);
        // }

    }

}
