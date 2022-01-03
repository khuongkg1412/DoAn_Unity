using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScence : MonoBehaviour
{
    //Wait for reoading excutes
    private float waitToLoad;

    public void reloadScence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void storeOpening()
    {
        SceneManager.LoadScene("Store");
    }

    public void achiveOpening()
    {
        SceneManager.LoadScene("ACHIEVEMENT");
    }

    public void howToPlayOpening() //khuong
    {
        SceneManager.LoadScene("how to play");
    }

    public void leaderGlobalOpening() //khuong
    {
        SceneManager.LoadScene("Leaderboard Global");
    }

    public void leaderLocalOpening() //khuong
    {
        SceneManager.LoadScene("Leaderboard Local");
    }

    public void notificationOpening() //khuong
    {
        SceneManager.LoadScene("Notification");
    }

    public void backtoMainPage()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainPage");
    }

    public void openProfile()
    {
        SceneManager.LoadScene("Main screen");
    }

    public void gameplayOpening()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("Tutorial");
    }

    public Text Coin, Diamond, Life, Name;
    public Image avatar;

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
        StartCoroutine(loadData());
    }

    IEnumerator loadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        string IDPlayer = AuthController.ID;
        if (IDPlayer == null) IDPlayer = FacebookManager.ID;

        DocumentReference docRef = db.Collection("Player").Document(IDPlayer);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {

            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log("Dang check");
                playerStruct player = snapshot.ConvertTo<playerStruct>();
                Coin.text = "" + player.coin_Player;
                Diamond.text = "" + player.diamond_Player;
                Life.text = "" + player.energy_Player;
                Name.text = "" + player.name_Player;
                StartCoroutine(addOtherCollection(IDPlayer));
                StartCoroutine(GetImage(player.avatar_Player));
            }
        });
        yield return true;
    }

    IEnumerator GetImage(string dataImage)
    {
        Debug.Log("Image Downloading");

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataImage);

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 1 * 1024 * 1024;
        storageRef
            .GetBytesAsync(maxAllowedSize)
            .ContinueWithOnMainThread(task =>
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
                    Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    avatar.sprite = sprite;
                }
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

