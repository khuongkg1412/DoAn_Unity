using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SocialNotificationData : MonoBehaviour
{
    FirebaseFirestore db;

    private SocialNotificationStruct objectData;

    List<SocialNotificationStruct> listData = new List<SocialNotificationStruct>();

    bool isRun = false;

    int count = 0;

    public RawImage notificationImage;

    public RawImage notificationIcon;

    public Text notificationContent;
    private Text notificationSenderId;

    public bool notificationStatus;
    public GameObject prefab;

    public int numberToCreate;


    SocialNotificationStruct rank1 =
            new SocialNotificationStruct
            {
                notificationImage = "Notification/SocialNotification/noti1/2.png",
                notificationIcon = "Notification/SocialNotification/noti1/mail-icon.png",
                notificationContent = "Gurdeep Crane has sent you a message.",
                notificationSenderID = "a123",
                notificationStatus = false
            };

    private void Start()
    {
        Debug.Log("Social Notification is running!");
        StartCoroutine(setDatatoGO());
    }

    void Populate(
        string notiContent,
        string notiSenderId,
        bool notiStatus
    )
    {
        GameObject newObj;
        Debug.Log("Social Notification's Populate is running!");
        notificationContent.text = notiContent.ToString();
        notificationSenderId.text = notiSenderId.ToString();
        notificationStatus = notiStatus;
        newObj = (GameObject)Instantiate(prefab, transform);
        Debug.Log("Social Notification Run");
    }

    IEnumerator GetData()
    {
        //db connection
        Debug.Log("Social Notification's GetData is running!");
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading " + Time.time);

        Query leaderQuery = db.Collection("socialNotification");
        leaderQuery
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                QuerySnapshot leaderQuerySnapshot = task.Result;
                foreach (DocumentSnapshot
                    documentSnapshot
                    in
                    leaderQuerySnapshot.Documents
                )
                {
                    objectData =
                        documentSnapshot.ConvertTo<SocialNotificationStruct>();
                    listData.Add(objectData);
                }
                Debug.Log("Reading Database Completed!");
                isRun = true;
            });
        yield return null;
    }

    IEnumerator GetImage(string dataImage, int num)
    {
        Debug.Log("Social Notification's GetImage is running!");
        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataImage);
        Debug.Log("Getting Image");
        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 1 * 1024 * 1024;
        storageRef
            .GetBytesAsync(maxAllowedSize)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log("Task Fault/Canceled");
                    // Uh-oh, an error occurred!
                    Debug.LogException(task.Exception);
                }
                else
                {
                    Debug.Log("Task accept");
                    Debug.Log("Image Downloading ");
                    byte[] fileContents = task.Result;
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(fileContents);
                    if (num == 1)
                    {
                        notificationImage.texture = texture;
                        count++;
                    }
                    else if (num == 2)
                    {
                        notificationIcon.texture = texture;
                        count++;
                    }
                    Debug.Log("Image Downloaded!");
                }

            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        Debug.Log("Social Notification's setDatatoGo is running!");
        StartCoroutine(GetData());
        yield return new WaitUntil(() => isRun == true);

        //Wait for data has been load from firebase
        StartCoroutine(GetImage(listData[0].notificationImage, 1));
        StartCoroutine(GetImage(listData[0].notificationIcon, 2));
        yield return new WaitUntil(() => count == 3);
        Debug.Log("Set Data Done!");

        //UIImage.texture = texture;
        Populate(
        listData[0].notificationContent,
        listData[0].notificationSenderID,
        listData[0].notificationStatus);
        yield return null;
    }
}
