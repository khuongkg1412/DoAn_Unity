using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class Item_Update : MonoBehaviour
{
    public static void UpdateItem()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        foreach (var item in Item_DataManager.Instance.Item)
        {
            //Get Collection And Document
            DocumentReference doc = db.Collection("Achievement").Document(item.ID);
            doc.SetAsync(item);
        }

    }
}
