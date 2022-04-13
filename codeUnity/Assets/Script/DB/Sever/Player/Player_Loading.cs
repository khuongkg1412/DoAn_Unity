using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase.Storage;

public class Player_Loading : MonoBehaviour
{
    private bool isDonePlayer = false, isDoneInvent = false, isDoneAchive = false, isDoneSystemNoti = false, isDoneFriend = false, isDoneNotification = false;
    string IDPlayer;

    private void Start()
    {
        StartCoroutine(LoadingDataFromSever());
        IDPlayer = AuthController.ID;
        if (IDPlayer == null) IDPlayer = FacebookManager.ID;
    }
    void loadingPlayer()
    {
        isDonePlayer = false;
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;


        DocumentReference docRef = db.Collection("Player").Document(IDPlayer);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {

            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Player_DataManager.Instance.Player = snapshot.ConvertTo<PlayerStruct>();
                Player_DataManager.Instance.Player.ID = snapshot.Id;
                Player_DataManager.Instance.settingCharacter(snapshot.ConvertTo<PlayerStruct>().numeral);
            }
            else
            {
                Debug.LogError("loadingPlayer Error");
                SceneManager.LoadScene("Create Character");
            }
            isDonePlayer = true;
            StartCoroutine(GetImage(Player_DataManager.Instance.Player));
        });

    }
    private void loadDataAchievement()
    {
        isDoneAchive = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Player").Document(IDPlayer).Collection("Achivement_Player");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                AchievementStruct objectData = documentSnapshot.ConvertTo<AchievementStruct>();
                objectData.ID = documentSnapshot.Id;
                if (!Player_DataManager.Instance.achivementReceived_Player.Exists(x => x.ID.Equals(objectData.ID)))
                {
                    Player_DataManager.Instance.achivementReceived_Player.Add(objectData);
                }
            }
            if (task.IsCanceled)
            {
                Debug.LogError("loadDataInvetory Error");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("loadDataInvetory Faulted");
            }
            isDoneAchive = true;
        });
    }
    private void loadDataInvetory()
    {
        isDoneInvent = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        Query allCitiesQuery = db.Collection("Player").Document(IDPlayer).Collection("Inventory_Player");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Inventory_Player objectData = documentSnapshot.ConvertTo<Inventory_Player>();
                objectData.ID = documentSnapshot.Id;
                if (!Player_DataManager.Instance.inventory_Player.Exists(x => x.ID.Equals(objectData.ID)))
                {
                    Player_DataManager.Instance.inventory_Player.Add(objectData);
                }

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

        Query allCitiesQuery = db.Collection("Player").Document(IDPlayer).Collection("SystemNotification");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                SystemNotification objectData = documentSnapshot.ConvertTo<SystemNotification>();
                if (!Player_DataManager.Instance.systemNotification.Contains(objectData))
                {
                    Player_DataManager.Instance.systemNotification.Add(objectData);
                }
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
        string IDPlayer = AuthController.ID;
        if (IDPlayer == null) IDPlayer = FacebookManager.ID;
        isDoneFriend = false;
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Query allCitiesQuery = db.Collection("Player").Document(IDPlayer).Collection("Friend_Player");
        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Friend_Player objectData = documentSnapshot.ConvertTo<Friend_Player>();
                objectData.friendID = documentSnapshot.Id;
                if (!Player_DataManager.Instance.friend_Player.Exists(x => x.friendID.Equals(objectData.friendID)))
                {
                    Player_DataManager.Instance.friend_Player.Add(objectData);
                }
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
        Query allCitiesQuery = db.Collection("Notifcation")
         // .WhereEqualTo("sentID_Notification", Player_DataManager.Instance.Player.ID)
         .WhereEqualTo("receivedID_Notification", Player_DataManager.Instance.Player.ID)
        // .OrderBy("dateCreate")
        ;

        allCitiesQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allCitiesQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Notification_Struct objectData = documentSnapshot.ConvertTo<Notification_Struct>();
                objectData.ID = documentSnapshot.Id;
                if (!Player_DataManager.Instance.notification_Player.Exists(x => x.ID.Equals(objectData.ID)))
                {
                    Player_DataManager.Instance.notification_Player.Add(objectData);
                }
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
                   Player_DataManager.Instance.Player.texture2D = texture;
               }
           });
        yield return null;
    }
    IEnumerator LoadingDataFromSever()
    {
        yield return new WaitUntil(() => Player_DataManager.Instance != null);
        loadingPlayer();
        yield return new WaitUntil(() => isDonePlayer);
        loadDataAchievement();
        loadDataFriend();
        loadDataInvetory();
        loadDataNotification();
        loadDataSystemNotification();

        yield return new WaitUntil(() => isDoneAchive && isDoneFriend && isDoneInvent && isDoneNotification && isDoneSystemNoti);
        Player_DataManager.Instance.updateStatPlayer();
        yield return null;
    }
    public bool loadDataAllDone()
    {
        return isDonePlayer;
    }
}
