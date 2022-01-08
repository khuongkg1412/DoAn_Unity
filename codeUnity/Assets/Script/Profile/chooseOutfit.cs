using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;

public class chooseOutfit : MonoBehaviour
{
    FirebaseFirestore db;
    public GameObject prefab;
    public GameObject content;
    List<ItemStruct> listShirt = new List<ItemStruct>();
    List<ItemStruct> listPants = new List<ItemStruct>();
    List<ItemStruct> listAccessory = new List<ItemStruct>();
    List<ItemStruct> listShoes = new List<ItemStruct>();
    bool isRun = false;
    private ItemStruct objectData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateItem(ModifyPlayerInfor.typeOfOutfit));
    }


    void Populate(Sprite sprite)
    {
        Debug.Log("Dang o day");

        GameObject scrollItemObj = (GameObject)Instantiate(prefab, transform);

        scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
        scrollItemObj.transform.Find("Outfit Item").gameObject.GetComponentInParent<Button>().enabled = false;
    }

    IEnumerator generateItem(string typeOfItem)
    {
        StartCoroutine(GetOutfitData(typeOfItem));
        yield return new WaitUntil(() => isRun == true);
        Debug.Log("Database Reading  " + listShirt[0].image_Item);

        foreach (ItemStruct shirt in listShirt)
        {
            StartCoroutine(GetImage(shirt.image_Item));
        }
        yield return null;
    }


    IEnumerator GetOutfitData(string typeOfItem)
    {
        switch (typeOfItem)
        {
            case "Shirt":
                foreach (ItemStruct item in listShirt) { }

                break;
            case "Pants":

                break;
            case "Accessory":

                break;
            case "Shoes":

                break;
        }
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
                    // Uh-oh, an error occurred!
                    Debug.LogException(task.Exception);
                }
                else
                {
                    byte[] fileContents = task.Result;
                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(fileContents);
                    Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

                    Populate(sprite);
                }
            });
        yield return null;
    }
}
