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
    public float currentHP;

    //Max Health Point
    public float maxHP;

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
            ChangeScence scence = canvas.GetComponent<ChangeScence>();
            scence.reloadScence();
        }
    }

    private void getDamage()
    {
        HealthBar.maxValue = maxHP;
        HealthBar.value = currentHP;
        HPText.text = currentHP + " / " + maxHP;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentHP -= other.gameObject.GetComponent<Enemy>().dameGiven;
            getDamage();

            if (currentHP == 0)
            {
                Debug.Log("Death");
                Reloading = true;
                isDead = true;

                // //Make object comes invisible
                // Renderer render = gameObject.GetComponentInChildren<Renderer>();
                // render.enabled = false;
                // other.gameObject.GetComponent<Enemy>().comeBackPos();
            }
        }
    }
}
