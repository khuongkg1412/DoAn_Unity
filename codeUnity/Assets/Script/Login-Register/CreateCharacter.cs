using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    FirebaseFirestore db;
    public InputField characterName;

    int male = 1;

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }
    public void Create()
    {
        if (characterName.text.Length == 0)
        {
            Debug.Log("chua co ten");
        }
        else StartCoroutine(createPlayer());
    }

    public void isMale()
    {
        male = 1;
    }

    public void isFemale()
    {
        male = 2;
    }

    IEnumerator createPlayer()
    {
        string IDPlayer = AuthController.ID;
        if (IDPlayer == null) IDPlayer = FacebookManager.ID;
        Debug.Log(IDPlayer);
        DocumentReference docRef = db.Collection("Player").Document(IDPlayer);
        playerStruct newPlayer = new playerStruct
        {
            avatar_Player = "",
            coin_Player = 50,
            diamond_Player = 10,
            energy_Player = 10,
            gender_Player = male,
            level_Player = 0,
            name_Player = characterName.text,
            stage_Player = 0,
            xp_Player = 0
        };

        docRef.SetAsync(newPlayer).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the LA document in the cities collection.");
            SceneManager.LoadScene("MainPage");
        });

        yield return null;
    }
}
