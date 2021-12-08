using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEditor;
using UnityEngine.UI;

//For Picking files
using UnityEngine.Assertions;
using System.Threading.Tasks;
using UnityEngine.Windows;

public class uploadAvatar : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference storageReference;
    public Image localImg;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPickImageButtonClick()
    {
        string path = EditorUtility.OpenFilePanel("Show all image (.png", "", "png,jpg");
       
        StartCoroutine(ReviewImageFromLocal(path));
    }

    public void onOkButtonClick(){
        //StartCoroutine(UpImageFromLocal(path));
    }
    public void onCancelButtonClick(){
    
    }

    IEnumerator ReviewImageFromLocal(string localFilePath)
    {
        //Review image before upload

        byte[] fileContents = File.ReadAllBytes(localFilePath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(fileContents);
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        localImg.GetComponent<Image>().sprite = sprite;
        yield return null;

        StartCoroutine(UpImageFromLocal(localFilePath));
    }

    IEnumerator UpImageFromLocal(string localFilePath){
        
        storage = FirebaseStorage.DefaultInstance;
        // Create a root reference
        storageReference = storage.GetReferenceFromUrl("gs://coviddefusegame.appspot.com/PlayerAvatar");

        // File located on disk
        
        string[] fName = localFilePath.Split('/');
        string fileName = fName[fName.Length-1];
        byte[] fileContents = File.ReadAllBytes(localFilePath);

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
                }
            });
        yield return null;
    }

    /*IEnumerator ShowLoadDialogCoroutine()
    {

        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            Debug.Log("File Selected");
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
            //Editing Metadata
            var newMetadata = new MetadataChange();
            newMetadata.ContentType = "image/jpeg";

            //Create a reference to where the file needs to be uploaded
            StorageReference uploadRef = storageReference.Child("uploads/newFile.jpeg");
            Debug.Log("File upload started");
            uploadRef.PutBytesAsync(bytes, newMetadata).ContinueWithOnMainThread((task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else
                {
                    Debug.Log("File Uploaded Successfully!");
                }
            });
        }
    }*/

}
