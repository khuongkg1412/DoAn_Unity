using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;

public class LoadingLeaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject topImg, topTxt, avtImg, namePlayer, mapPlayer, levelPlayer, itemRow;

    bool isRun = false;

    void Start()
    {
        StartCoroutine(loadLeaderboard());
    }
    IEnumerator loadLeaderboard()
    {
        int countTop = 0;
        foreach (var item in ListPlayer_DataManager.Instance.listPlayer)
        {

            StartCoroutine(GetImage(item.generalInformation.avatar_Player));
            yield return new WaitUntil(() => isRun == true);
            namePlayer.GetComponent<Text>().text = item.generalInformation.username_Player;
            mapPlayer.GetComponent<Text>().text = item.level.stage.ToString();
            levelPlayer.GetComponent<Text>().text = item.level.level.ToString();
            if (countTop == 0)
            {
                topImg.GetComponent<RawImage>().texture = loadingImageFromFilePath("Leaderboard_Image/1");
                topTxt.SetActive(false);
            }
            else if (countTop == 1)
            {
                topImg.GetComponent<RawImage>().texture = loadingImageFromFilePath("Leaderboard_Image/2");
                topTxt.SetActive(false);
            }
            else if (countTop == 2)
            {
                topImg.GetComponent<RawImage>().texture = loadingImageFromFilePath("Leaderboard_Image/3");
                topTxt.SetActive(false);
            }
            else
            {
                topTxt.SetActive(true);
                topImg.SetActive(false);
                topTxt.GetComponent<Text>().text = (countTop + 1).ToString();
            }

            Instantiate(itemRow, transform);
            countTop++;
        }
        yield return null;
    }

    IEnumerator GetImage(string dataImage)
    {
        isRun = false;
        // Get a reference to the storage service, using the default Firebase App
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        // Create a storage reference from our storage service
        StorageReference storageRef = storage.GetReference(dataImage);

        // Download in memory with a maximum allowed size of 1MB (1 * 1024 * 1024 bytes)
        const long maxAllowedSize = 1 * 1024 * 1024;
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
                   avtImg.GetComponent<RawImage>().texture = texture;
                   isRun = true;
               }

           });
        yield return null;
    }
    Texture2D loadingImageFromFilePath(string Filepath)
    {
        //Check filepath is valid
        if (Resources.Load<Sprite>(Filepath) != null)
        {
            //Return image in Texture2D type
            return Resources.Load<Texture2D>(Filepath);
        }
        return null;
    }


}
