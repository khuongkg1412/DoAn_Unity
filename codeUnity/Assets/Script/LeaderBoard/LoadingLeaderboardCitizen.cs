using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;

public class LoadingLeaderboardCitizen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject topImg, topTxt, avtImg, namePlayer, citizenSaved, itemRow;

    bool isRun = false;

    void Start()
    {
        loadLeaderboard();
    }
    void loadLeaderboard()
    {
        int countTop = 0;
        foreach (var item in ListPlayer_DataManager.Instance.returnListPlayerSortBySavedCitizen())
        {
            avtImg.GetComponent<RawImage>().texture = item.texture2D;
            namePlayer.GetComponent<Text>().text = item.generalInformation.username_Player;
            citizenSaved.GetComponent<Text>().text = item.statistic["Citizen_Saved"].ToString();
            //set top image for Rank 1
            if (countTop == 0)
            {
                topImg.GetComponent<RawImage>().texture = loadingImageFromFilePath("Leaderboard_Image/1");
                topTxt.SetActive(false);
            }
            //set top image for Rank 2
            else if (countTop == 1)
            {
                topImg.GetComponent<RawImage>().texture = loadingImageFromFilePath("Leaderboard_Image/2");
                topTxt.SetActive(false);
            }
            //set top image for Rank 3
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
