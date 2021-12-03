using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

//For Picking files
using UnityEngine.Assertions;
using System.Threading.Tasks;


public class uploadAvatar : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference storageReference;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnButtonClick()
    {
        StartCoroutine(UpImageFromLocal("ID player vô đây"));

    }

    IEnumerator UpImageFromLocal(string PlayerID)
    {
        // Create a root reference
        StorageReference storageRef = storage.RootReference;

        // File located on disk
        string localFile = "content://" + Application.persistentDataPath;
        string fileName = "avatar" + PlayerID;

        // Create a reference to the file you want to upload
        StorageReference newAvatarsRef = storageRef.Child("PlayerAvatar/" + fileName);

        // Create file metadata including the content type
        var newMetadata = new MetadataChange();
        newMetadata.ContentType = "image/png";

        // Upload the file to the path "images/rivers.jpg"
        newAvatarsRef.PutFileAsync(localFile,newMetadata)
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

    // IEnumerator ShowLoadDialogCoroutine()
    // {

    //     yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

    //     Debug.Log(FileBrowser.Success);

    //     if (FileBrowser.Success)
    //     {
    //         // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
    //         for (int i = 0; i < FileBrowser.Result.Length; i++)
    //             Debug.Log(FileBrowser.Result[i]);

    //         Debug.Log("File Selected");
    //         byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
    //         //Editing Metadata
    //         var newMetadata = new MetadataChange();
    //         newMetadata.ContentType = "image/jpeg";

    //         //Create a reference to where the file needs to be uploaded
    //         StorageReference uploadRef = storageReference.Child("uploads/newFile.jpeg");
    //         Debug.Log("File upload started");
    //         uploadRef.PutBytesAsync(bytes, newMetadata).ContinueWithOnMainThread((task) =>
    //         {
    //             if (task.IsFaulted || task.IsCanceled)
    //             {
    //                 Debug.Log(task.Exception.ToString());
    //             }
    //             else
    //             {
    //                 Debug.Log("File Uploaded Successfully!");
    //             }
    //         });
    //     }
    // }
}
