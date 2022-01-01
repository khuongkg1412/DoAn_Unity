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
//using UnityEngine.Windows;
using UnityEngine.SceneManagement;
public class uploadAvatar : MonoBehaviour
{
    FirebaseStorage storage;
    FirebaseFirestore db;
    StorageReference storageReference;
    public Image localImg;
    static string paths;
    static bool reviewImgActived = false;
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

               StartCoroutine(ReviewImageFromLocal(path));
               // Assign texture to a temporary quad and destroy it after 5 seconds
               GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
               quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
               quad.transform.forward = Camera.main.transform.forward;
               quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);
               Material material = quad.GetComponent<Renderer>().material;
               if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                   material.shader = Shader.Find("Legacy Shaders/Diffuse");
               material.mainTexture = texture;
               Destroy(quad, 5f);
               // If a procedural texture is not destroyed manually, 
               // it will only be freed after a scene change
               Destroy(texture, 5f);
           }
       }, "Select a PNG image", "image/png");
        Debug.Log("Permission result: " + permission);
    }

    public void OnPickImageButtonClick()
    {
        //paths = EditorUtility.OpenFilePanel("Show all image (.png", "", "png,jpg");
        //StartCoroutine(ReviewImageFromLocal(paths));
        PickImage(512);
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

    IEnumerator ReviewImageFromLocal(string localFilePath){
         //Review image before upload

        //byte[] fileContents = File.ReadAllBytes(localFilePath);
        Texture2D texture = new Texture2D(1, 1);
        //texture.LoadImage(fileContents);
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        localImg.GetComponent<Image>().sprite = sprite;
        reviewImgActived = true;
        paths = localFilePath;
        yield return null;
        //StartCoroutine(UpImageFromLocal(localFilePath));
    }

    IEnumerator UpImageFromLocal(string localFilePath)
    {
        storage = FirebaseStorage.DefaultInstance;
        // Create a root reference
        storageReference = storage.GetReferenceFromUrl("gs://ltd2k-fptk14.appspot.com/PlayerAvatar");
        // File located on disk
        string[] fName = localFilePath.Split('/');
        string fileName = fName[fName.Length - 1];
        //byte[] fileContents = File.ReadAllBytes(localFilePath);

        // Create a reference to the file you want to upload
        StorageReference newAvatarsRef = storageReference.Child(fileName);

        // Create file metadata including the content type
        var newMetadata = new MetadataChange();
        newMetadata.ContentType = "image/png";

        // Upload the file to the path 
        newAvatarsRef.PutFileAsync(localFilePath, newMetadata)
            .ContinueWith((Task<StorageMetadata> task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log("Hủy rồi, Lỗi rồi");
                }
                else
                {
                    // Metadata contains file metadata such as size, content-type, and download URL.
                    StorageMetadata metadata = task.Result;
                    string md5Hash = metadata.Md5Hash;
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
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("player").Document("ID");
        Dictionary<string, object> update = new Dictionary<string, object>{{"avatarUrl",newPath }};
        
        docRef.SetAsync(update, SetOptions.MergeAll).ContinueWithOnMainThread(task =>
        {
            if (true)
            {
                Debug.Log("Modify successfully");
                isModify = true;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
            else
            {
                Debug.Log("Modify crashing");
            }
        });
    }
}