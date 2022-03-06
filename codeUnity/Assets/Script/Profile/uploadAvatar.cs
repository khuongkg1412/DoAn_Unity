using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//Firebase
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Storage;
//For Picking files
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class uploadAvatar : MonoBehaviour
{
    FirebaseStorage storage;
    FirebaseFirestore db;
    StorageReference storageReference;
    public Image localImg;
    static string paths;
    //static bool reviewImgActived = false;
    public static bool isModify = false;
    public static bool isUpDone = false;


    // Start is called before the first frame update
    void Start()
    {
        isModify = false;
    }

    void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
       {
           Debug.Log("Image path: " + path);
           if (path != null)
           {
               // Create Texture from selected image
               Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
               if (texture == null)
               {
                   Debug.Log("Couldn't load texture from " + path);
                   return;
               }

               StartCoroutine(ReviewImageFromLocal(texture, path));
           }
       }, "Select a PNG image", "image/png");
        Debug.Log("Permission result: " + permission);
    }

    public void OnPickImageButtonClick()
    {
        PickImage(1024);
    }

    public void onOkButtonClick()
    {
        if (paths != null) StartCoroutine(UpImageFromLocal(paths));
        //GameObject.Find("Box Change avatar").gameObject.SetActive(false);
    }

    public void onCancelButtonClick()
    {
        paths = null;
        //GameObject.Find("Box Change avatar").gameObject.SetActive(false);
        // if(reviewImgActived == true) {
        //     GameObject.Find("../Box Change avatar/Panel/Avatar holder/newAvatar").gameObject.SetActive(false);
        //     GameObject.Find("../Box Change avatar/Panel/Avatar holder/IconDuy").gameObject.SetActive(true);
        // }
    }

    IEnumerator ReviewImageFromLocal(Texture2D texture, string localFilePath)
    {
        //Review image before upload

        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        localImg.GetComponent<Image>().sprite = sprite;
        //reviewImgActived = true;
        paths = localFilePath;
        yield return null;
    }

    IEnumerator UpImageFromLocal(string localFilePath)
    {
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://ltd2k-fptk14.appspot.com/PlayerAvatar");

        string fileName = Player_DataManager.Instance.Player.ID;


        // Create a reference to the file you want to upload
        StorageReference newAvatarsRef = storageReference.Child(fileName);

        // Create file metadata including the content type
        var newMetadata = new MetadataChange();
        newMetadata.ContentType = "image/png";

        // Upload the file to the path 
        newAvatarsRef.PutFileAsync("file://" + localFilePath, newMetadata)
            .ContinueWith((Task<StorageMetadata> task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else
                {
                    Debug.Log("Finished uploading...");
                    isUpDone = true;
                }
            });
        paths = null;
        changeCurrentAvatar("PlayerAvatar/" + fileName);
        yield return null;
    }

    void changeCurrentAvatar(string newPath)
    {
        Player_DataManager.Instance.changeAvatar(newPath);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}