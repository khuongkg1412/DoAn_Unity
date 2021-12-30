using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class InventoryStruct
{
    [FirestoreProperty]
    public int quantity { get; set; }
}
