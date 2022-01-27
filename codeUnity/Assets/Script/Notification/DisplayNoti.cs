using System.Collections;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNoti : MonoBehaviour
{
    public GameObject lifeRequest_prefab, addFriendRequest_prefab, systemUpdate_prefab, reachAchieve_prefab, systemGift_prefab;

    public GameObject FriendNoti, SystemNoti, SocialNoti;

    public GameObject FriendNotiPanel, SystemNotiPanel, SocialNotiPanel;

    private void Start()
    {
        StartCoroutine(LoadNoti());
    }

    private IEnumerator LoadNoti()
    {
        int UnseenFriendNoti = 0, UnseenSystemNoti = 0, UnseenSocialNoti = 0;

        foreach (Notification_Struct noti in Player_DataManager.Instance.notification_Player)
        {
            if (noti.type_Notification < 3)
            {
                if (!noti.isRead_Notification) UnseenSystemNoti += 1;

                StartCoroutine(Populate(noti, noti.type_Notification));
            }
            else if (noti.type_Notification == 3)
            {

                if (!noti.isRead_Notification) UnseenFriendNoti += 1;

                StartCoroutine(GetPlayerAvatar(noti.sentID_Notification));
                yield return new WaitForSeconds(3);
                StartCoroutine(Populate(noti, noti.type_Notification));
            }
            else
            {

                if (!noti.isRead_Notification) UnseenSocialNoti += 1;

                StartCoroutine(GetPlayerAvatar(noti.sentID_Notification));
                yield return new WaitForSeconds(3);
                StartCoroutine(Populate(noti, noti.type_Notification));
            }
        }
        setUnseenNotiNumber(UnseenFriendNoti, UnseenSocialNoti, UnseenSystemNoti);
        yield return null;
    }

    private void setUnseenNotiNumber(int NotiFriend, int NotiSocial, int NotiSystem)
    {
        GameObject[] objs;
        if (NotiFriend > 0)
        {
            objs = GameObject.FindGameObjectsWithTag("notify_count1");
            foreach (GameObject obj in objs) obj.GetComponentInChildren<Text>().text = NotiFriend.ToString();
        }
        else
        {
            objs = GameObject.FindGameObjectsWithTag("notify_count1");
            foreach (GameObject obj in objs) obj.SetActive(false);
        }

        if (NotiSocial > 0)
        {
            objs = GameObject.FindGameObjectsWithTag("notify_count2");
            foreach (GameObject obj in objs) obj.GetComponentInChildren<Text>().text = NotiSocial.ToString();
        }
        else
        {
            objs = GameObject.FindGameObjectsWithTag("notify_count2");
            foreach (GameObject obj in objs) obj.SetActive(false);
        }

        if (NotiSystem > 0)
        {
            objs = GameObject.FindGameObjectsWithTag("notify_count");
            foreach (GameObject obj in objs) obj.GetComponentInChildren<Text>().text = NotiSystem.ToString();
        }
        else
        {
            objs = GameObject.FindGameObjectsWithTag("notify_count");
            foreach (GameObject obj in objs) obj.SetActive(false);
        }
    }

    IEnumerator Populate(Notification_Struct noti, int typeNoti)
    {
        GameObject Noti;
        switch (typeNoti)
        {
            case 1:
                Noti = (GameObject)Instantiate(reachAchieve_prefab, SystemNoti.transform);
                Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnReachAchiveNotiClick());
                Noti.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                if (noti.isRead_Notification)
                {
                    Color color = Hetx2RGB("D9D9E3");
                    Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Image>().color = color;
                }

                break;
            case 2:
                Noti = (GameObject)Instantiate(systemGift_prefab, SystemNoti.transform);
                Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnSystemGiftNotiClick());
                Noti.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                if (noti.isRead_Notification)
                {
                    Color color = Hetx2RGB("D9D9E3");
                    Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Image>().color = color;
                }

                break;
            case 3:
                Noti = (GameObject)Instantiate(lifeRequest_prefab, FriendNoti.transform);
                Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnFriendNotiClick(Noti, noti));
                Noti.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                Noti.transform.Find("Mask Circle/SenderAvatar").gameObject.GetComponent<Image>().sprite = avatarSender;
                if (noti.isRead_Notification)
                {
                    Color color = Hetx2RGB("D9D9E3");
                    Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Image>().color = color;
                }

                break;
            case 4:

                Noti = (GameObject)Instantiate(addFriendRequest_prefab, SocialNoti.transform);

                Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnSocialNotiClick());
                Noti.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                Noti.transform.Find("Mask Circle/SenderAvatar").gameObject.GetComponent<Image>().sprite = avatarSender;
                if (noti.isRead_Notification)
                {
                    Color color = Hetx2RGB("D9D9E3");
                    Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Image>().color = color;
                }

                break;
            default:
                Noti = (GameObject)Instantiate(systemUpdate_prefab, SystemNoti.transform);
                Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => OnSystemUpdateNotiClick());
                Noti.transform.Find("Title_noti").gameObject.GetComponent<Text>().text = noti.title_Notification;
                if (noti.isRead_Notification)
                {
                    Color color = Hetx2RGB("D9D9E3");
                    Noti.transform.Find("Title_noti").gameObject.GetComponentInParent<Image>().color = color;
                }
                break;
        }
        yield return null;
    }

    private Color Hetx2RGB(string hex)
    {

        char[] values = hex.ToCharArray();
        Color newColor = Color.white;

        //Make sure we dont have any alpha values
        if (hex.Length != 6)
        {
            return newColor;
        }

        var hexRed = int.Parse(hex[0].ToString() + hex[1].ToString(),
        System.Globalization.NumberStyles.HexNumber);

        var hexGreen = int.Parse(hex[2].ToString() + hex[3].ToString(),
        System.Globalization.NumberStyles.HexNumber);

        var hexBlue = int.Parse(hex[4].ToString() + hex[5].ToString(),
        System.Globalization.NumberStyles.HexNumber);

        newColor = new Color(hexRed / 255f, hexGreen / 255f, hexBlue / 255f);

        return newColor;

    }

    public void OnSystemUpdateNotiClick() { }
    public void OnSystemGiftNotiClick() { }
    public void OnReachAchiveNotiClick() { }
    public void OnFriendNotiClick(GameObject CurentNoti_prefab, Notification_Struct noti)
    {
        GameObject obj = (GameObject)Resources.Load("Prefabs/Notification/Popup window/LifeRequestWindow", typeof(GameObject));

        GameObject popupWindow = (GameObject)Instantiate(obj, FriendNotiPanel.transform);

        //IsreadNoti(CurentNoti_prefab, noti);
    }
    public void OnSocialNotiClick() { Debug.Log("Thanh cong"); }

    private void IsreadNoti(GameObject CurentNoti_prefab, Notification_Struct noti)
    {
        Color color = Hetx2RGB("D9D9E3");
        CurentNoti_prefab.transform.Find("Title_noti").gameObject.GetComponentInParent<Image>().color = color;
        Player_DataManager.Instance.read_Notification(noti);
    }

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
