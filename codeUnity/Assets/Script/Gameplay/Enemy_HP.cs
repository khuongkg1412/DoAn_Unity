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

    // public GameObject HPText;
    private void Start()
    {
        maxHP = 30f;
        currentHP = maxHP;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            currentHP -= other.gameObject.GetComponent<Bullet>().dameGiven;
            if (currentHP > 0)
            {
                
                HealthBar.transform.localScale =
                    new Vector3((currentHP/ maxHP)* HealthBar.transform.transform.localScale.x,
                        HealthBar.transform.transform.localScale.y,
                        HealthBar.transform.transform.localScale.z);
            }
            else
            {
                currentHP = 0;
                HealthBar.transform.localScale =
                    new Vector3(0,
                        HealthBar.transform.transform.localScale.y,
                        HealthBar.transform.transform.localScale.z);
                Destroy (gameObject);
                //gamePlay.GetComponent<Game_Start>().score += 100f;
                gamePlay.GetComponent<Game_Start>().UpdateScore();
            }
        }
    }
}
