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
        male = 0;
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
                avatar_Player = "PlayerAvatar/" + IDPlayer,
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
                {"Virus_Kill",0},
                {"Citizen_Saved",0}
            },
            currentOutfit = new Outfit()
            {
                currentShirt = "wo7LWsLLKNB3riZdVaAg",
                currentPant = "kAaxU7wzg9t2N1KZomPj",
                currentAccesory = "bj1vVjGVvMiYROHKfrE4",
                currentShoes = "DiK9GKuMVltYg5lc9neE"
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
        Inventory_Player Shirt = new Inventory_Player()
        {
            item = new Dictionary<string, float>
            {
                {"basic_shirt", 1 }
            }
        };
        Inventory_Player Pant = new Inventory_Player()
        {
            item = new Dictionary<string, float>
            {
                {"basic_pant", 1 }
            }
        };
        Inventory_Player Accessory = new Inventory_Player()
        {
            item = new Dictionary<string, float>
            {
                {"basic_accessory", 1 }
            }
        };
        Inventory_Player Shoes = new Inventory_Player()
        {
            item = new Dictionary<string, float>
            {
                {"basic_shoes", 1 }
            }
        };

        Friend_Player friend_Player = new Friend_Player()
        {
            accept_Friend = true,
            friendID = "7xv28G3fCIf2UoO0rV2SFV5tTr62"
        };

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("wo7LWsLLKNB3riZdVaAg");
        doc.SetAsync(Shirt);
        doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("kAaxU7wzg9t2N1KZomPj");
        doc.SetAsync(Pant);
        doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("bj1vVjGVvMiYROHKfrE4");
        doc.SetAsync(Accessory);
        doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("DiK9GKuMVltYg5lc9neE");
        doc.SetAsync(Shoes);

        doc = db.Collection("Player").Document(ID).Collection("Friend_Player").Document(friend_Player.friendID);
        doc.SetAsync(friend_Player);
        yield return null;
    }
}
