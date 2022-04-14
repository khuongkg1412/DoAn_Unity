using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achie_Item : MonoBehaviour
{
    AchievementStruct item;
    [SerializeField]
    GameObject ContentAchive, ContentReward, ProgressAchive, ContentSlider, Fill, RewardButton;

    public void rewardClicked()
    {
        if (ProgressAchive.GetComponent<Slider>().value == 1)
        {
            Player_DataManager.Instance.player_GetRewardAchievement(item);
            checkReceivedAchievement();
        }
        else
        {
            Debug.Log("Not Possible to claim reward");
        }

    }

    public void Populate(AchievementStruct achievement, double percentage)
    {
        //Set general achievement object
        item = achievement;
        //Set title achievement
        ContentAchive.GetComponent<Text>().text = achievement.title_Achievement;
        //Set text concurrency by checking which concurrency is more than 0 
        ContentReward.gameObject.GetComponent<Text>().text = "" + achievement.concurrency.Diamond;
        //Set slider value base on percentage of achievement
        if (percentage < 1) //Not complete achievement
        {
            ProgressAchive.GetComponent<Slider>().value = (float)percentage;
            ContentSlider.gameObject.GetComponent<TMPro.TMP_Text>().text = (float)percentage * 100 + "%";
        }
        else if (percentage >= 1) //Completed Achievement
        {
            percentage = 100; //Full percentage
            ProgressAchive.GetComponent<Slider>().value = (float)percentage;
            ContentSlider.GetComponent<TMPro.TMP_Text>().text = (float)percentage + "%";
            //Change color to green for slider and button
            Fill.GetComponent<Image>().color = new Color(0, 1, 0);
            RewardButton.GetComponent<Image>().color = new Color(0, 1, 0);
        }
        //Set listener Function for click button
        RewardButton.GetComponent<Button>().onClick.AddListener(rewardClicked);
        checkReceivedAchievement();
    }

    void checkReceivedAchievement()
    {
        foreach (var receivedItem in Player_DataManager.Instance.achivementReceived_Player)
        {
            if (receivedItem.ID == item.ID)
            {
                //Change color to gray for achievement that has been received from Player
                Color gray = new Color((float)0.5, (float)0.5, (float)0.5);
                Fill.GetComponent<Image>().color = gray;
                RewardButton.GetComponent<Image>().color = gray;
                //set interacable to false with achievement that has been claimed
                RewardButton.GetComponent<Button>().interactable = false;
            }
        }
    }
}
