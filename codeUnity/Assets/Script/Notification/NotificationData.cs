using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationData : MonoBehaviour
{
    FirebaseFirestore db;
    public NotificationStruct objectData;

    List<NotificationStruct> listData = new List<NotificationStruct>();

    bool isRun = false;

    int count = 0;

    public RawImage notificationAvatar;
    public RawImage notificationImage;

    public string notificationContent;
    public bool notificationStatus;

}
