using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine.SceneManagement;

public class friendHandler : MonoBehaviour
{
    FirebaseFirestore db;
    public GameObject prefab, addFriend_box;
    public ScrollRect friendList;
    public GameObject content;


    // Start is called before the first frame update
    void Start()
    {
        //friendList.verticalNormalizedPosition = 1;
        StartCoroutine(GetFriendData());
    }

    void OnLifeRequestClick(string FriendId)
    {
        Debug.Log(FriendId);
        Player_DataManager.Instance.SendLiferequest(FriendId);
    }

    void Populate(Sprite sprite, PlayerStruct friend)
    {
        GameObject scrollItemObj = (GameObject)Instantiate(prefab, content.transform);


        scrollItemObj.transform.Find("Name & level/Name").gameObject.GetComponent<Text>().text = friend.generalInformation.username_Player;
        scrollItemObj.transform.Find("Name & level/Level").gameObject.GetComponent<Text>().text = "Level " + (int)friend.level.level;
        scrollItemObj.transform.Find("Mask/Avatar").gameObject.GetComponent<Image>().sprite = sprite;
        scrollItemObj.transform.Find("FriendID").gameObject.GetComponent<Text>().text = friend.ID;
        scrollItemObj.transform.Find("Name & level/LifeRequest").gameObject.GetComponent<Button>().onClick.AddListener(() => OnLifeRequestClick(GameObject.Find("FriendID").gameObject.GetComponent<Text>().text));
    }

    private PlayerStruct player;
    IEnumerator GetFriendData()
    {
        foreach (Friend_Player friend in Player_DataManager.Instance.friend_Player)
        {
            //db connection
            db = FirebaseFirestore.DefaultInstance;
            Debug.Log("Database Reading " + friend.friendID);

            DocumentReference docRef = db.Collection("Player").Document(friend.friendID);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    player = snapshot.ConvertTo<PlayerStruct>();
                    player.ID = snapshot.Id;
                    StartCoroutine(GetImage(player));
                }
                else
                {
                    Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
                }
            });
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator GetImage(PlayerStruct friend)
    {
        Debug.Log("Image Downloading");

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(friend.generalInformation.avatar_Player);

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 4 * 1024 * 1024;
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

                    Populate(sprite, friend);
                }
            });
        yield return null;
    }

    public void findNewFriend(Text name)
    {
        PlayerStruct player = ListPlayer_DataManager.Instance.listPlayer.Find(e => e.generalInformation.username_Player.Equals(name.text));
        StartCoroutine(GetFriendImage(player));
    }

    IEnumerator GetFriendImage(PlayerStruct friend)
    {
        Debug.Log("Image Downloading");

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(friend.generalInformation.avatar_Player);

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 4 * 1024 * 1024;
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

                    DisplayInfor(sprite, friend);
                }
            });
        yield return null;
    }

    private void DisplayInfor(Sprite sprite, PlayerStruct friend)
    {
        GameObject scrollItemObj = (GameObject)Instantiate(addFriend_box, transform);

        scrollItemObj.transform.Find("Name player").gameObject.GetComponent<Text>().text = friend.generalInformation.username_Player;
        scrollItemObj.transform.Find("infor box/level").gameObject.GetComponent<Text>().text = "Level " + (int)friend.level.level;
        scrollItemObj.transform.Find("infor box/Image").gameObject.GetComponent<Image>().sprite = sprite;
        scrollItemObj.transform.Find("infor box/ok_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => addFriend(friend));
        scrollItemObj.transform.Find("close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(scrollItemObj));
    }

    private void addFriend(PlayerStruct friend)
    {
        Player_DataManager.Instance.addFriend(friend);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
