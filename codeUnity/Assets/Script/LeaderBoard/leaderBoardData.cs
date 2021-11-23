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

    int count = 0;

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

    void Populate(
        int textLevel,
        int textMap,
        string textName
    )
    {
        GameObject newObj;

        playerLevel.text = textLevel.ToString();
        playerMap.text = textMap.ToString();
        playerName.text = textName;
        newObj = (GameObject) Instantiate(prefab, transform);
        Debug.Log("Run");
    }

    IEnumerator GetData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading " + Time.time);

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
                    texture.LoadImage (fileContents);
                    if (num == 1)
                    {
                        playerAvatar.texture = texture;
                        count++;
                    }
                    else if (num == 2)
                    {
                        playerRank.texture = texture;
                        count++;
                    }
                    else
                    {
                        flagImage.texture = texture;
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
        StartCoroutine(GetImage(listData[0].playerAvatar, 1));
        StartCoroutine(GetImage(listData[0].playerRank, 2));
        StartCoroutine(GetImage(listData[0].flagImage, 3));

        yield return new WaitUntil(() => count == 3);
        Debug.Log("Done" + Time.time);

        //UIImage.texture = texture;
        Populate(
        listData[0].playerLevel,
        listData[0].playerMap,
        listData[0].playerName);
        yield return null;
    }
}
