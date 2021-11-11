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

    // Data of Object
    ItemDailyStruct
        item =
            new ItemDailyStruct {
                itemImage = "Store/ItemDaily/EnergyPills.png",
                itemName = "EnergyPills",
                itemType = "itemDaily",
                quantity = 10
            };

    ItemDailyStruct
        item2 =
            new ItemDailyStruct {
                itemImage = "Store/ItemDaily/PainKiller.png",
                itemName = "PainKillers",
                itemType = "itemDaily",
                quantity = 40
            };

    //Object to store that has been loaded from Firebase
    private ItemDailyStruct objectData;

    //Store the list of data object
    List<ItemDailyStruct> listData = new List<ItemDailyStruct>();

    //Let Coroutine run
    bool isRun = false;

    // //TextTure,RawImage,Text
    public RawImage itemImage;

    // public RawImage itemImage2;

    // public RawImage itemImage3;

    public TMPro.TMP_Text itemName;

    // public TMPro.TMP_Text itemName2;

    // public TMPro.TMP_Text itemName3;

    public GameObject prefab; // This is our prefab object that will be exposed in the inspector

    public int numberToCreate; // number of objects to create. Exposed in inspector

    //Start Program
    private void Start()
    {
       

        //AddData();
        StartCoroutine(setDatatoGO());
    }

    void Populate(Texture2D texture,string name)
    {
        GameObject newObj; // Create GameObject instance

        // for (int i = 0; i < numberToCreate; i++)
        // {
            // Create new instances of our prefab until we've created as many as we specified
            itemImage.texture = texture;
            itemName.text = name;
            newObj = (GameObject) Instantiate(prefab, transform);
            Debug.Log("Run");
            

        //}
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
        db.Collection("Store").AddAsync(item2);
    }

    /*
        Method GetData() : Add object ItemDailyStruct to FireBase
    */
    IEnumerator GetData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading");

        Query allItemQuery = db.Collection("Store");
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
                    objectData = documentSnapshot.ConvertTo<ItemDailyStruct>();
                    listData.Add (objectData);
                }
                isRun = true;
            });
        yield return null;
    }

    IEnumerator GetImage(string dataImage,string dataName)
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
                    Populate(texture,dataName);
                }
            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        StartCoroutine(GetData());
        yield return new WaitUntil(() => isRun == true);

        //Wait for data has been load from firebase
        StartCoroutine(GetImage(listData[0].itemImage, listData[0].itemName));
        StartCoroutine(GetImage(listData[1].itemImage, listData[1].itemName));
        StartCoroutine(GetImage(listData[2].itemImage, listData[2].itemName));


        // itemName2.text = listData[1].itemName;

        // itemName3.text = listData[2].itemName;
        yield return null;
    }
}
