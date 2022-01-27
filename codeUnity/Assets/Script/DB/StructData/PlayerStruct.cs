using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

[FirestoreData]
public class Inventory_Player
{
    public string ID { get; set; }

    [FirestoreProperty]
    public Dictionary<string, float> item { get; set; }
}

[FirestoreData]
public class SystemNotification
{
    [FirestoreProperty]
    public bool status_Notification { get; set; }
}

[FirestoreData]
public class Friend_Player
{
    [FirestoreProperty]
    public bool accept_Friend { get; set; }
    public string friendID { get; set; }
}


[FirestoreData]
[System.Serializable]
public class PlayerStruct
{
    [FirestoreProperty]
    public GeneralInformation_Player generalInformation { get; set; }
    [FirestoreProperty]
    public Concurrency concurrency { get; set; }
    [FirestoreProperty]
    public NumeralStruct numeral { get; set; }
    [FirestoreProperty]
    public Level level { get; set; }
    [FirestoreProperty]
    public Dictionary<string, float> statistic { get; set; }
    [FirestoreProperty]
    public Outfit currentOutfit { get; set; }

    public string ID { get; set; }

    public Texture2D texture2D { get; set; }

}

[FirestoreData]
public class GeneralInformation_Player
{
    [FirestoreProperty]
    public string username_Player { get; set; }
    [FirestoreProperty]
    public string avatar_Player { get; set; }
    [FirestoreProperty]
    public int gender_Player { get; set; }
}
[FirestoreData]
public class Concurrency
{
    [FirestoreProperty]
    public float Diamond { get; set; }
    [FirestoreProperty]
    public float Coin { get; set; }
}

[FirestoreData]
public class Level
{
    [FirestoreProperty] public float currentXP { get; set; }
    [FirestoreProperty] public float reachXP { get; set; }
    [FirestoreProperty] public int level { get; set; }
    [FirestoreProperty] public int stage { get; set; }
    [FirestoreProperty] public int life { get; set; }
}

[FirestoreData]
public class Outfit
{
    [FirestoreProperty] public string currentShirt { get; set; }
    [FirestoreProperty] public string currentPant { get; set; }
    [FirestoreProperty] public string currentAccesory { get; set; }
    [FirestoreProperty] public string currentShoes { get; set; }
}
