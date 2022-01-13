using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class ListPlayer_Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        listPlayerOrderByLevel();
    }

    void listPlayerOrderByLevel()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Player");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                PlayerStruct objectData = documentSnapshot.ConvertTo<PlayerStruct>();
                objectData.ID = documentSnapshot.Id;
                ListPlayer_DataManager.Instance.listPlayer.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataItem Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataItem Faulted");
            }
            //Sort by order of level descending
            ListPlayer_DataManager.Instance.listPlayer.Sort((p1, p2) => p1.level.level.CompareTo(p2.level.level));
            ListPlayer_DataManager.Instance.listPlayer.Reverse();
        });

    }
}
