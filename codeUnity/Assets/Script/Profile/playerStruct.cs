using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

[FirestoreData]
public class playerStruct : MonoBehaviour
{
    [FirestoreProperty]
    public string avatarUrl { get; set; }
    [FirestoreProperty]
    public int level { get; set; }
    [FirestoreProperty]
    public string playerName { get; set; }
}
