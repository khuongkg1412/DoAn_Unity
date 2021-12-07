using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemNotificationData : MonoBehaviour
{

    FirebaseFirestore db;

    private SystemNotificationStruct objectData;

    List<SystemNotificationStruct> listData = new List<SystemNotificationStruct>();

    bool isRun = false;

    int count = 0;
    public RawImage notificationIcon;
    public Text notificationContent;
    public bool notificationStatus;

    public GameObject prefab;


    SystemNotificationStruct noti1 = new SystemNotificationStruct
    {
        notificationIcon = "Notification/SystemNotification/noti1/achievement_icon.png",
        notificationContent = "You got a new achievement",
        notificationStatus = false
    };

    private void Start()
    {
        Debug.Log("SystemNotification is running");
        StartCoroutine(setDatatoGO());
    }

    void Populate(string notiContent)
    {
        Debug.Log("SystemNotification Populate is running");
        GameObject newObj;
        notificationContent.text = notiContent;
        newObj = (GameObject)Instantiate(prefab, transform);
        Debug.Log("SystemNotification Run");
    }

    IEnumerator GetData()
    {
        //db connection

        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("SystemNotification Database Reading");

        Query leaderQuery = db.Collection("systemNotification");
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
                        documentSnapshot.ConvertTo<SystemNotificationStruct>();
                    listData.Add(objectData);
                }
                Debug.Log("Add data complete!");
                isRun = true;
            });
        yield return null;
    }
    IEnumerator GetImage(string dataImage)
    {
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
                    // Uh-oh, an error occurred!
                    Debug.LogException(task.Exception);
                }
                else
                {
                    Debug.Log("SystemNotification Image Downloaded ");
                    byte[] fileContents = task.Result;
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(fileContents);
                    notificationIcon.texture = texture;
                    count++;
                    Debug.Log("SystemNotification Image is loaded");
                }
            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        StartCoroutine(GetData());
        yield return new WaitUntil(() => isRun == true);

        //Wait for data has been load from firebase
        StartCoroutine(GetImage(listData[0].notificationIcon));

        yield return new WaitUntil(() => count == 3);
        Debug.Log("Done" + Time.time);

        //UIImage.texture = texture;
        Populate(
        listData[0].notificationContent);
        yield return null;
    }
}
