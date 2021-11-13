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
    List<playerStruct> listData = new List<playerStruct>();
    bool isRun = false;
    // public Image avatarPlayer;
    // public Text playerName;
    // public Text level;

    private playerStruct objectData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateItem());
        //friendList.verticalNormalizedPosition = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Populate(Sprite sprite, string name, int Level)
    {
        Debug.Log("Dang o day");
        
        // GameObject scrollPlayer;
        // avatarPlayer.sprite = sprite;
        // playerName.text = name;
        // level.text = "Level "+Level;
        // scrollPlayer = (GameObject) Instantiate(prefab, transform);


        GameObject scrollItemObj = (GameObject) Instantiate(prefab, transform);
        //scrollItemObj.transform.SetParent(content.transform, false);
        scrollItemObj.transform.Find("Name & level/Name").gameObject.GetComponent<Text>().text = name;
        scrollItemObj.transform.Find("Name & level/level").gameObject.GetComponent<Text>().text = "Level " + Level;
        scrollItemObj.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = sprite;
    }

    IEnumerator generateItem()
    {
        StartCoroutine(GetFriendData());
        yield return new WaitUntil(() => isRun == true);
        Debug.Log("Database Reading  "+ listData[0].avatarUrl);

        // foreach (playerStruct player in listData)
        // {
        StartCoroutine(GetImage(listData[0].avatarUrl, listData[0].playerName, listData[0].level));
        //}
        yield return null;
    }


    IEnumerator GetFriendData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading");

        Query allItemQuery = db.Collection("player");
        allItemQuery
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                QuerySnapshot allItemQuerySnapshot = task.Result;
                foreach (DocumentSnapshot
                    documentSnapshot
                    in
                    allItemQuerySnapshot.Documents
                )
                {
                    objectData = documentSnapshot.ConvertTo<playerStruct>();
                    Debug.Log("Database Reading 1 " + objectData.avatarUrl);
                    listData.Add(objectData);
                }
                isRun = true;
            });
        yield return null;
    }
    IEnumerator GetImage(string dataImage, string Name, int level)
    {
        Debug.Log("Image Downloading");

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
                    Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    //UIImage.texture = texture;
                    Populate(sprite, Name, level);
                }
            });
        yield return null;
    }
}
