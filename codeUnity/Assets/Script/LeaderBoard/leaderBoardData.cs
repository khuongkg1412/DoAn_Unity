using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class leaderBoardData : MonoBehaviour
{
    FirebaseFirestore db;

    private leaderBoardStruct objectData;

    List<leaderBoardStruct> listData = new List<leaderBoardStruct>();

    bool isRun = false;

    public RawImage flagImage;

    public RawImage playerAvatar;

    public RawImage playerRank;

    public Text playerLevel;

    public Text playerMap;

    public Text playerName;

    public GameObject prefab;

    public int numberToCreate;

    leaderBoardStruct
        rank1 =
            new leaderBoardStruct {
                flagImage = "LeaderBoard/Local/Rank1/VN.png",
                playerAvatar = "LeaderBoard/Local/Rank1/top1_avatar.png",
                playerLevel = 99,
                playerMap = 99,
                playerName = "Minh Hai",
                playerRank = "LeaderBoard/Local/Rank1/cup1.png"
            };

    private void Start()
    {
        StartCoroutine(setDatatoGO());
    }

    void Populate( // Texture2D textureFlag,
        Texture2D textureAvatar,
        // Texture2D textureRank,
        // string textLevel,
        // string textMap,
        string textName
    )
    {
        GameObject newObj;

        // flagImage.texture = textureFlag;
        playerAvatar.texture = textureAvatar;

        // playerRank.texture = textureRank;
        // playerLevel.text = textLevel;
        // playerMap.text = textMap;
        playerName.text = textName;
        newObj = (GameObject) Instantiate(prefab, transform);
        Debug.Log("Run");
    }

    IEnumerator GetData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading");

        Query leaderQuery = db.Collection("leaderBoard");
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
                        documentSnapshot.ConvertTo<leaderBoardStruct>();
                    listData.Add (objectData);
                }
                isRun = true;
            });
        yield return null;
    }

    IEnumerator GetImage(string dataAvatar, string dataName)
    {
        Debug.Log("Image Downloading");

        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataAvatar);

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
                    Texture2D textureAvatar = new Texture2D(1, 1);
                    textureAvatar.LoadImage (fileContents);

                    //UIImage.texture = texture;
                    Populate (textureAvatar, dataName);
                }
            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        StartCoroutine(GetData());
        yield return new WaitUntil(() => isRun == true);

        //Wait for data has been load from firebase
        StartCoroutine(GetImage(listData[0].playerAvatar,
        listData[0].playerName));

        yield return null;
    }
}
