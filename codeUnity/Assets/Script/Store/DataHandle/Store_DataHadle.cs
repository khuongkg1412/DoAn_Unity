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

    List<ItemStruct> listDataItemDaily = new List<ItemStruct>();

    List<ItemStruct> listDataItemWeekly = new List<ItemStruct>();

    List<ItemStruct> listDataItemChest = new List<ItemStruct>();

    public GameObject verticalDaily;

    public GameObject verticalWeekly;

    public GameObject verticalChest;

    public TMPro.TMP_Text coin, diamond;

    //Start the scence this method would run 
    private void Start()
    {
        //Start load data into the prototype
        setDatatoGO();

    }

    private void Update()
    {
        //Update the coin and diamond of player every frame
        coin.text = Player_DataManager.Instance.Player.concurrency.Coin.ToString();
        diamond.text = Player_DataManager.Instance.Player.concurrency.Diamond.ToString();
    }
    /*
    Load data from the exist list in the system, then initiate the object in the scence
    */
    private void setDatatoGO()
    {
        //call to static method to load data into list of three type of store item
        listDataItemDaily = Item_DataManager.Instance.itemDaily();
        listDataItemWeekly = Item_DataManager.Instance.itemWeekly();
        listDataItemChest = Item_DataManager.Instance.itemChest();
        /*
        Initiate the object in the scence
        */
        foreach (var objectItem in listDataItemDaily)
        {
            Populate(verticalDaily, objectItem);
        }

        foreach (var objectItem in listDataItemWeekly)
        {
            Populate(verticalWeekly, objectItem);
        }

        foreach (var objectItem in listDataItemChest)
        {
            Populate(verticalChest, objectItem);
        }
    }

    //Instaniate the object item for each one
    void Populate(GameObject verticalObject, ItemStruct Item)
    {
        //The prototype of each item in store
        GameObject prefab = GameObject.Find("BoxItem"); // Create GameObject instance
        //Set data in that prototype 
        dataImage.texture = Item.texture2D;
        dataImage.SetNativeSize();
        itemName.text = Item.name_Item;
        //Instaniate the object item
        GameObject item = Instantiate(prefab, verticalObject.transform);
        //Set data type for each prototype
        item.GetComponent<OpenItem>().dataItem = Item;
    }


    // Data of Object
    //Item Daily Data
    ItemStruct item1 = new ItemStruct
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
        type_Item = (int)TypeItem.ItemDaily,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 10,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct item2 = new ItemStruct
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
        type_Item = (int)TypeItem.ItemDaily,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 20,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct item3 = new ItemStruct
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
        type_Item = (int)TypeItem.ItemDaily,
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
        type_Item = (int)TypeItem.ItemWeekly,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct item5 = new ItemStruct
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
        type_Item = (int)TypeItem.ItemWeekly,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct item6 = new ItemStruct
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
        type_Item = (int)TypeItem.ItemWeekly,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct item7 = new ItemStruct
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
        type_Item = (int)TypeItem.Chest,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct item8 = new ItemStruct
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
        type_Item = (int)TypeItem.Chest,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 0
                    }
    };
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
}
