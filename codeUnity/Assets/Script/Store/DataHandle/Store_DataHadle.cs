using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store_DataHadle : MonoBehaviour
{
    public RawImage dataImage;

    public TMPro.TMP_Text itemName;

    bool isWeekly = false;

    bool isDaily = false;

    bool isChest = false;

    // Data of Object
    //Item Daily Data
    ItemStruct
        item1 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 100,
                    Diamond = 0
                },
                description_Item = "Heal Pills is name of Item",
                image_Item = "Item_Image/HealPills",
                name_Item = "Heal Pills",
                rate_Item = 1,
                type_Item = "Medicine_DailyItem",
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 10,
                        SPD_Numeral = 0
                    }
            };

    ItemStruct
        item2 = new ItemStruct
        {
            concurrency = new Concurrency
            {
                Coin = 200,
                Diamond = 0
            },
            description_Item = "Energy Pills is name of Item",
            image_Item = "Item_Image/EnergyPills",
            name_Item = "Energy Pills",
            rate_Item = 2,
            type_Item = "Medicine_DailyItem",
            numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 20,
                        SPD_Numeral = 0
                    }
        };

    ItemStruct
        item3 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 250,
                    Diamond = 0
                },
                description_Item = "Pain Killers is name of Item",
                image_Item = "Item_Image/PainKiller",
                name_Item = "Pain Killers",
                rate_Item = 2,
                type_Item = "Medicine_DailyItem",
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
            };

    ItemStruct item4 = new ItemStruct
    {
        concurrency = new Concurrency
        {
            Coin = 250,
            Diamond = 0
        },
        description_Item = "Yellow Tube is name of Item",
        image_Item = "Item_Image/blood-test (1)",
        name_Item = "Yellow Tube",
        rate_Item = 2,
        type_Item = "Medicine_WeeklyItem",
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct
        item5 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 250,
                    Diamond = 0
                },
                description_Item = "Test Kit is name of Item",
                image_Item = "Item_Image/image 58",
                name_Item = "Test Kit",
                rate_Item = 2,
                type_Item = "Medicine_WeeklyItem",
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
            };

    ItemStruct
        item6 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 0,
                    Diamond = 50
                },
                description_Item = "Test Sample is name of Item",
                image_Item = "Item_Image/blood-test",
                name_Item = "Test Sample",
                rate_Item = 2,
                type_Item = "Medicine_WeeklyItem",
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
            };

    ItemStruct
        item7 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 0,
                    Diamond = 50
                },
                description_Item = "Common Chest is name of Item",
                image_Item = "Item_Image/KitCommon",
                name_Item = "Common Chest",
                rate_Item = 2,
                type_Item = "Chest",
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 0
                    }
            };

    ItemStruct
        item8 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 0,
                    Diamond = 100
                },
                description_Item = "Rare Chest is name of Item",
                image_Item = "Item_Image/KitRare",
                name_Item = "Rare Chest",
                rate_Item = 2,
                type_Item = "Chest",
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 0
                    }
            };

    List<ItemStruct> listDataItemDaily = new List<ItemStruct>();

    List<ItemStruct> listDataItemWeekly = new List<ItemStruct>();

    List<ItemStruct> listDataItemChest = new List<ItemStruct>();

    public GameObject verticalDaily;

    public GameObject verticalWeekly;

    public GameObject verticalChest;

    private void Start()
    {
        StartCoroutine(setDatatoGO());
    }

    public void AddData()
    {
        Debug.Log("Database Added");

        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;

        //Get Collection And Document
        DocumentReference doc = db.Collection("Item").Document();
        doc.SetAsync(item1);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item2);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item3);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item4);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item5);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item6);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item7);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item8);
    }

    IEnumerator GetDataDaily()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Query itemDailyQuery =
            db
                .Collection("Item")
                .WhereEqualTo("type_Item", "Medicine_DailyItem");
        itemDailyQuery
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
                    ItemStruct objectData =
                        documentSnapshot.ConvertTo<ItemStruct>();
                    listDataItemDaily.Add(objectData);
                }
                isDaily = true;
            });
        yield return null;
    }

    IEnumerator GetDataWeekly()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Query itemDailyQuery =
            db
                .Collection("Item")
                .WhereEqualTo("type_Item", "Medicine_WeeklyItem");
        itemDailyQuery
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
                    ItemStruct objectData =
                        documentSnapshot.ConvertTo<ItemStruct>();
                    listDataItemWeekly.Add(objectData);
                }
                isWeekly = true;
            });
        yield return null;
    }

    IEnumerator GetDataChest()
    {
        //FireBase Object
        FirebaseFirestore db;

        //db connection
        db = FirebaseFirestore.DefaultInstance;
        Query itemDailyQuery =
            db.Collection("Item").WhereEqualTo("type_Item", "Chest");
        itemDailyQuery
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
                    ItemStruct objectData =
                        documentSnapshot.ConvertTo<ItemStruct>();
                    listDataItemChest.Add(objectData);
                }
                isChest = true;
            });
        yield return null;
    }

    IEnumerator setDatatoGO()
    {
        StartCoroutine(GetDataDaily());
        StartCoroutine(GetDataWeekly());
        StartCoroutine(GetDataChest());

        //Run When the data from Daily is loaded
        yield return new WaitUntil(() =>
                    isDaily == true && isWeekly == true && isChest == true);

        //Wait for data has been load from firebase
        // loadingImageFromFilePath( listData[0].image_Item);
        foreach (var objectItem in listDataItemDaily)
        {
            Populate(objectItem.name_Item,
            objectItem.image_Item,
            verticalDaily,
            objectItem);
        }

        foreach (var objectItem in listDataItemWeekly)
        {
            Populate(objectItem.name_Item,
            objectItem.image_Item,
            verticalWeekly,
            objectItem);
        }

        foreach (var objectItem in listDataItemChest)
        {
            Populate(objectItem.name_Item,
            objectItem.image_Item,
            verticalChest,
            objectItem);
        }
        yield return null;
    }

    Texture2D loadingImageFromFilePath(string Filepath)
    {
        if (Resources.Load<Sprite>(Filepath) != null)
        {
            return Resources.Load<Texture2D>(Filepath);
        }
        return null;
    }

    void Populate(
        string name,
        string filePath,
        GameObject verticalObject,
        ItemStruct typeItem
    )
    {
        GameObject prefab = GameObject.Find("BoxItem"); // Create GameObject instance

        dataImage.texture = loadingImageFromFilePath(filePath);
        dataImage.SetNativeSize();
        itemName.text = name;
        GameObject item = Instantiate(prefab, verticalObject.transform);
        item.GetComponent<OpenItem>().dataItem = typeItem;
    }
}
