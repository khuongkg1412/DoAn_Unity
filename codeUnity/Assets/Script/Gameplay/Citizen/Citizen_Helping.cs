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

    public bool isSick = false;

    // public GameObject HPText;
    private void Start()
    {
        maxHP = 30f;
        currentHP = maxHP;
    }

    public void getSicked(){
        currentHP -= 1f;
    }
}
