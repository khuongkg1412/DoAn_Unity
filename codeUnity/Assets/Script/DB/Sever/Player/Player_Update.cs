using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class Player_Update
{
    public static void UpdatePlayer()
    {
        string IDPlayer = AuthController.ID;
        if (IDPlayer == null) IDPlayer = FacebookManager.ID;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        DocumentReference doc = db.Collection("Player").Document(IDPlayer);
        doc.SetAsync(Player_DataManager.Instance.Player);

        foreach (var i in Player_DataManager.Instance.inventory_Player)
        {
            doc = db.Collection("Player").Document(IDPlayer).Collection("Inventory_Player").Document(i.ID);
            doc.SetAsync(i);
        }
        foreach (var i in Player_DataManager.Instance.systemNotification)
        {
            doc = db.Collection("Player").Document(IDPlayer).Collection("SystemNotification").Document();
            doc.SetAsync(i);
        }
        foreach (var i in Player_DataManager.Instance.friend_Player)
        {
            doc = db.Collection("Player").Document(IDPlayer).Collection("Friend_Player").Document(i.friendID);
            doc.SetAsync(i);
        }
        foreach (var i in Player_DataManager.Instance.achivementReceived_Player)
        {
            doc = db.Collection("Player").Document(IDPlayer).Collection("Achivement_Player").Document(i.ID);
            doc.SetAsync(i);
        }

    }

}
