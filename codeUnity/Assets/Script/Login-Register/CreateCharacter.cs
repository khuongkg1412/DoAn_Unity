using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    FirebaseFirestore db;
    public InputField characterName;

    int male = 1;

    Inventory_Player inventory_Player = new Inventory_Player()
    {
        quantity = 0
    };
    SystemNotification systemNotification = new SystemNotification()
    {
        status_Notification = false
    };
    Friend_Player friend_Player = new Friend_Player()
    {
        accept_Friend = false,
        notificationID = false
    };
    Achievement_Player achievement_Player = new Achievement_Player()
    {
        achived_Player = true,
        progress_Player = 0
    };
    Notification_Player notification_Player = new Notification_Player()
    {
        content_Notification = "This is the content of the first notification",
        sentID_Notification = "ID sent",
        status_Notification = false,
        title_Notification = "This is title of the first notification",
        type_Notification = 0
    };

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }
    public void Create()
    {
        if (characterName.text.Length == 0)
        {
            Debug.Log("chua co ten");
        }
        else StartCoroutine(createPlayer());
    }

    public void isMale()
    {
        male = 1;
    }

    public void isFemale()
    {
        male = 2;
    }

    IEnumerator createPlayer()
    {
        string IDPlayer = AuthController.ID;
        if (IDPlayer == null) IDPlayer = FacebookManager.ID;
        Debug.Log(IDPlayer);
        DocumentReference docRef = db.Collection("Player").Document(IDPlayer);
        PlayerStruct newPlayer = new PlayerStruct()
        {
            generalInformation = new GeneralInformation_Player()
            {
                username_Player = "Khuong Meo",
                avatar_Player = "PlayerAvatar/Avatar item.png",
                gender_Player = 0
            },
            concurrency = new Concurrency()
            {
                Coin = 0,
                Diamond = 0
            },
            numeral = new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 10,
                SPD_Numeral = 300
            },
            level = new Level()
            {
                currentXP = 0,
                reachXP = 130,
                level = 0,
                stage = 0,
                life = 6
            },
            statistic = new Dictionary<string, float>
        {
            {"VirusA_Killed",0},
            {"VirusB_Killed",0},
            {"VirusC_Killed",0},
            {"VirusD_Killed",0},
            {"Citizen_Saved",0}
        }
        };

        docRef.SetAsync(newPlayer).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the LA document in the cities collection.");
            StartCoroutine(addOtherCollection(IDPlayer));
            SceneManager.LoadScene("MainPage");
        });

        yield return null;
    }
    IEnumerator addOtherCollection(string ID)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("Demo");
        doc.SetAsync(inventory_Player);

        doc = db.Collection("Player").Document(ID).Collection("SystemNotification").Document("Demo");
        doc.SetAsync(systemNotification);

        doc = db.Collection("Player").Document(ID).Collection("Friend_Player").Document("Demo");
        doc.SetAsync(friend_Player);

        doc = db.Collection("Player").Document(ID).Collection("Achievement_Player").Document("Demo");
        doc.SetAsync(achievement_Player);

        doc = db.Collection("Player").Document(ID).Collection("Notification_Player").Document("Demo");
        doc.SetAsync(notification_Player);

        yield return null;
    }
}
