using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
[System.Serializable]
[FirestoreData]
public class NumeralStruct
{
    [FirestoreProperty]
    public float ATK_Numeral { get; set; }

    [FirestoreProperty]
    public float DEF_Numeral { get; set; }

    [FirestoreProperty]
    public float HP_Numeral { get; set; }

    [FirestoreProperty]
    public float SPD_Numeral { get; set; }
    [FirestoreProperty]
    public float ATKSPD_Numeral { get; set; }
}

[FirestoreData]
[System.Serializable]
public class ItemStruct
{
    public string ID { get; set; }
    public Texture2D texture2D { get; set; }
    [FirestoreProperty]
    public Concurrency concurrency { get; set; }
    [FirestoreProperty]
    public string description_Item { get; set; }

    [FirestoreProperty]
    public string image_Item { get; set; }

    [FirestoreProperty]
    public string name_Item { get; set; }

    [FirestoreProperty]
    public double rate_Item { get; set; }

    [FirestoreProperty]
    public int type_Item { get; set; }
    [FirestoreProperty]
    public NumeralStruct numeral_Item { get; set; }
}

public enum TypeItem
{
    Shirt = 0,
    Pants = 1,
    Shoes = 2,
    Accessory = 3,
    ItemDaily = 4,
    ItemWeekly = 5,
    Chest = 6,
    Buff = 7,
    Gun = 8
}

public struct RateItem
{
    public const double Common = 75.5;
    public const double Rare = 31.5;
    public const double Epic = 15.5;
    public const double Legendary = 4.5;
}