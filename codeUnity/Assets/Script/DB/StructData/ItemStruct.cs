using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
[System.Serializable]
[FirestoreData]
public class NumeralStruct
{
    [FirestoreProperty]
    public float ATK_Numeral { get; set; }

    [FirestoreProperty]
    public float DEF_Numeral { get; set; }

    [FirestoreProperty]
    public float HP_Numeral { get; set; }

    [FirestoreProperty]
    public float SPD_Numeral { get; set; }
}

[FirestoreData]
public class ItemStruct
{
    public string ID { get; set; }
    [FirestoreProperty]
    public Concurrency concurrency { get; set; }
    [FirestoreProperty]
    public string description_Item { get; set; }

    [FirestoreProperty]
    public string image_Item { get; set; }

    [FirestoreProperty]
    public string name_Item { get; set; }

    [FirestoreProperty]
    public float rate_Item { get; set; }

    [FirestoreProperty]
    public string type_Item { get; set; }
    [FirestoreProperty]
    public NumeralStruct numeral_Item { get; set; }
}
