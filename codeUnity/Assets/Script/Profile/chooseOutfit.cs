using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;

public class chooseOutfit : MonoBehaviour
{
    FirebaseFirestore db;
    public GameObject prefab;
    public GameObject content;


    // Start is called before the first frame update
    void Start()
    {
        GetOutfitData(ModifyPlayerInfor.typeOfOutfit);
        //AddData();
    }

    void GetOutfitData(string typeOfItem)
    {
        bool notHavethisItem;
        switch (int.Parse(typeOfItem))
        {
            case (int)TypeItem.Shirt:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item.Equals((int)TypeItem.Shirt))
                    {
                        foreach (Inventory_Player outfit in Player_DataManager.Instance.inventory_Player)
                        {
                            if (item.ID.Equals(outfit.ID))
                            {
                                notHavethisItem = false;
                                break;
                            }
                        }
                        Populate(item, notHavethisItem);
                    }
                }
                break;
            case (int)TypeItem.ItemDaily:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item.Equals("Pants"))
                    {
                        foreach (Inventory_Player outfit in Player_DataManager.Instance.inventory_Player)
                        {
                            if (item.ID.Equals(outfit.ID))
                            {
                                notHavethisItem = false;
                                break;
                            }
                        }
                        Populate(item, notHavethisItem);
                    }
                }
                break;
            case (int)TypeItem.ItemWeekly:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item.Equals("Accessory"))
                    {
                        foreach (Inventory_Player outfit in Player_DataManager.Instance.inventory_Player)
                        {
                            if (item.ID.Equals(outfit.ID))
                            {
                                notHavethisItem = false;
                                break;
                            }
                        }
                        Populate(item, notHavethisItem);
                    }
                }
                break;
            case (int)TypeItem.Chest:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item.Equals("Shoes"))
                    {
                        foreach (Inventory_Player outfit in Player_DataManager.Instance.inventory_Player)
                        {
                            if (item.ID.Equals(outfit.ID))
                            {
                                notHavethisItem = false;
                                break;
                            }
                        }
                        Populate(item, notHavethisItem);
                    }
                }
                break;
        }
    }

    void Populate(ItemStruct item, bool notHavethisItem)
    {
        Texture2D OutfitImage = loadingImageFromFilePath(item.image_Item);
        Sprite sprite = Sprite.Create(OutfitImage, new Rect(0.0f, 0.0f, OutfitImage.width, OutfitImage.height), new Vector2(0.5f, 0.5f), 100.0f);
        GameObject scrollItemObj = (GameObject)Instantiate(prefab, transform);

        if (notHavethisItem)
        {
            scrollItemObj.transform.Find("choosed frame").gameObject.GetComponent<Image>().color = Color.black;
            scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
            //scrollItemObj.transform.Find("Outfit Item").gameObject.GetComponentInParent<Button>().enabled = false;
        }
        else
        {
            scrollItemObj.transform.Find("choosed frame").gameObject.GetComponent<Image>().color = Color.cyan;
            scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
        }
    }

    Texture2D loadingImageFromFilePath(string Filepath)
    {
        if (Resources.Load<Sprite>(Filepath) != null)
        {
            return Resources.Load<Texture2D>(Filepath);
        }
        return null;
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

    ItemStruct
        item1 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 100,
                    Diamond = 0
                },
                description_Item = "Heal Shirt is name of Item",
                image_Item = "Outfit_Image/Shirt/Shirt 1",
                name_Item = "Heal Shirt",
                rate_Item = 1,
                type_Item = (int)TypeItem.Shirt,
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
            description_Item = "Energy Shirt is name of Item",
            image_Item = "Outfit_Image/Shirt/Shirt 2",
            name_Item = "Energy Shirt",
            rate_Item = 2,
            type_Item = (int)TypeItem.Shirt,
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
                description_Item = "Shirt Killers is name of Item",
                image_Item = "Outfit_Image/Shirt/Shirt 3",
                name_Item = "Shirt Killers",
                rate_Item = 2,
                type_Item = (int)TypeItem.Shirt,
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
        description_Item = "Yellow Shirt is name of Item",
        image_Item = "Outfit_Image/Shirt/Shirt 4",
        name_Item = "Yellow Shirt",
        rate_Item = 2,
        type_Item = (int)TypeItem.Shirt,
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
                description_Item = "Shirt Kit is name of Item",
                image_Item = "Outfit_Image/Shirt/Shirt 5",
                name_Item = "Shirt Kit",
                rate_Item = 2,
                type_Item = (int)TypeItem.Shirt,
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
                description_Item = "Shirt Sample is name of Item",
                image_Item = "Outfit_Image/Shirt/Shirt 6",
                name_Item = "Shirt Sample",
                rate_Item = 2,
                type_Item = (int)TypeItem.Shirt,
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
                description_Item = "Common Shirt is name of Item",
                image_Item = "Outfit_Image/Shirt/Shirt 7",
                name_Item = "Common Shirt",
                rate_Item = 2,
                type_Item = (int)TypeItem.Shirt,
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
                description_Item = "Rare Shirt is name of Item",
                image_Item = "Outfit_Image/Shirt/Shirt 8",
                name_Item = "Rare Shirt",
                rate_Item = 2,
                type_Item = (int)TypeItem.Shirt,
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 0
                    }
            };

}
