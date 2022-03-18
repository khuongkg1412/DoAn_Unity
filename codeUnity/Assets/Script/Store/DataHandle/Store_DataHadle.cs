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
        // AddData();

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
        //dataImage.SetNativeSize();
        itemName.text = Item.name_Item;
        //Instaniate the object item
        GameObject item = Instantiate(prefab, verticalObject.transform);
        //Set data type for each prototype
        item.GetComponent<OpenItem>().dataItem = Item;
    }


    // Data of Object
    //Item Daily Data
    ItemStruct itemReal1 = new ItemStruct
    {
        concurrency = new Concurrency
        {
            Coin = 0,
            Diamond = 0
        },
        description_Item = "Revive is one of the rarest buff in the game. Using Revive, you have one chance to revive from your dead. Can't buy this item in shop. You only get it from Chest.",
        image_Item = "Item_Image/Revive",
        name_Item = "Revive",
        rate_Item = RateItem.Legendary,
        type_Item = (int)TypeItem.Buff,
        type_Store = (int)Type_Store.ItemWeekly,
        numeral_Item =
                   new NumeralStruct
                   {
                       ATK_Numeral = 0,
                       DEF_Numeral = 0,
                       HP_Numeral = 0,
                       SPD_Numeral = 0
                   }
    };
    ItemStruct itemReal2 = new ItemStruct
    {
        concurrency = new Concurrency
        {
            Coin = 100,
            Diamond = 0
        },
        description_Item = "This buff helps player to recover the small amount of HP.",
        image_Item = "Item_Image/Heal",
        name_Item = "Heal",
        rate_Item = RateItem.Common,
        type_Item = (int)TypeItem.Buff,
        type_Store = (int)Type_Store.ItemDaily,
        numeral_Item =
                   new NumeralStruct
                   {
                       ATK_Numeral = 0,
                       DEF_Numeral = 0,
                       HP_Numeral = 10,
                       SPD_Numeral = 0
                   }
    };
    ItemStruct itemReal3 = new ItemStruct
    {
        concurrency = new Concurrency
        {
            Coin = 150,
            Diamond = 0
        },
        description_Item = "Shield helps players to increase the DEF points.",
        image_Item = "Item_Image/Shield",
        name_Item = "Shield",
        rate_Item = RateItem.Common,
        type_Item = (int)TypeItem.Buff,
        type_Store = (int)Type_Store.ItemDaily,
        numeral_Item =
                   new NumeralStruct
                   {
                       ATK_Numeral = 0,
                       DEF_Numeral = 10,
                       HP_Numeral = 0,
                       SPD_Numeral = 0
                   }
    };
    ItemStruct itemReal4 = new ItemStruct
    {
        concurrency = new Concurrency
        {
            Coin = 200,
            Diamond = 0
        },
        description_Item = "Speed helps players to speed up the movement in 5 seconds.",
        image_Item = "Item_Image/Speed",
        name_Item = "Speed",
        rate_Item = RateItem.Common,
        type_Item = (int)TypeItem.Buff,
        type_Store = (int)Type_Store.ItemDaily,
        numeral_Item =
               new NumeralStruct
               {
                   ATK_Numeral = 0,
                   DEF_Numeral = 10,
                   HP_Numeral = 0,
                   SPD_Numeral = 100
               }
    };
    ItemStruct itemReal5 = new ItemStruct
    {
        concurrency = new Concurrency
        {
            Coin = 200,
            Diamond = 0
        },
        description_Item = "This buff helps players to speed up the Attack Speed in 10 seconds.",
        image_Item = "Item_Image/AttackSpeed",
        name_Item = "Speed",
        rate_Item = RateItem.Common,
        type_Item = (int)TypeItem.Buff,
        type_Store = (int)Type_Store.ItemDaily,
        numeral_Item =
           new NumeralStruct
           {
               ATK_Numeral = 0,
               DEF_Numeral = 0,
               HP_Numeral = 0,
               SPD_Numeral = 0
           }
    };

    ItemStruct itemReal6 = new ItemStruct
    {
        concurrency = new Concurrency
        {
            Coin = 200,
            Diamond = 50
        },
        description_Item = "Common Chest is name of Item",
        image_Item = "Item_Image/KitCommon",
        name_Item = "Common Chest",
        rate_Item = 2,
        type_Store = (int)Type_Store.Chest,
        numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 0
                    }
    };

    ItemStruct itemReal7 = new ItemStruct
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
        type_Store = (int)Type_Store.Chest,
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
        doc = db.Collection("Item").Document();
        doc.SetAsync(itemReal1);
        doc = db.Collection("Item").Document();
        doc.SetAsync(itemReal2);
        doc = db.Collection("Item").Document();
        doc.SetAsync(itemReal3);
        doc = db.Collection("Item").Document();
        doc.SetAsync(itemReal4);
        doc = db.Collection("Item").Document();
        doc.SetAsync(itemReal5);
        doc = db.Collection("Item").Document();
        doc.SetAsync(itemReal6);
        doc = db.Collection("Item").Document();
        doc.SetAsync(itemReal7);
    }
}
