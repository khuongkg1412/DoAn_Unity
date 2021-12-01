using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

[FirestoreData]

public class outfitItemStruct : MonoBehaviour
{
    [FirestoreProperty]
    public string imgUrl { get; set; }
    [FirestoreProperty]
    public string imgName { get; set; }
}
