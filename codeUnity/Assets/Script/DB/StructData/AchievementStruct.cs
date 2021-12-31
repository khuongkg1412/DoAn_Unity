using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;

[FirestoreData]
public class AchievementStruct
{
    [FirestoreProperty]
    public string description_Achievement { get; set; }

    [FirestoreProperty]
    public float goal_Achievement { get; set; }

    [FirestoreProperty]
    public float rewardType_Achievement { get; set; }

    [FirestoreProperty]
    public float reward_Achievement { get; set; }

    [FirestoreProperty]
    public string title_Achievement { get; set; }
}
