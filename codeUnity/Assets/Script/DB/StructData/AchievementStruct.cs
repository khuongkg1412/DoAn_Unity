using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

[FirestoreData]
public class AchievementStruct
{
    public string ID { get; set; }
    [FirestoreProperty]
    public string title_Achievement { get; set; }

    [FirestoreProperty]
    public APICall_Achievement APICall { get; set; }

    [FirestoreProperty]
    public Concurrency concurrency { get; set; }
}
[FirestoreData]
public class APICall_Achievement
{
    [FirestoreProperty]
    public string APIMethod { get; set; }
    [FirestoreProperty]
    public float goal { get; set; }
}
