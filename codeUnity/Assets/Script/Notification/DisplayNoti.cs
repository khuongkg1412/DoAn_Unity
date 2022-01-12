using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNoti : MonoBehaviour
{
    public GameObject lifeRequest_prefab, addFriendRequest_prefab, systemRequest_prefab;

    public ScrollRect FriendNoti, SystemNoti, SocialNoti;
    public Text numberUnseenFriendNoti, numberUnseenSocialNoti, numberUnseenSystemNoti;
    int UnseenFriendNoti = 0, UnseenSystemNoti = 0, UnseenSocialNoti = 0;

    private void LoadNoti()
    {
        foreach (Notification_Struct noti in Player_DataManager.Instance.notification_Player)
        {
            if (noti.type_Notification == 0)
            {
                if (noti.isRead_Notification == false) UnseenSystemNoti += 1;

                break;
            }
            else if (noti.type_Notification == 1)
            {
                if (noti.isRead_Notification == false) UnseenFriendNoti += 1;

                break;
            }
            else
            {
                if (noti.isRead_Notification == false) UnseenSocialNoti += 1;

            }
        }
        setUnseenNotiNumber(UnseenFriendNoti, UnseenSocialNoti, UnseenSystemNoti);
    }

    private void setUnseenNotiNumber(int NotiFriend, int NotiSocial, int NotiSystem)
    {
        if (NotiFriend > 0)
        {
            GameObject.Find("notify_count").SetActive(true);
            numberUnseenFriendNoti.text = UnseenFriendNoti.ToString();
        }
        //else GameObject.Find("notify_count").SetActive(false);

        if (NotiSocial > 0)
        {
            GameObject.Find("notify_count").SetActive(true);
            numberUnseenSocialNoti.text = UnseenSocialNoti.ToString();
        }
        //else GameObject.Find("notify_count").SetActive(false);

        if (NotiSystem > 0)
        {
            GameObject.Find("notify_count").SetActive(true);
            numberUnseenSystemNoti.text = UnseenSystemNoti.ToString();
        }
        //else GameObject.Find("notify_count").SetActive(false);
    }

    IEnumerator Populate(string IDSender, GameObject verticalObject, Notification_Struct noti, int typeNoti)
    {
        StartCoroutine(GetPlayerAvatar(IDSender));
        yield return new WaitUntil(() => IsDoneDownload = true);
        switch (typeNoti)
        {
            case 1:

                GameObject Noti1 = (GameObject)Instantiate(lifeRequest_prefab, FriendNoti.transform);

                Noti1.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                Noti1.transform.Find("SenderAvatar").gameObject.GetComponent<Image>().sprite = avatarSender;

                break;
            case 2:

                GameObject Noti2 = (GameObject)Instantiate(addFriendRequest_prefab, SocialNoti.transform);
                Noti2.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;

                break;
            default:

                GameObject Noti = (GameObject)Instantiate(systemRequest_prefab, SystemNoti.transform);
                Noti.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;

                break;
        }
    }

    FirebaseFirestore db;
    Sprite avatarSender;
    bool IsDoneDownload;
    IEnumerator GetPlayerAvatar(string ID)
    {
        db = FirebaseFirestore.DefaultInstance;

        DocumentReference docRef = db.Collection("Player").Document(ID);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                PlayerStruct player = snapshot.ConvertTo<PlayerStruct>();
                IsDoneDownload = false;
                StartCoroutine(GetImage(player.generalInformation.avatar_Player));
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
        yield return null;
    }

    IEnumerator GetImage(string dataImage)
    {
        Debug.Log("Image Downloading");

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataImage);

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 4 * 1024 * 1024;
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
                Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                avatarSender = sprite;
                IsDoneDownload = true;
            }
        });
        yield return null;
    }
}
