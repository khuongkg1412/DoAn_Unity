using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public class leaderBoardGlobalStruct
{
    [FirestoreProperty]
    public string flagImage { get; set; }

    [FirestoreProperty]
    public string playerAvatar { get; set; }

    [FirestoreProperty]
    public int playerLevel { get; set; }

    [FirestoreProperty]
    public int playerMap { get; set; }

    [FirestoreProperty]
    public string playerName { get; set; }

    [FirestoreProperty]
    public string playerRank { get; set; }
}
