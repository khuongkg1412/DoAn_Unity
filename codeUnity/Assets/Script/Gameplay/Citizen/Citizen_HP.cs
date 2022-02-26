using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Citizen_HP : MonoBehaviour
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

    //Time to manipulate when people get sick and get heal
    float timerGetSick = 0f;

    public float timerGetHeal = 7f;
    //Count time
    float countTimeHealing = 0;

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
        //Set HP for Citizen
        maxHP = 30f;
        currentHP = maxHP;

        //Set Max size for HP bar
        Healthbarmaxsize = HealthBar.transform.localScale.x;

        //When game start people are not sicked
        isSicked = false;
        isHeal = false;
        //Set Timehealing bar
        TimeHealingBar.GetComponent<Slider>().maxValue = 7f;
        TimeHealingBar.GetComponent<Slider>().value = 0;
        // TimeHealingBar.SetActive(false);
    }

    private void Update()
    {
        //If people are get sicked, we decrease HP of them
        if (isDoneHealing)
        {
            healingComplete();
        }
        else if (isHeal)
        {
            //Increase Hp of them
            getHeal();
        }
        else if (isSicked)
        {
            //Decrease HP of them
            getSicked();
        }

        //Display HP to healthbar
        HPHealthBar();
    }
    public void resetHealing()
    {
        //Reset time and decrese Hp
        isHeal = false;
        countTimeHealing = 0;
        timerGetHeal = 7f;
        hpText.text = "Healing in " + 0 + "s";
        TimeHealingBar.GetComponent<Slider>().value = 0;
        TimeHealingBar.SetActive(false);
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

        countTimeHealing += Time.deltaTime;
        //Decrease HP
        if (timerGetHeal == 0f)
        {
            currentHP = maxHP;
            timerGetHeal = 0f;
            isDoneHealing = true;
        }
        else if (countTimeHealing > 1f)
        {
            //Reset time and decrese Hp
            countTimeHealing = 0;
            if (currentHP < maxHP)
            {
                currentHP += (maxHP - currentHP) / timerGetHeal;
            }
            else
            {
                currentHP = maxHP;
            }
            timerGetHeal -= 1f;
            //Set text for timehealing bar
            TimeHealingBar.GetComponent<Slider>().value = (7 - timerGetHeal);
            hpText.text = "Healing in " + (7 - timerGetHeal) + "s";
        }
    }
    void healingComplete()
    {
        updateScoretoCanvas();
        //Disable Help button and TimeHealingbar
        Button button = GameObject.Find("HelpButton").GetComponent<Button>();
        button.interactable = false;
        TimeHealingBar.SetActive(false);

        //Detroy Object
        Destroy(gameObject);
    }
    void updateScoretoCanvas()
    {
        if (GameObject.Find("Canvas").GetComponent<Game_Start>() != null)

        {
            GameObject.FindWithTag("Player").GetComponent<Player_Controller>().Character.score += 100;
            GameObject.Find("Canvas").GetComponent<Game_Start>().UpdateCitizen(1);
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<Game_Tutorial>().UpdateScore(100f);
            GameObject.Find("Canvas").GetComponent<Game_Tutorial>().UpdateCitizen(1);
        }
    }

    void updateConditionVictory()
    {
        if (GameObject.Find("Canvas").GetComponent<Game_Start>() != null)
        {
            GameObject.Find("Canvas").GetComponent<Game_Start>().isVictory = false;
            GameObject.Find("Canvas").GetComponent<Game_Start>().isGameOver = true;
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<Game_Tutorial>().isGameOver = true;
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
            HealthBar.transform.localScale = new Vector3((currentHP / maxHP) * Healthbarmaxsize, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
        }
        else
        {
            // People's dead
            currentHP = 0;
            HealthBar.transform.localScale = new Vector3(0, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);

            updateConditionVictory();
            //Detroy Object
            Destroy(gameObject);
        }
    }
}
