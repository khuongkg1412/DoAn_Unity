using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_HP : MonoBehaviour
{
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

    private void Start()
    {
        maxHP = 50f;
        currentHP = maxHP;
    }

    private void reloadScence()
    {
        if (Reloading)
        {
            waitToLoad += Time.deltaTime;
            if (waitToLoad >= 2f)
            {
                maxHP = 50f;
                currentHP = maxHP;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void Update()
    {
        reloadScence();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentHP -= other.gameObject.GetComponent<Enemy>().dameGiven;
            Debug.Log("Current HP: " + currentHP);
            if (currentHP == 0)
            {
                Debug.Log("Death");
                Reloading = true;
                isDead = true;

                //Make object comes invisible
                Renderer render = gameObject.GetComponentInChildren<Renderer>();
                render.enabled = false;
                other.gameObject.GetComponent<Enemy>().comeBackPos();
            }
        }
    }
}
