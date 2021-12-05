using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class SystemNotificationStruct : MonoBehaviour
{
    [FirestoreProperty] public string notificationIcon { get; set; }
    [FirestoreProperty] public string notificationContent { get; set; }
    [FirestoreProperty] public bool notificationStatus { get; set; }
}
