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
    List<achieveItemStruct> listData = new List<achieveItemStruct>();
    bool isRun = false;
    private achieveItemStruct objectData;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generateItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Populate(string title, int prize)
    {
        Debug.Log("Dang o day " + title);

        GameObject itemObj = (GameObject) Instantiate(prefab, transform);
       
        itemObj.transform.Find("ContentAchive").gameObject.GetComponent<Text>().text = title;
        itemObj.transform.Find("RewardButton/ContentReward").gameObject.GetComponent<Text>().text = "" + prize;
        yield return true;
    }

    IEnumerator generateItem()
    {
        StartCoroutine(GetFriendData());
        yield return new WaitUntil(() => isRun == true);
        Debug.Log("Database Counting "+ listData.Count);

        foreach(achieveItemStruct item in listData)
        {
            StartCoroutine(Populate(item.achieveTitle,item.achievePrize));
        }
        yield return null;
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
