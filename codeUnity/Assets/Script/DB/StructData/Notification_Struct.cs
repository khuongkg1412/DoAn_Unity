using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System;

[FirestoreData]
public class Notification_Struct
{
    public string ID { get; set; }
    [FirestoreProperty]
    public string content_Notification { get; set; }
    [FirestoreProperty]
    public string title_Notification { get; set; }
    [FirestoreProperty]
    public string sentID_Notification { get; set; }
    [FirestoreProperty]
    public string receivedID_Notification { get; set; }
    [FirestoreProperty]
    public bool isRead_Notification { get; set; }
    [FirestoreProperty]
    public int type_Notification { get; set; }
    [FirestoreProperty]
    public DateTime dateCreate { get; set; }

}
public enum Notification
{
    SystemUpdate_Notification = 0,
    ReachAchive_Notification = 1,
    SystemGift_Notification = 2,
    Friend_Notification = 3,
    Social_Notifacation = 4

}