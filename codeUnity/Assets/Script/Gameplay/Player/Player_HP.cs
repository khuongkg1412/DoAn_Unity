using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_HP : MonoBehaviour
{
    public Canvas canvas;

    //Reload the scence cause you lost the game
    public bool Reloading = false;

    //Wait for reoading excutes
    private float waitToLoad;

    //Player dead
    public bool isDead = false;

    //Cureent Health Point
    private float currentHP;

    //Max Health Point
    private float maxHP;

    public Slider HealthBar;

    public Text HPText;

    private void Start()
    {
        maxHP = 50f;
        currentHP = maxHP;
    }

    private void Update()
    {
        if (isDead)
        {
            gameObject.SetActive(false);
            canvas.GetComponent<Game_Start>().GameOVer();
        }
    }

    private void getDamage()
    {
        HealthBar.maxValue = maxHP;
        HealthBar.value = currentHP;
        HPText.text = currentHP + " / " + maxHP;
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
            currentHP -= other.gameObject.GetComponent<Enemy>().dameGiven;
            getDamage();

            if (currentHP == 0)
            {
                Reloading = true;
                isDead = true;
            }
        }
    }
}
