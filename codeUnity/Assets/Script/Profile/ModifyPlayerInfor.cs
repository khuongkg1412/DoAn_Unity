using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModifyPlayerInfor : MonoBehaviour
{
    FirebaseFirestore db;
    private PlayerStruct player;
    bool isModify = uploadAvatar.isModify;
    bool isUpDone = uploadAvatar.isUpDone;
    bool isRun = false;
    public Image currentAvatar;

    // Start is called before the first frame update
    void Start()
    {
        if (isModify == true)
        {
            StartCoroutine(lam());
            //Debug.Log("isModify: " + isModify);
            isModify = false;
        }
    }

    IEnumerator lam()
    {
        StartCoroutine(updatePlayerInfor());

        Debug.Log("isModify 1: " + isUpDone);
        //yield return new WaitUntil(() => isUpDone == true);
        //yield return new WaitWhile(() => isUpDone == true)
        yield return new WaitForSeconds(7);
        Debug.Log("isModify 2: " + player.avatar_Player);

        StartCoroutine(GetImage(player.avatar_Player));
    }
    IEnumerator updatePlayerInfor()
    {
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("player").Document("ID");
        docRef.GetSnapshotAsync().ContinueWith(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));

                }
                player = snapshot.ConvertTo<PlayerStruct>();
                Debug.Log("Player : " + player.avatar_Player);
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
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
        const long maxAllowedSize = 1 * 1024 * 1024;
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
                    currentAvatar.sprite = sprite;
                    //Populate(sprite, Name, level);
                }
            });
        yield return null;
    }

    public static string typeOfOutfit;
    public void openWindowForShirt()
    {
        typeOfOutfit = "Shirt";
    }
    public void openWindowForPants()
    {
        typeOfOutfit = "Pants";
    }
    public void openWindowForAccessory()
    {
        typeOfOutfit = "Accessory";
    }
    public void openWindowForShoes()
    {
        typeOfOutfit = "Shoes";
    }
}