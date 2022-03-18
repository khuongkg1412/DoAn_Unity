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

    void GetOutfitData(int typeOfItem)
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
            case (int)TypeItem.Gun:
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
            case (int)TypeItem.Accessory:
                foreach (ItemStruct item in Item_DataManager.Instance.Item)
                {
                    notHavethisItem = true;
                    if (item.type_Item == 3)
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













    /*
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
        doc = db.Collection("Item").Document();
        doc.SetAsync(item9);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item10);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item11);
        doc = db.Collection("Item").Document();
        doc.SetAsync(item12);
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
                    description_Item = "Basic shirt for new player",
                    image_Item = "Outfit_Image/Shirt/basic_shirt",
                    name_Item = "Basic Shirt",
                    rate_Item = 0,
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
            item2 = new ItemStruct
            {
                concurrency = new Concurrency
                {
                    Coin = 200,
                    Diamond = 0
                },
                description_Item = "Energy Shirt is name of Item",
                image_Item = "Outfit_Image/Shirt/red_shirt",
                name_Item = "Red Shirt",
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
                    image_Item = "Outfit_Image/Shirt/gray_shirt",
                    name_Item = "Gray Shirt",
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

        //Pant
        ItemStruct item4 = new ItemStruct
        {
            concurrency = new Concurrency
            {
                Coin = 0,
                Diamond = 0
            },
            description_Item = "Basic pant for new player",
            image_Item = "Outfit_Image/Pants/basic_pant",
            name_Item = "Basic pant",
            rate_Item = 0,
            type_Item = (int)TypeItem.Pants,
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
            item5 =
                new ItemStruct
                {
                    concurrency = new Concurrency
                    {
                        Coin = 250,
                        Diamond = 0
                    },
                    description_Item = "Shirt Kit is name of Item",
                    image_Item = "Outfit_Image/Pants/red_pant",
                    name_Item = "Red pant",
                    rate_Item = 2,
                    type_Item = (int)TypeItem.Pants,
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
                    description_Item = "Gray pant is name of Item",
                    image_Item = "Outfit_Image/Pants/gray_pant",
                    name_Item = "Gray pant",
                    rate_Item = 2,
                    type_Item = (int)TypeItem.Pants,
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
                    name_Item = "Common accessory",
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
                    name_Item = "Red accessory",
                    rate_Item = 2,
                    type_Item = (int)TypeItem.Accessory,
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
                        name_Item = "Gray accessory",
                        rate_Item = 2,
                        type_Item = (int)TypeItem.Accessory,
                        numeral_Item =
                            new NumeralStruct
                            {
                                ATK_Numeral = 40,
                                DEF_Numeral = 0,
                                HP_Numeral = 0,
                                SPD_Numeral = 0
                            }
                    };

        // Shoes
        ItemStruct
            item10 =
                new ItemStruct
                {
                    concurrency = new Concurrency
                    {
                        Coin = 0,
                        Diamond = 0
                    },
                    description_Item = "Basic shoes for new player",
                    image_Item = "Outfit_Image/Shoes/basic_shoes",
                    name_Item = "Basic Shoes",
                    rate_Item = 0,
                    type_Item = (int)TypeItem.Shoes,
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
                    image_Item = "Outfit_Image/Shoes/red_shoes",
                    name_Item = "Red Shoes",
                    rate_Item = 2,
                    type_Item = (int)TypeItem.Shoes,
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
                        image_Item = "Outfit_Image/Shoes/gray_shoes",
                        name_Item = "Gray Shoes",
                        rate_Item = 2,
                        type_Item = (int)TypeItem.Shoes,
                        numeral_Item =
                        new NumeralStruct
                        {
                            ATK_Numeral = 0,
                            DEF_Numeral = 0,
                            HP_Numeral = 0,
                            SPD_Numeral = 25
                        }
                    };

                    */
}
