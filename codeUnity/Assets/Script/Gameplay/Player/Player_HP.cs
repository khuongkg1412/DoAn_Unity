using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
    public Canvas canvas;

    //Player dead
    public bool isDead = false;

    //Max Health Point
    private float maxHP;

    public Slider HealthBar;

    public Text HPText;

    private void Start()
    {
        //Set Max HP for player characters
        maxHP = Player_DataManager.Instance.Player.numeral.HP_Numeral;
        //Set max HP to slider
        HealthBar.maxValue = maxHP;
    }

    private void Update()
    {
        //Update Text and health bar by the current Health from the object Player
        HealthBar.value = canvas.GetComponent<Game_Start>().Player.returnHP();
        HPText.text = (HealthBar.value + " / " + HealthBar.maxValue);
        //Cheeck the player is dead
        if (canvas.GetComponent<Game_Start>().Player.isPlayerDead())
        {
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
            canvas.GetComponent<Game_Start>().Player.getDamage(other.gameObject.GetComponent<Enemy>().virus.returnATK());
            getDamage();
        }
    }
}
