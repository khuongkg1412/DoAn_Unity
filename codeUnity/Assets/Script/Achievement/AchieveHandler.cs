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

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        generateItem();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void TaskOnClick()
    {
        Debug.Log("On Click_" + gameObject.name);
    }

    void generateItem()
    {
        foreach (var item in Achievement_DataManager.Instance.Achievement)
        {
            GameObject itemObj = (GameObject)Instantiate(prefab, transform);
            itemObj.GetComponent<Achie_Item>().Populate(item, item.percentage);
        }
    }

}
