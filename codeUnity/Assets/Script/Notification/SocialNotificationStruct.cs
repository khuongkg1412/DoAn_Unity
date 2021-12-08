using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
[FirestoreData]
public class SocialNotificationStruct
{
    [FirestoreProperty] public string notificationImage { get; set; }
    [FirestoreProperty] public string notificationIcon { get; set; }
    [FirestoreProperty] public string notificationContent { get; set; }
    [FirestoreProperty] public string notificationSenderId { get; set; }
    [FirestoreProperty] public bool notificationStatus { get; set; }
}
