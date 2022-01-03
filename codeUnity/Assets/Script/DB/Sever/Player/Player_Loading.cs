using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class Player_Loading : MonoBehaviour
{
    private bool isDonePlayer = false, isDoneInvent = false, isDoneAchive = false, isDoneSystemNoti = false, isDoneFriend = false, isDoneNotification = false;

    private void Start()
    {
        StartCoroutine(LoadingDataFromSever());
    }

    void loadingPlayer()
    {
        isDonePlayer = false;
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        string IDPlayer = "7xv28G3fCIf2UoO0rV2SFV5tTr62";//AuthController.ID;
        if (IDPlayer == null) IDPlayer = FacebookManager.ID;

        DocumentReference docRef = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62");
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {

            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Player_DataManager.Instance.Player = snapshot.ConvertTo<PlayerStruct>();
            }
            else
            {
                Debug.LogError("loadingPlayer Error");
            }
            isDonePlayer = true;
        });

    }
    private void loadDataInvetory()
    {
        isDoneInvent = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Inventory_Player");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Inventory_Player objectData = documentSnapshot.ConvertTo<Inventory_Player>();
                Player_DataManager.Instance.inventory_Player.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataInvetory Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataInvetory Faulted");
            }
            isDoneInvent = true;
        });
    }
    private void loadDataAchievement()
    {
        isDoneAchive = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Achievement");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Achievement_Player objectData = documentSnapshot.ConvertTo<Achievement_Player>();
                Player_DataManager.Instance.achievement_Player.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataAchievement Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataAchievement Faulted");
            }
            isDoneAchive = true;
        });
    }
    private void loadDataSystemNotification()
    {
        isDoneSystemNoti = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("SystemNotification");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                SystemNotification objectData = documentSnapshot.ConvertTo<SystemNotification>();
                Player_DataManager.Instance.systemNotification.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataSystemNotification Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataSystemNotification Faulted");
            }
            isDoneSystemNoti = true;
        });
    }

    private void loadDataFriend()
    {
        isDoneFriend = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Query allCitiesQuery = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Friend_Player");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Friend_Player objectData = documentSnapshot.ConvertTo<Friend_Player>();
                Player_DataManager.Instance.friend_Player.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("Friend_Player Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Friend_Player Faulted");
            }
            isDoneFriend = true;
        });
    }

    private void loadDataNotification()
    {
        isDoneNotification = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Player").Document("7xv28G3fCIf2UoO0rV2SFV5tTr62").Collection("Notification_Player");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Notification_Player objectData = documentSnapshot.ConvertTo<Notification_Player>();
                Player_DataManager.Instance.notification_Player.Add(objectData);
            }
            if (task.IsCanceled)
            {
                Debug.LogError("Notification_Player Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Notification_Player Faulted");
            }
            isDoneNotification = true;
        });
    }

    IEnumerator LoadingDataFromSever()
    {
        yield return new WaitUntil(() => Player_DataManager.Instance != null);
        loadingPlayer();
        loadDataAchievement();
        loadDataFriend();
        loadDataInvetory();
        loadDataNotification();
        loadDataSystemNotification();

        yield return new WaitUntil(() => isDonePlayer && isDoneAchive && isDoneFriend && isDoneInvent && isDoneNotification && isDoneSystemNoti);
        yield return null;
    }
}