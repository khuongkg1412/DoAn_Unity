using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_Helping : MonoBehaviour
{
    //Cureent Health Point
    public float currentHP;

    //Max Health Point
    public float maxHP;

    public GameObject HealthBar;

    float timerGetSick = 0f;

    float Healthbarmaxsize;

    bool isSicked;

    // public GameObject HPText;
    private void Start()
    {
        maxHP = 30f;
        currentHP = maxHP;
        Healthbarmaxsize = HealthBar.transform.localScale.x;
        isSicked = false;
    }

    private void Update()
    {
        if (isSicked)
        {
            getSicked();
        }

        // HPHealthBar();
    }

    public void getSicked()
    {
        timerGetSick += Time.deltaTime;
        if (timerGetSick > 3f)
        {
            timerGetSick = 0f;
            currentHP -= 1f;
            HPHealthBar();
            Debug.Log("Get sicked " + currentHP);
        }
    }

    void HPHealthBar()
    {
        if (currentHP > 0)
        {
            Debug.Log("Hurt " + currentHP);
            HealthBar.transform.localScale =
                new Vector3((currentHP / maxHP) * Healthbarmaxsize,
                    HealthBar.transform.transform.localScale.y,
                    HealthBar.transform.transform.localScale.z);
        }
        else
        {
            Debug.Log("Death " + currentHP);
            currentHP = 0;
            HealthBar.transform.localScale =
                new Vector3(0,
                    HealthBar.transform.transform.localScale.y,
                    HealthBar.transform.transform.localScale.z);
            Destroy (gameObject);
            //gamePlay.GetComponent<Game_Start>().score += 100f;
            //gamePlay.GetComponent<Game_Start>().UpdateScore();
        }
    }
}
