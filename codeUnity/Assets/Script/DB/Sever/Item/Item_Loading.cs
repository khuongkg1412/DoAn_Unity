using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class Item_Loading : MonoBehaviour
{
    private bool isDoneItem = false;

    private void Start()
    {
        StartCoroutine(LoadingDataFromSever());
    }
    IEnumerator LoadingDataFromSever()
    {
        yield return new WaitUntil(() => Item_DataManager.Instance != null);

        loadDataItem();

        yield return new WaitUntil(() => isDoneItem);

    }

    private void loadDataItem()
    {
        isDoneItem = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Item");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                ItemStruct objectData = documentSnapshot.ConvertTo<ItemStruct>();
                objectData.ID = documentSnapshot.Id;
                Item_DataManager.Instance.Item.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataItem Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataItem Faulted");
            }
            isDoneItem = true;
        });
    }
}
