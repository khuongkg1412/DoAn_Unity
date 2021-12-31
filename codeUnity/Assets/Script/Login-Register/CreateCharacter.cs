using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    public InputField characterName;

    int male = 1;

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
        PlayerStruct newPlayer = new PlayerStruct()
        {
            avatar_Player = "",
            coin_Player = 0,
            diamond_Player = 0,
            energy_Player = 5,
            gender_Player = 0,
            level_Player = 0,
            name_Player = "User02",
            stage_Player = 0,
            xp_Player = 0,
            numeral_Player =
                     new NumeralStruct
                     {
                         ATK_Numeral = 2,
                         DEF_Numeral = 2,
                         HP_Numeral = 2,
                         SPD_Numeral = 2
                     }
        };

        docRef.SetAsync(newPlayer).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the LA document in the cities collection.");
            SceneManager.LoadScene("MainPage");
        });

        yield return null;
    }
}
