using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus_Numeral : MonoBehaviour
{
    public NumeralStruct virusNumeral;

    public void settingNumeral(Enemy enemy)
    {
        virusNumeral = enemy.numeral;
    }
}
