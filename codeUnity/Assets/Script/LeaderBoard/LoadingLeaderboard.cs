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
        loadLeaderboard();
    }
    void loadLeaderboard()
    {
        int countTop = 0;
        foreach (var item in ListPlayer_DataManager.Instance.returnListPlayerSortByLevel())
        {
            avtImg.GetComponent<RawImage>().texture = item.texture2D;
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
