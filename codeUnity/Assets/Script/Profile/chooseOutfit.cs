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
        DisplayOutfitData(ModifyPlayerInfor.typeOfOutfit);
        //AddData();
    }

    void DisplayOutfitData(int typeOfItem)
    {
        bool notHavethisItem;
        switch (typeOfItem)
        {
            case (int)TypeItem.Suit:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item == 0)
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
            case (int)TypeItem.Accessory:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item == 1)
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
            case (int)TypeItem.Gun:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item == 2)
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
        Texture2D OutfitImage = item.texture2D;
        Sprite sprite = Sprite.Create(OutfitImage, new Rect(0.0f, 0.0f, OutfitImage.width, OutfitImage.height), new Vector2(0.5f, 0.5f), 100.0f);
        GameObject scrollItemObj = (GameObject)Instantiate(prefab, content.transform);

        if (notHavethisItem)
        {
            scrollItemObj.transform.Find("choosed frame").gameObject.GetComponent<Image>().color = Color.black;
            scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
            scrollItemObj.transform.Find("choosed frame").gameObject.GetComponentInParent<Button>().enabled = false;
        }
        else
        {
            scrollItemObj.transform.Find("choosed frame").gameObject.GetComponent<Image>().color = Color.yellow;
            scrollItemObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
            scrollItemObj.transform.Find("choosed frame").gameObject.GetComponentInParent<Button>().onClick.AddListener(() => chooseThisItem());
        }
    }

    private void chooseThisItem()
    {
        Debug.Log("OK");
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
        // doc.SetAsync(item1);
        // doc = db.Collection("Item").Document();
        doc.SetAsync(item2);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item3);
        doc = db.Collection("Item").Document();
        // doc.SetAsync(item7);
        // doc = db.Collection("Item").Document();
        doc.SetAsync(item8);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item9);
        doc = db.Collection("Item").Document();
        // doc.SetAsync(item10);
        // doc = db.Collection("Item").Document();
        // doc.SetAsync(item11);
        // doc = db.Collection("Item").Document();
        // doc.SetAsync(item12);
    }

    ItemStruct
        item1 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 0,
                    Diamond = 0
                },
                description_Item = "Basic Suit for new player",
                image_Item = "Outfit_Image/Suit/basic_suit",
                name_Item = "basic_suit",
                rate_Item = 0,
                type_Item = (int)TypeItem.Suit,
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
        item2 = new ItemStruct
        {
            concurrency = new Concurrency
            {
                Coin = 200,
                Diamond = 0
            },
            description_Item = "Energy Suit is name of Item",
            image_Item = "Outfit_Image/Suit/red_suit",
            name_Item = "red_suit",
            rate_Item = 2,
            type_Item = (int)TypeItem.Piece,
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
                description_Item = "Suit Killers is name of Item",
                image_Item = "Outfit_Image/Suit/gray_suit",
                name_Item = "gray_suit",
                rate_Item = 2,
                type_Item = (int)TypeItem.Piece,
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 30,
                        SPD_Numeral = 0
                    }
            };

    // Accessory
    ItemStruct
        item7 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 0,
                    Diamond = 50
                },
                description_Item = "Basic accessory for new player",
                image_Item = "Outfit_Image/Accessory/basic_accessory",
                name_Item = "basic_accessory",
                rate_Item = 0,
                type_Item = (int)TypeItem.Accessory,
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
                image_Item = "Outfit_Image/Accessory/red_accessory",
                name_Item = "red_accessory",
                rate_Item = 2,
                type_Item = (int)TypeItem.Piece,
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 25,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 0
                    }
            };
    ItemStruct
            item9 =
                new ItemStruct
                {
                    concurrency = new Concurrency
                    {
                        Coin = 0,
                        Diamond = 100
                    },
                    description_Item = "Rare Shirt is name of Item",
                    image_Item = "Outfit_Image/Accessory/gray_accessory",
                    name_Item = "gray_accessory",
                    rate_Item = 2,
                    type_Item = (int)TypeItem.Piece,
                    numeral_Item =
                        new NumeralStruct
                        {
                            ATK_Numeral = 40,
                            DEF_Numeral = 0,
                            HP_Numeral = 0,
                            SPD_Numeral = 0
                        }
                };

    // Gun
    /*ItemStruct
        item10 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 0,
                    Diamond = 0
                },
                description_Item = "Basic Gun for new player",
                image_Item = "Outfit_Image/Gun/basic_gun",
                name_Item = "Basic Gun",
                rate_Item = 0,
                type_Item = (int)TypeItem.Gun,
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
        item11 =
            new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 0,
                    Diamond = 100
                },
                description_Item = "Rare Shirt is name of Item",
                image_Item = "Outfit_Image/Gun/red_gun",
                name_Item = "Red Gun",
                rate_Item = 2,
                type_Item = (int)TypeItem.Gun,
                numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 30
                    }
            };
    ItemStruct
            item12 =
                new ItemStruct
                {
                    concurrency = new Concurrency
                    {
                        Coin = 0,
                        Diamond = 100
                    },
                    description_Item = "Rare Shirt is name of Item",
                    image_Item = "Outfit_Image/Gun/gray_gun",
                    name_Item = "Gray Gun",
                    rate_Item = 2,
                    type_Item = (int)TypeItem.Gun,
                    numeral_Item =
                    new NumeralStruct
                    {
                        ATK_Numeral = 0,
                        DEF_Numeral = 0,
                        HP_Numeral = 0,
                        SPD_Numeral = 25
                    }
                };*/
}
