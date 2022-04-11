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
                ATKSPD_Numeral = 1.5f
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
                currentSuit = "UPkIg3ScX4kpf7gYMpGg",
                currentAccesory = "r6mtpflH43MJSo42N7JR",
                currentGun = "1W76WPc2tzUbr1dRLxEM"
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
        Inventory_Player Suit = new Inventory_Player()
        {
            quantiy = 1,
        };
        Inventory_Player Accessory = new Inventory_Player()
        {
            quantiy = 1,
        };
        Inventory_Player Gun = new Inventory_Player()
        {
            quantiy = 1,
        };

        Inventory_Player Buff = new Inventory_Player()
        {
            quantiy = 1,
            level = 0
        };

        Friend_Player friend_Player = new Friend_Player()
        {
            accept_Friend = true,
            friendID = "7xv28G3fCIf2UoO0rV2SFV5tTr62"
        };

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("UPkIg3ScX4kpf7gYMpGg");
        doc.SetAsync(Suit);
        doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("r6mtpflH43MJSo42N7JR");
        doc.SetAsync(Accessory);
        doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("1W76WPc2tzUbr1dRLxEM");
        doc.SetAsync(Gun);
        doc = db.Collection("Player").Document(ID).Collection("Inventory_Player").Document("d8YgPsLzWPeeuJxsD8yv");
        doc.SetAsync(Buff);

        doc = db.Collection("Player").Document(ID).Collection("Friend_Player").Document(friend_Player.friendID);
        doc.SetAsync(friend_Player);
        yield return null;
    }
}
