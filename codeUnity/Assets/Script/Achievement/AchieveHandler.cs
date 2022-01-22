using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
public class AchieveHandler : MonoBehaviour
{
    FirebaseFirestore db;
    public GameObject prefab;
    public GameObject content;
    Slider assd;
    List<achieveItemStruct> listData = new List<achieveItemStruct>();
    bool isRun = false;
    private achieveItemStruct objectData;

    // Start is called before the first frame update
    void Start()
    {
        generateItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Populate(AchievementStruct achievement, double percentage)
    {
        GameObject itemObj = (GameObject)Instantiate(prefab, transform);

        itemObj.transform.Find("ContentAchive").gameObject.GetComponent<Text>().text = achievement.title_Achievement;
        //Set text concurrency by checking which concurrency is more than 0 
        Concurrency rewardReceived = achievement.concurrency;
        if (rewardReceived.Coin > 0 && rewardReceived.Diamond <= 0)
        {
            itemObj.transform.Find("RewardButton/ContentReward").gameObject.GetComponent<Text>().text = "" + achievement.concurrency.Coin;
        }
        else
        {
            itemObj.transform.Find("RewardButton/ContentReward").gameObject.GetComponent<Text>().text = "" + achievement.concurrency.Diamond;
        }
        if (percentage < 1)
        {
            itemObj.transform.Find("ProgressAchive").gameObject.GetComponent<Slider>().value = (float)percentage;
            itemObj.transform.Find("ProgressAchive/ContentSlider").gameObject.GetComponent<TMPro.TMP_Text>().text = (float)percentage * 100 + "%";
        }
        else if (percentage >= 1)
        {
            percentage = 100;
            itemObj.transform.Find("ProgressAchive").gameObject.GetComponent<Slider>().value = (float)percentage;
            itemObj.transform.Find("ProgressAchive/ContentSlider").gameObject.GetComponent<TMPro.TMP_Text>().text = (float)percentage + "%";
            itemObj.transform.Find("ProgressAchive/Fill Area/Fill").gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
            itemObj.transform.Find("RewardButton").GetComponent<Image>().color = new Color(0, 1, 0);
        }


    }

    void generateItem()
    {
        Debug.Log("Go");
        foreach (var item in Achievement_DataManager.Instance.Achievement)
        {
            Populate(item, item.percentage);
        }
    }


    IEnumerator GetFriendData()
    {
        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Database Reading");

        Query allItemQuery = db.Collection("Achievement");
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
                    objectData = documentSnapshot.ConvertTo<achieveItemStruct>();
                    Debug.Log("Database Reading 1 2 3");
                    listData.Add(objectData);
                }
                isRun = true;
            });
        yield return null;
    }
}
