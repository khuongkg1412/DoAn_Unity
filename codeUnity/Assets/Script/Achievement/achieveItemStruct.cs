using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

[FirestoreData]
public class achieveItemStruct : MonoBehaviour
{
    [FirestoreProperty]
    public string achieveTitle { get; set; }
    [FirestoreProperty]
    public int achievePrize { get; set; }
}