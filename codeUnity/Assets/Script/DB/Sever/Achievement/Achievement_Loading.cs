using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class Achievement_Loading : MonoBehaviour
{
    private bool isDoneAchieve = false;

    private void Start()
    {
        StartCoroutine(LoadingDataFromSever());
    }
    IEnumerator LoadingDataFromSever()
    {
        yield return new WaitUntil(() => Achievement_DataManager.Instance != null);

        loadDataAchievement();

        yield return new WaitUntil(() => isDoneAchieve);
        Achievement_DataManager.Instance.Achievement_TrueOrFalse();

    }

    private void loadDataAchievement()
    {
        isDoneAchieve = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Achievement");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                AchievementStruct objectData = documentSnapshot.ConvertTo<AchievementStruct>();
                objectData.ID = documentSnapshot.Id;
                Achievement_DataManager.Instance.Achievement.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataAchievement Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataAchievement Faulted");
            }
            isDoneAchieve = true;
        });
    }
}
