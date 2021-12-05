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
    RawImage notificationIcon;
    Text notificationContent;
    bool notificationStatus;

    public GameObject prefab;

    public int numberToCreate;


    SystemNotificationStruct

    noti1 = new SystemNotificationStruct
    {
        notificationIcon = "",
        notificationContent = "",
        notificationStatus = false
    };

    private void Start()
    {
        StartCoroutine(setDatatoGO());
    }

    void Populate(string notiContent)
    {
        GameObject newObj;
        notificationContent.text = notiContent;
        newObj = (GameObject)Instantiate(prefab, transform);
        Debug.Log("Run");
    }

    IEnumerator GetData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading " + Time.time);

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
                isRun = true;
            });
        yield return null;
    }
    IEnumerator GetImage(string dataImage, int num)
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
                    Debug.Log("Image Downloaded " + Time.time);
                    byte[] fileContents = task.Result;
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(fileContents);
                    if (num == 1)
                    {
                        notificationIcon.texture = texture;
                        count++;
                    }
                }
            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        StartCoroutine(GetData());
        yield return new WaitUntil(() => isRun == true);

        //Wait for data has been load from firebase
        StartCoroutine(GetImage(listData[0].notificationIcon, 2));

        yield return new WaitUntil(() => count == 3);
        Debug.Log("Done" + Time.time);

        //UIImage.texture = texture;
        Populate(
        listData[0].notificationContent);
        yield return null;
    }
}
