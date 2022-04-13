using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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
                if (AuthController.LifeisRun == -1) AuthController.LifeisRun = Player_DataManager.Instance.Player.level.life;
                if (!AuthController.TimeisRun) TestAsync();

            }
            else
            {
                Debug.LogError("loadingPlayer Error");
                SceneManager.LoadScene("Create Character");
            }
            isDonePlayer = true;
        });

    }
    private void loadDataAchievement()
    {
        isDoneInvent = false;
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
                Player_DataManager.Instance.achivementReceived_Player.Add(objectData);
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

        Query allCitiesQuery = db.Collection("Player").Document(IDPlayer).Collection("SystemNotification");
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

    float timeRemaining;

    async void TestAsync()
    {
        timeRemaining = 60;
        AuthController.TimeisRun = true;
        while (AuthController.TimeisRun)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            if (AuthController.LifeisRun < 6)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= 1;
                    //  if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainPage")) Player_DataManager.Instance.DisplayTime(timeRemaining);
                    Debug.Log(timeRemaining);
                }
                else
                {
                    //Time's up
                    timeRemaining = 60;
                    Player_DataManager.Instance.Player.level.life += 1;
                    // if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainPage")) Player_DataManager.Instance.Life.text = Player_DataManager.Instance.Player.level.life + "/6";
                    Player_Update.UpdatePlayer();
                }
            }
            else AuthController.TimeisRun = false;
        }
    }
}
