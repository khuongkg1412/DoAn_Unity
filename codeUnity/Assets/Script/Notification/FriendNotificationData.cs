using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FriendNotificationData : MonoBehaviour
{
    FirebaseFirestore db;

    private FriendNotificationStruct objectData;

    List<FriendNotificationStruct> listData = new List<FriendNotificationStruct>();

    bool isRun = false;

    int count = 0;
    public GameObject prefab;
    public ScrollRect notiList;
    public GameObject content;

    public int numberToCreate;

    // FriendNotificationStruct
    // noti1 = new FriendNotificationStruct
    // {
    //     notificationImage = "Notification/FriendNotification/noti1/2.png",
    //     notificationIcon = "Notification/FriendNotification/noti1/mail-icon.png",
    //     notificationContent = "Gurdeep Crane has sent you a message.",
    //     notificationSenderID = "a123",
    //     notificationStatus = false
    // };

    private void Start()
    {
        StartCoroutine(setDatatoGO());
    }

    void Populate(Sprite notificationIcon, Sprite notificationImage, string notiContent)
    {
        // GameObject newObj;
        // newObj = (GameObject)Instantiate(prefab, transform);
        // Debug.Log("Run");
        // newObj.transform.Find("avatar_img").gameObject.GetComponent<Image>().sprite = sprite;


        GameObject scrollItemObj = (GameObject)Instantiate(prefab, transform);

        scrollItemObj.transform.Find("notify_icon").gameObject.GetComponent<Image>().sprite = notificationIcon;
        scrollItemObj.transform.Find("notify_txt").gameObject.GetComponent<Text>().text = notiContent;
        scrollItemObj.transform.Find("avatar_img").gameObject.GetComponent<Image>().sprite = notificationImage;
    }

    IEnumerator GetData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading at GetData friendNotification");

        Query leaderQuery = db.Collection("FriendNotification");
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
                        documentSnapshot.ConvertTo<FriendNotificationStruct>();
                    listData.Add(objectData);
                }
                isRun = true;
            });
        yield return null;
    }
    Sprite avatar;
    Sprite icon;
    IEnumerator GetImage(string avatarImage, string iconImage, string content)
    {
        Debug.Log("Currently at GetImage");
        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(avatarImage);
        StorageReference storageRef1 = storage.GetReference(iconImage);


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
                    count++;
                }
                else
                {
                    Debug.Log("Image Downloaded " + Time.time);
                    byte[] fileContents = task.Result;
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(fileContents);
                    avatar = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    count++;
                }

            });
        storageRef1
            .GetBytesAsync(maxAllowedSize)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    // Uh-oh, an error occurred!
                    Debug.LogException(task.Exception);
                    count++;
                }
                else
                {
                    Debug.Log("Image Downloaded " + Time.time);
                    byte[] fileContents = task.Result;
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(fileContents);
                    icon = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    count++;
                }
            });
        Populate(icon, avatar, content);
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        Debug.Log("Currently at setDatatoGo");
        StartCoroutine(GetData());
        yield return new WaitUntil(() => isRun == true);
         foreach (FriendNotificationStruct noti in listData)
        {
            StartCoroutine(GetImage(noti.notificationImage, noti.notificationIcon, 
            noti.notificationContent));
        }
        // //Wait for data has been load from firebase
        // StartCoroutine(GetImage(listData[0].notificationImage,
        //     listData[0].notificationIcon, listData[0].notificationContent));

        // yield return new WaitUntil(() => count == 3);
        // Debug.Log("Done" + Time.time);

        // //UIImage.texture = texture;
        // // Populate(sprite,listData[0].notificationContent);
        yield return null;
    }
}
