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
        foreach (var i in Player_DataManager.Instance.notification_Player)
        {
            doc = db.Collection("Notifcation").Document(i.ID);
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
    public static void deleteItem(string ID_Player, string ID_Item)
    {
        //Call to update the information off Player
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference doc = db.Collection("Player").Document(ID_Player).Collection("Inventory_Player").Document(ID_Item);
        doc.DeleteAsync();
    }

}
