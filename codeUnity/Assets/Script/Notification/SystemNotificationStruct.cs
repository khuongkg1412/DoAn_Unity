using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public class SystemNotificationStruct 
{
    [FirestoreProperty] public string notificationIcon { get; set; }
    [FirestoreProperty] public string notificationContent { get; set; }
    [FirestoreProperty] public bool notificationStatus { get; set; }
}
