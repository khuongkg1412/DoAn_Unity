using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen
{
    private NumeralStruct numeral;
    public void settingNumeral(NumeralStruct numeral)
    {
        this.numeral = numeral;
    }

    public void getDamage(int damageTaken)
    {
        this.numeral.HP_Numeral -= damageTaken;
    }

    public void getHeal(int healTaken)
    {
        this.numeral.HP_Numeral += healTaken;
    }
}
