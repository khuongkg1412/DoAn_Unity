using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;


[FirestoreData]
public class ItemDailyStruct
{       
        [FirestoreProperty]
        public string itemImage { get; set; }

        [FirestoreProperty]
        public string itemName   { get; set; }

        [FirestoreProperty]
        public string itemType   { get; set; }

        [FirestoreProperty]
        public int quantity { get; set; }
}
