using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    //Getting Canvas
    GameObject canvas;

    Slider HealthBar;

    Text HPText;

    //Create a new object for player
    Character Character;
    float test;
    NumeralStruct originNumeral;
    // Start is called before the first frame update
    void Start()
    {
        settingCharacter();
        //Setting object for Game object
        canvas = GameObject.Find("Canvas");
        HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        HPText = GameObject.Find("HPText").GetComponent<Text>();
        //Set max HP to slider
        HealthBar.maxValue = Character.returnHP();
    }

    // Update is called once per frame
    void Update()
    {
        updateProcessHP();
    }

    void resetNumeral()
    {
        Player_DataManager.Instance.Player.numeral = originNumeral;
        Player_DataManager.Instance.playerCharacter = new Character(originNumeral);
    }
    void settingCharacter()
    {
        //
        //Character.setATK(Player_DataManager.Instance.returnATK());
        //Character.setDEF(Player_DataManager.Instance.returnDEF());
        //Character.setHP(Player_DataManager.Instance.returnHP());
        //Character.setSPD(Player_DataManager.Instance.returnSPD());
        //Character.setATKSPD(Player_DataManager.Instance.returnATKSPD());
        //
        //Create Character for gameplay
        originNumeral = Player_DataManager.Instance.playerCharacter.returnNumeral();
        Character = new Character(Player_DataManager.Instance.Player.numeral);
    }
    void updateProcessHP()
    {
        //Update Text and health bar by the current Health from the object Player
        HealthBar.value = Character.returnHP();
        HPText.text = (HealthBar.value + " / " + HealthBar.maxValue);
        //Cheeck the player is dead
        if (Character.isPlayerDead())
        {
            Debug.Log("Dead");
            resetNumeral();
            canvas.GetComponent<Game_Start>().GameOVer();
            //If dead, set false for player game object
            gameObject.SetActive(false);
            //Split up by 2 modes : Story Mode and Tutorial
            if (canvas.GetComponent<Game_Start>() != null)
            {
                canvas.GetComponent<Game_Start>().GameOVer();
            }
            else
            {
                canvas.GetComponent<Game_Tutorial>().GameOVer();
            }
        }
    }
    private void getDamage()
    {
        /*
            Player Get Hurt then stop the player movement
        */
        GetComponent<Player_Movement>().isMoving = false;
        Invoke("allowMoving", 0.5f);
    }

    void allowMoving()
    {
        GetComponent<Player_Movement>().isMoving = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            Character.getDamage(other.gameObject.GetComponent<Enemy>().virus.returnATK());
            getDamage();
        }
    }
}
