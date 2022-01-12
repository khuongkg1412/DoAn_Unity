using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeName : MonoBehaviour
{
    public Text newName, currentName;
    FirebaseFirestore db;

    public void acceptChange()
    {
        if (newName.text.Length == 0)
        {
            Debug.Log("chưa có gì hết");
        }
        else
        {
            string str = newName.text;
            currentName.text = str;

            db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("Player").Document(Player_DataManager.Instance.Player.ID);
            PlayerStruct player = Player_DataManager.Instance.Player;
            player.generalInformation.username_Player = str;

            docRef.SetAsync(player);

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
