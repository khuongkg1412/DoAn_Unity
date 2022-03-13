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
    public Image currentAvatar;
    public Text Name, Level;

    // Start is called before the first frame update
    private void Awake()
    {
        loadplayerInfor();
    }

    void loadplayerInfor()
    {
        //load basic infor 
        player = Player_DataManager.Instance.Player;
        StartCoroutine(GetImage(player.generalInformation.avatar_Player));
        Name.text = player.generalInformation.username_Player;
        Level.text = "LV " + player.level.level;


        //load outfit
        GameObject.Find("accesory/head").GetComponent<Image>().sprite = getOutfitImage(player.currentOutfit.currentAccesory);
        GameObject.Find("shirt/body").GetComponent<Image>().sprite = getOutfitImage(player.currentOutfit.currentShirt);
        GameObject.Find("shoes/foot").GetComponent<Image>().sprite = getOutfitImage(player.currentOutfit.currentShoes);
        GameObject.Find("pant/leg").GetComponent<Image>().sprite = getOutfitImage(player.currentOutfit.currentPant);


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
                    currentAvatar.sprite = sprite;
                }
            });
        yield return null;
    }

    public static int typeOfOutfit;
    public void openWindowForShirt()
    {
        typeOfOutfit = 0;
    }
    public void openWindowForPants()
    {
        typeOfOutfit = 1;
    }
    public void openWindowForShoes()
    {
        typeOfOutfit = 2;
    }
    public void openWindowForAccessory()
    {
        typeOfOutfit = 3;
    }

    private Sprite getOutfitImage(string ID)
    {
        Texture2D OutfitImage;
        foreach (ItemStruct item in Item_DataManager.Instance.Item)
        {
            if (item.ID.Equals(ID))
            {
                OutfitImage = item.texture2D;
                Sprite sprite = Sprite.Create(OutfitImage, new Rect(0.0f, 0.0f, OutfitImage.width, OutfitImage.height), new Vector2(0.5f, 0.5f), 100.0f);
                return sprite;
            }
        }
        return null;
    }
}