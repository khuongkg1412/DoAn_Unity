using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class Achievement_Update
{
    public static void UpdateAchievement()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        foreach (var item in Achievement_DataManager.Instance.Achievement)
        {
            //Get Collection And Document
            DocumentReference doc = db.Collection("Achievement").Document(item.Key.ID);
            doc.SetAsync(item.Key);
        }

    }
}
