using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_Helping : MonoBehaviour
{
    //Cureent Health Point
    public float currentHP;

    //Max Health Point
    public float maxHP;

    //Helth bar object
    public GameObject HealthBar;
    //Time to manipulate when people get sick 
    float timerGetSick = 0f;
    //Max size of health bar
    float Healthbarmaxsize;
    //Detemine people are sick or not
    bool isSicked;

    // public GameObject HPText;
    private void Start()
    {
        /*
            Setting value to variable 
        */
        //Set HP for Player
        maxHP = 30f;
        currentHP = maxHP;
        //Set Max size for HP bar
        Healthbarmaxsize = HealthBar.transform.localScale.x;
        //When game start people are not sicked
        isSicked = false;
    }

    private void Update()
    {   
        //If people are get sicked, we decrease HP of them
        if (isSicked)
        {
            //Decrease HP of them
            getSicked();
        }
    }

    /* 
        Every 3s, drop People HP by 1
    */
    public void getSicked()
    {
        //Count time
        timerGetSick += Time.deltaTime;
        //Decrease HP
        if (timerGetSick > 3f)
        {
            //Reset time and decrese Hp
            timerGetSick = 0f;
            currentHP -= 1f;
            //Display HP to healthbar
            HPHealthBar();
        }
    }

    /*
        Display current HP on Healthbar
    */
    void HPHealthBar()
    {   
        //Scale healthbar by Current HP. if current HP > 0 means people are still alive. In contrast, we must detroy object
        if (currentHP > 0)
        {
            //Scale Healthbar
            HealthBar.transform.localScale =
                new Vector3((currentHP / maxHP) * Healthbarmaxsize,
                    HealthBar.transform.transform.localScale.y,
                    HealthBar.transform.transform.localScale.z);
        }
        else
        {
            // People's dead
            currentHP = 0;
            HealthBar.transform.localScale =
                new Vector3(0,
                    HealthBar.transform.transform.localScale.y,
                    HealthBar.transform.transform.localScale.z);
            //Detroy Object
            Destroy (gameObject);
        }
    }
}
