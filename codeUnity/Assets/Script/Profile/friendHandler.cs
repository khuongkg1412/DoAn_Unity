using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;

public class friendHandler : MonoBehaviour
{
    FirebaseFirestore db;
    public GameObject prefab;
    public ScrollRect friendList;
    public GameObject content;


    // Start is called before the first frame update
    void Start()
    {
        //friendList.verticalNormalizedPosition = 1;
        StartCoroutine(GetFriendData());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Populate(Sprite sprite, string name, float Level)
    {
        GameObject scrollItemObj = (GameObject)Instantiate(prefab, transform);

        scrollItemObj.transform.Find("Name & level/Name").gameObject.GetComponent<Text>().text = name;
        scrollItemObj.transform.Find("Name & level/level").gameObject.GetComponent<Text>().text = "Level " + Level;
        scrollItemObj.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = sprite;
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
                    StartCoroutine(GetImage(player.generalInformation.avatar_Player, player.generalInformation.username_Player, player.level.level));
                }
                else
                {
                    Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
                }
            });
        }
        yield return null;
    }

    IEnumerator GetImage(string dataImage, string Name, float level)
    {
        Debug.Log("Image Downloading");

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataImage);

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

                    Populate(sprite, Name, level);
                }
            });
        yield return null;
    }
}
