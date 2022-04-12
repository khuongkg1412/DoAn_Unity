using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine;

public class ListPlayer_Loading_SavedCitizen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        listPlayerOrderByCitizen();
    }
    void listPlayerOrderByCitizen()
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
                ListPlayer_DataManager.Instance.listPlayer2.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataItem Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataItem Faulted");
            }
            //Sort by order of Citizen saved descending
            ListPlayer_DataManager.Instance.listPlayer2.ForEach(item => StartCoroutine(GetImage(item)));
            ListPlayer_DataManager.Instance.listPlayer2.Sort((p1, p2) => p1.statistic["Citizen_Saved"].CompareTo(p2.statistic["Citizen_Saved"]));
            ListPlayer_DataManager.Instance.listPlayer2.Reverse();
        });

    }
    IEnumerator GetImage(PlayerStruct player)
    {

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(player.generalInformation.avatar_Player);

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
                   int index = ListPlayer_DataManager.Instance.listPlayer2.FindIndex(item => item.ID == player.ID);
                   ListPlayer_DataManager.Instance.listPlayer2[index].texture2D = texture;
               }
           });
        yield return null;
    }
}
