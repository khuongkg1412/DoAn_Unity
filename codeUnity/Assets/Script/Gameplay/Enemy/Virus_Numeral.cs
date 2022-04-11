using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus_Numeral : MonoBehaviour
{
    private Enemy virus;
    private NumeralStruct virusNumeral;

    public void settingNumeral(Enemy enemy)
    {
        virus = enemy;
        virusNumeral = enemy.getNumeral();
    }
    public float returnATK_Numeral()
    {
        return this.virusNumeral.ATK_Numeral;
    }

    public float returnDifficultLevel()
    {
        return this.virus.getDifficultLevel();
    }
}
