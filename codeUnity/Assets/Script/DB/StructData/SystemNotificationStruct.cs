using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

[FirestoreData]
public class SystemNotification_Struct
{
    [FirestoreProperty]
    public string content_SystemNotification { get; set; }

    [FirestoreProperty]
    public string title_SystemNotification { get; set; }

}
