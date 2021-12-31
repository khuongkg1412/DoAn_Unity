using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

[FirestoreData]
public class Inventory_Player
{
    [FirestoreProperty]
    public float quantity { get; set; }
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
    [FirestoreProperty]
    public bool notificationID { get; set; }
}

[FirestoreData]
public class Achievement_Player
{
    [FirestoreProperty]
    public bool achived_Player { get; set; }
    [FirestoreProperty]
    public float progress_Player { get; set; }
}

[FirestoreData]
public class Notification_Player
{
    [FirestoreProperty]
    public string content_Notification { get; set; }
    [FirestoreProperty]
    public string sentID_Notification { get; set; }
    [FirestoreProperty]
    public bool status_Notification { get; set; }
    [FirestoreProperty]
    public string title_Notification { get; set; }
    [FirestoreProperty]
    public float type_Notification { get; set; }
}


[FirestoreData]
[System.Serializable]
public class PlayerStruct
{
    [FirestoreProperty]
    public string avatar_Player { get; set; }
    [FirestoreProperty]
    public float coin_Player { get; set; }
    [FirestoreProperty]
    public float diamond_Player { get; set; }
    [FirestoreProperty]
    public float energy_Player { get; set; }
    [FirestoreProperty]
    public float gender_Player { get; set; }
    [FirestoreProperty]
    public float level_Player { get; set; }
    [FirestoreProperty]
    public string name_Player { get; set; }
    [FirestoreProperty]
    public float stage_Player { get; set; }
    [FirestoreProperty]
    public float xp_Player { get; set; }
    [FirestoreProperty]
    public NumeralStruct numeral_Player { get; set; }
}

