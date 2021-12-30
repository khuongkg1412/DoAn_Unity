using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HP : MonoBehaviour
{
    public Canvas gamePlay;
    //Cureent Health Point
    public float currentHP;

    //Max Health Point
    public float maxHP;

    public GameObject HealthBar;

    float maxHPsize;
    // public GameObject HPText;
    private void Start()
    {
        maxHP = 30f;
        currentHP = maxHP;
        maxHPsize = HealthBar.transform.localScale.x;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            currentHP -= other.gameObject.GetComponent<Bullet>().dameGiven;
            if (currentHP > 0)
            {

                HealthBar.transform.localScale = new Vector3((currentHP / maxHP) * maxHPsize, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
            }
            else
            {
                currentHP = 0;
                HealthBar.transform.localScale =
                    new Vector3(0,
                        HealthBar.transform.transform.localScale.y,
                        HealthBar.transform.transform.localScale.z);
                Destroy(gameObject);
                if (gamePlay.GetComponent<Game_Start>() != null)
                {
                    gamePlay.GetComponent<Game_Start>().UpdateScore(10f);
                    gamePlay.GetComponent<Game_Start>().UpdateEnemyNumber(1);
                }
                else
                {
                    gamePlay.GetComponent<Game_Tutorial>().UpdateScore(10f);
                    gamePlay.GetComponent<Game_Tutorial>().UpdateEnemyNumber(1);
                }

            }
        }
    }
}
