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
    bool isRun = false;
    private ItemStruct objectData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateItem());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Populate(Sprite sprite)
    {
        Debug.Log("Dang o day");

        GameObject scrollItemObj = (GameObject)Instantiate(prefab, transform);

        scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
    }

    IEnumerator generateItem()
    {
        StartCoroutine(GetOutfitData());
        yield return new WaitUntil(() => isRun == true);
        Debug.Log("Database Reading  " + listShirt[0].image_Item);

        foreach (ItemStruct shirt in listShirt)
        {
            StartCoroutine(GetImage(shirt.image_Item));
        }
        yield return null;
    }


    IEnumerator GetOutfitData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading");

        Query allItemQuery = db.Collection("Outfits");
        allItemQuery
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                Debug.Log("Database Reading ");
                QuerySnapshot allItemQuerySnapshot = task.Result;
                foreach (DocumentSnapshot
                    documentSnapshot
                    in
                    allItemQuerySnapshot.Documents
                )
                {
                    objectData = documentSnapshot.ConvertTo<ItemStruct>();
                    Debug.Log("Database Reading 1 ");
                    listShirt.Add(objectData);
                }
                isRun = true;
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
                    Populate(sprite);
                }
            });
        yield return null;
    }
}
