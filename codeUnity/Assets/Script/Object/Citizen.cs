using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen
{
    Citizen()
    {
        maxHP = 30f;
        currentHP = maxHP;
        isSicked = false;
        isDoneHealing = false;
        isHeal = false;
    }
    //Cureent Health Point
    private float currentHP;
    public float returnCurrentHP()
    {
        return this.currentHP;
    }

    public void setCurrentHP(float HP)
    {
        this.currentHP = HP;
    }

    //Max Health Point
    private float maxHP;
    public float returnMaxHP()
    {
        return this.maxHP;
    }

    public void setMaxHP(float HP)
    {
        this.maxHP = HP;
    }

    //Detemine people are sick or not
    private bool isSicked;
    public bool returnIsSicked()
    {
        return isSicked;
    }
    public void setIsSicked(bool isSicked)
    {
        this.isSicked = isSicked;
    }
    private bool isHeal;

    public bool returnIsHeal()
    {
        return isHeal;
    }
    public void setsIsHeal(bool isHeal)
    {
        this.isHeal = isHeal;
    }

    private bool isDoneHealing;
    public bool returnIsDoneHealing()
    {
        return isDoneHealing;
    }
    public void setsIsDoneHealing(bool isDoneHealing)
    {
        this.isDoneHealing = isDoneHealing;
    }
    private float TimerGetSicked = 3f;

    public void getSickedByTime(float Timer)
    {
        if (Timer >= TimerGetSicked)
        {
            this.currentHP -= 2f;
        }
    }
}
