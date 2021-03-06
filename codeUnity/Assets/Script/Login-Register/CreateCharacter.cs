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
        PlayerStruct newPlayer = new PlayerStruct()
        {
            generalInformation = new GeneralInformation_Player()
            {
                username_Player = characterName.text,
                avatar_Player = "PlayerAvatar/Avatar item.png",
                gender_Player = male
            },
            concurrency = new Concurrency()
            {
                Coin = 50,
                Diamond = 50
            },
            numeral = new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 50,
                SPD_Numeral = 300,
                ATKSPD_Numeral = 1
            },
            level = new Level()
            {
                currentXP = 0,
                reachXP = 130,
                level = 0,
                stage = 0,
                life = 6
            },
            statistic = new Dictionary<string, float>
            {
                {"VirusA_Killed",0},
                {"VirusB_Killed",0},
                {"VirusC_Killed",0},
                {"VirusD_Killed",0},
                {"Citizen_Saved",0}
            },
            currentOutfit = new Outfit()
            {
                currentShirt = "SEWnEHmTZSTMWKvAPF8l",
                currentPant = "",
                currentAccesory = "",
                currentShoes = ""
            }
        };

        docRef.SetAsync(newPlayer).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to the LA document in the cities collection.");
            StartCoroutine(addOtherCollection(IDPlayer));
            SceneManager.LoadScene("MainPage");
        });

        yield return null;
    }


    IEnumerator addOtherCollection(string ID)
    {
        Inventory_Player inventory_Player = new Inventory_Player()
        {
            item = new Dictionary<string, float>
            {
                {"Shirt 2", 1 }
            }
        };

        Friend_Player friend_Player = new Friend_Player()
        {
            accept_Friend = true,
            friendID = "7xv28G3fCIf2UoO0rV2SFV5tTr62"
        };

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("SEWnEHmTZSTMWKvAPF8l");
        doc.SetAsync(inventory_Player);

        doc = db.Collection("Player").Document(ID).Collection("Friend_Player").Document(friend_Player.friendID);
        doc.SetAsync(friend_Player);
        yield return null;
    }
}
