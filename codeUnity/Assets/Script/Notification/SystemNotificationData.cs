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

    private Notification_Struct objectData;

    List<Notification_Struct> listData = new List<Notification_Struct>();

    bool isRun = false;

    int count = 0;
    public RawImage notificationIcon;

    public Text notificationContent;

    public bool notificationStatus;
    public GameObject prefab;


    Notification_Struct noti1 =
            new Notification_Struct
            {

            };

    private void Start()
    {
        // Debug.Log("System Add data is running");
        //AddData();
        // Debug.Log("System Notification is running!");
        StartCoroutine(setDatatoGO());
    }

    void Populate(
        string notiContent,
        bool notiStatus
    )
    {
        GameObject newObj;

        notificationContent.text = notiContent;
        notificationStatus = notiStatus;
        newObj = (GameObject)Instantiate(prefab, transform);

    }

    public void AddData()
    {

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        db.Collection("SystemNotification").AddAsync(noti1);
    }
    IEnumerator GetData()
    {
        //db connection

        db = FirebaseFirestore.DefaultInstance;


        Query leaderQuery = db.Collection("SystemNotification");
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
                        documentSnapshot.ConvertTo<Notification_Struct>();

                    listData.Add(objectData);

                }
                isRun = true;
            });

        yield return null;
    }

    IEnumerator GetImage(string dataImage, string notiContent, bool notiStatus)
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

                    byte[] fileContents = task.Result;
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(fileContents);
                    notificationIcon.texture = texture;
                    count++;
                    Populate(notiContent, notiStatus);
                }

            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {

        StartCoroutine(GetData());
        yield return new WaitUntil(() => isRun == true);

        foreach (var objectItem in listData)
        {


            count = 0;
        }
        yield return null;
    }

}
