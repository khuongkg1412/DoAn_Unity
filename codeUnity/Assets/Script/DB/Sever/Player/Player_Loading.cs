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
                Player_DataManager.Instance.Player.ID = snapshot.Id;
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
        Debug.Log("ID Player" + Player_DataManager.Instance.Player.ID);
        Query allCitiesQuery = db.Collection("Notifcation")
         .WhereEqualTo("sentID_Notification", Player_DataManager.Instance.Player.ID)
        // .WhereEqualTo("receivedID_Notification", Player_DataManager.Instance.Player.ID)
        // .OrderBy("dateCreate")
        ;
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Notification_Struct objectData = documentSnapshot.ConvertTo<Notification_Struct>();
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
            else if (task.IsCompleted)
            {
                foreach (var item in Player_DataManager.Instance.notification_Player)
                {
                    Debug.Log(item.type_Notification);
                }
            }
            isDoneNotification = true;
        });
    }

    IEnumerator LoadingDataFromSever()
    {
        yield return new WaitUntil(() => Player_DataManager.Instance != null);
        loadingPlayer();
        yield return new WaitUntil(() => isDonePlayer);
        loadDataFriend();
        loadDataInvetory();
        loadDataNotification();
        loadDataSystemNotification();

        yield return new WaitUntil(() => isDoneAchive && isDoneFriend && isDoneInvent && isDoneNotification && isDoneSystemNoti);
        yield return null;
    }
}
