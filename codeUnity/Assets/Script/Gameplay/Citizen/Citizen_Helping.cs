using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Citizen_Helping : MonoBehaviour
{
    //Cureent Health Point
    public float currentHP;

    //Max Health Point
    public float maxHP;

    //Helth bar object
    public GameObject HealthBar;

    //Helth bar object
    public GameObject TimeHealingBar;

    public TextMeshProUGUI hpText;

    //Time to manipulate when people get sick
    float timerGetSick = 0f;

    public float timerGetHeal = 7f;

    //Max size of health bar
    float Healthbarmaxsize;

    //Detemine people are sick or not
    public bool isSicked;

    public bool isHeal;

    public bool isDoneHealing = false;

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
        isSicked = true;
        isHeal = false;

        //Set Timehealing bar
        TimeHealingBar.GetComponent<Slider>().maxValue = 7f;
        TimeHealingBar.GetComponent<Slider>().value = 0;
        // TimeHealingBar.SetActive(false);
    }

    private void Update()
    {
        //If people are get sicked, we decrease HP of them
        if (isSicked)
        {
            // TimeHealingBar.SetActive(false);
            //Decrease HP of them
            getSicked();
        }
        else if (isHeal && !isDoneHealing)
        {
            // TimeHealingBar.SetActive(true);
            getHeal();
        }

        //Display HP to healthbar
        HPHealthBar();
    }

    //Red color Hex value: 245 0 39
    //Green color Hex value: 32 255 0
    /* 
        Every 3s, drop People HP by 2
    */
    public void getSicked()
    {
        //Count time
        timerGetSick += Time.deltaTime;
        isHeal = false;
        timerGetHeal = 7f;

        //Decrease HP
        if (timerGetSick > 3f)
        {
            //Reset time and decrese Hp
            timerGetSick = 0f;
            currentHP -= 2f;

            //Flashing Red and Green Color
            Invoke("healthHurtState", 0f);
            Invoke("healthNormalState", 0.5f);
        }
    }

    /* 
        Every 1s, up People's HP by 1
    */
    public void getHeal()
    {
        //Count time
        timerGetSick += Time.deltaTime;

        //Decrease HP
        if (timerGetSick > 1f && currentHP < maxHP)
        {
            //Reset time and decrese Hp
            timerGetSick = 0;
            currentHP += (maxHP - currentHP) / timerGetHeal;
            timerGetHeal -= 1f;

            TimeHealingBar.GetComponent<Slider>().value = (7 - timerGetHeal);
            hpText.text = "Healing in " + (7 - timerGetHeal) + "s";
        }
        else if (timerGetHeal > 7f || currentHP >= maxHP)
        {
            //Reset time and decrese Hp
            currentHP = maxHP;
            timerGetHeal = 0f;
            isDoneHealing = true;
            isSicked = false;
            GameObject
                .Find("Canvas")
                .GetComponent<Game_Start>()
                .UpdateScore(100f);
            timerGetHeal = 0f;
            Button button =
                GameObject.Find("HelpButton").GetComponent<Button>();
            button.interactable = false;
            TimeHealingBar.SetActive(false);

            //Detroy Object
            Destroy (gameObject);
        }
    }

    void healthHurtState()
    {
        HealthBar.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    }

    void healthNormalState()
    {
        HealthBar.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
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
