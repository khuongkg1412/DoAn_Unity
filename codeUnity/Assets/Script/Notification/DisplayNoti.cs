using System.Collections;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNoti : MonoBehaviour
{
    public GameObject lifeRequest_prefab, addFriendRequest_prefab, systemRequest_prefab;

    public GameObject FriendNoti, SystemNoti, SocialNoti;

    public ScrollRect FriendNotiList, SystemNotiList, SocialNotiList;

    //public Text numberUnseenFriendNoti, numberUnseenSocialNoti, numberUnseenSystemNoti;

    private void Start()
    {
        StartCoroutine(LoadNoti());
    }

    private IEnumerator LoadNoti()
    {
        int UnseenFriendNoti = 0, UnseenSystemNoti = 0, UnseenSocialNoti = 0;

        foreach (Notification_Struct noti in Player_DataManager.Instance.notification_Player)
        {
            if (noti.type_Notification == 0)
            {
                if (noti.isRead_Notification == false) UnseenSystemNoti += 1;

                StartCoroutine(GetPlayerAvatar(noti.sentID_Notification));
                yield return new WaitForSeconds(3);
                StartCoroutine(Populate(noti, 0));
            }
            else if (noti.type_Notification == 1)
            {

                if (noti.isRead_Notification == false) UnseenFriendNoti += 1;

                StartCoroutine(GetPlayerAvatar(noti.sentID_Notification));
                yield return new WaitForSeconds(3);
                StartCoroutine(Populate(noti, 1));
            }
            else
            {

                if (noti.isRead_Notification == false) UnseenSocialNoti += 1;

                StartCoroutine(GetPlayerAvatar(noti.sentID_Notification));
                yield return new WaitForSeconds(3);
                StartCoroutine(Populate(noti, 2));
            }
        }
        //setUnseenNotiNumber(UnseenFriendNoti, UnseenSocialNoti, UnseenSystemNoti);
        yield return null;
    }

    private void setUnseenNotiNumber(int NotiFriend, int NotiSocial, int NotiSystem)
    {
        if (NotiFriend > 0)
        {
            GameObject obj = GameObject.Find("notify_count1");
            obj.SetActive(true);
            obj.GetComponentInChildren<Text>().text = NotiFriend.ToString();
        }
        //else GameObject.Find("notify_count").SetActive(false);

        if (NotiSocial > 0)
        {
            GameObject obj = GameObject.Find("notify_count2");
            obj.SetActive(true);
            obj.transform.Find("Text").gameObject.GetComponent<Text>().text = NotiSocial.ToString();
        }
        //else GameObject.Find("notify_count").SetActive(false);

        if (NotiSystem > 0)
        {
            GameObject obj = GameObject.Find("notify_count");
            obj.SetActive(true);
            obj.transform.Find("Text").gameObject.GetComponent<Text>().text = NotiSystem.ToString();
        }
        //else GameObject.Find("notify_count").SetActive(false);
    }

    IEnumerator Populate(Notification_Struct noti, int typeNoti)
    {
        switch (typeNoti)
        {
            case 1:
                Debug.Log(avatarSender);
                //lifeRequest_prefab.GetComponent<Button>().onClick.AddListener(() => OnFriendNotiClick());
                GameObject Noti1 = (GameObject)Instantiate(lifeRequest_prefab, FriendNoti.transform);
                Noti1.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnFriendNotiClick());
                Noti1.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                Noti1.transform.Find("Mask Circle/SenderAvatar").gameObject.GetComponent<Image>().sprite = avatarSender;

                break;
            case 2:
                Debug.Log(avatarSender);
                GameObject Noti2 = (GameObject)Instantiate(addFriendRequest_prefab, SocialNoti.transform);
                Noti2.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnSocialNotiClick());
                Noti2.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                Noti2.transform.Find("SenderAvatar").gameObject.GetComponent<Image>().sprite = avatarSender;

                break;
            default:

                GameObject Noti = (GameObject)Instantiate(systemRequest_prefab, SystemNoti.transform);
                Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnSystemNotiClick());
                Noti.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                //Noti.transform.Find("SenderAvatar").gameObject.GetComponent<Image>().sprite = avatarSender;
                break;
        }
        yield return null;
    }

    void OnFriendNotiClick() { Debug.Log("Thanh cong"); }
    void OnSystemNotiClick() { }
    void OnSocialNotiClick() { Debug.Log("Thanh cong"); }


    FirebaseFirestore db;
    public static Sprite avatarSender;
    bool IsDoneDownload = false;
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

    private IEnumerator GetImage(string dataImage)
    {
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
                Debug.Log("Image Downloaded");
            }
        });
        yield return null;
    }
}
