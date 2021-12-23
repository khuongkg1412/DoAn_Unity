using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDaily : MonoBehaviour
{
    //FireBase Object
    FirebaseFirestore db;

    //Object to store that has been loaded from Firebase
    private ItemDailyStruct objectData;

    //Store the list of data object
    List<ItemDailyStruct> listItemDaily = new List<ItemDailyStruct>();

    //Let Coroutine run
    bool isDaily = false;

    // //TextTure,RawImage,Text
    public RawImage itemImage;

    public TMPro.TMP_Text itemName;

    public GameObject prefab; // This is our prefab object that will be exposed in the inspector

    public int numberToCreate; // number of objects to create. Exposed in inspector

    // Data of Object
    //Item Daily Data
    ItemDailyStruct
        itemDaily =
            new ItemDailyStruct {
                itemImage = "Store/ItemDaily/EnergyPills.png",
                itemName = "EnergyPills",
                itemType = "itemDaily",
                quantity = 10
            };

    ItemDailyStruct
        itemDaily2 =
            new ItemDailyStruct {
                itemImage = "Store/ItemDaily/PainKiller.png",
                itemName = "Pain Killers",
                itemType = "itemDaily",
                quantity = 40
            };

    //Start Program
    private void Start()
    {
        /*
         Run this function to add data to your firebase 
        */
        //AddData();
        /*
         Run this function to add data to your firebase 
        */
        StartCoroutine(setDatatoGO());
    }

    void Populate(Texture2D texture, string name)
    {
        GameObject newObj; // Create GameObject instance
        itemImage.texture = texture;
        itemName.text = name;
        newObj = (GameObject) Instantiate(prefab, transform);
    }

    /*
        Method AddData() : Add object ItemDailyStruct to FireBase
    */
    public void AddData()
    {
        Debug.Log("Database Added");

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
    }

    /*
        Method GetData() : Add object ItemDailyStruct to FireBase
    */
    IEnumerator GetData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading");
        Query itemDailyQuery =
            db.Collection("Store").WhereEqualTo("itemType", "itemDaily");
        itemDailyQuery
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
                    objectData = documentSnapshot.ConvertTo<ItemDailyStruct>();
                    listItemDaily.Add (objectData);
                }
                isDaily = true;
            });
        yield return null;
    }

    IEnumerator GetImage(string dataImage, string dataName)
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
                    texture.LoadImage (fileContents);

                    //UIImage.texture = texture;
                    Populate (texture, dataName);
                }
            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        StartCoroutine(GetData());

        //Run When the data from Daily is loaded
        yield return new WaitUntil(() => isDaily == true);

        //Wait for data has been load from firebase
        StartCoroutine(GetImage(listItemDaily[0].itemImage, listItemDaily[0].itemName));
        StartCoroutine(GetImage(listItemDaily[1].itemImage, listItemDaily[1].itemName));
        StartCoroutine(GetImage(listItemDaily[2].itemImage, listItemDaily[2].itemName));
        StartCoroutine(GetImage(listItemDaily[0].itemImage, listItemDaily[0].itemName));
        StartCoroutine(GetImage(listItemDaily[1].itemImage, listItemDaily[1].itemName));
        StartCoroutine(GetImage(listItemDaily[2].itemImage, listItemDaily[2].itemName));
        StartCoroutine(GetImage(listItemDaily[0].itemImage, listItemDaily[0].itemName));
        StartCoroutine(GetImage(listItemDaily[1].itemImage, listItemDaily[1].itemName));
        StartCoroutine(GetImage(listItemDaily[2].itemImage, listItemDaily[2].itemName));
        // foreach (var objectItem in listItemDaily)
        // {
        //     StartCoroutine(GetImage(objectItem.itemImage, objectItem.itemName));
        // }
        yield return null;
    }
}
