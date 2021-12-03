using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class NotificationStruct : MonoBehaviour
{
    
    [FirestoreProperty] public string notificationAvatar { get; set; }
    [FirestoreProperty] public string notificationImage { get; set; }
    [FirestoreProperty] public string notificationContent { get; set; }
    [FirestoreProperty] public bool notificationStatus { get; set; }
    
}
