using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UI;

public class CloudFirestore : MonoBehaviour
{
    Dictionary<string, object> playerr;

    public Text playerName;

    FirebaseFirestore db;

    [SerializeField]
    private string addressPath = "player/ID";

    //FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    //public GameObject playerName;
    // Start is called before the first frame upd ate
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void readData()
    {
        //FirebaseFirestore db = FirebaseFirestore.DefaultInstance; //hình như ko đọc dc cái này
        db = FirebaseFirestore.DefaultInstance;
        db
            .Document(addressPath)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread((task) =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    playerr = snapshot.ToDictionary();
                    foreach (KeyValuePair<string, object> pair in playerr)
                    {
                        //Debug.Log(("{0}: {1}", pair.Key, pair.Value));
                        if (pair.Key == "playerName")
                        {
                            Debug.Log("Name: " + pair.Value);
                            playerName.text = (string) pair.Value;
                        }
                    }
                }
                else
                {
                    Debug.Log(String.Format("snapshot không tồn tại!"));
                }
            });
    }
}
