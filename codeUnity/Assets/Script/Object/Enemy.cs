using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy
{
    public Enemy()
    {
        numeral = new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 30,
            SPD_Numeral = 200,
            ATKSPD_Numeral = 1
        };
        typeVirus = VirusType.VirusA;
        image = loadingImageFromFilePath("Virus/A");
        detectRange = 300f;
    }
    public VirusType typeVirus;
    public NumeralStruct numeral;
    public Sprite image;
    public float detectRange;
    public void settingNumeral(NumeralStruct numeral)
    {
        this.numeral = numeral;
    }
    public Sprite loadingImageFromFilePath(string Filepath)
    {
        if (Resources.Load<Sprite>(Filepath) != null)
        {
            return Resources.Load<Sprite>(Filepath);
        }
        return null;
    }

    public void getDamage(int damageTaken)
    {
        this.numeral.HP_Numeral -= damageTaken;

    }
}


public enum VirusType
{
    VirusA,
    VirusB,
    VirusC,
    VirusD,
    Boss1,
    Boss2
}
public class VirusA : Enemy
{
    public VirusA()
    {
        numeral = new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 30,
            SPD_Numeral = 200,
            ATKSPD_Numeral = 1
        };
        typeVirus = VirusType.VirusA;
        image = loadingImageFromFilePath("Virus/A");
        detectRange = 300f;
    }

}
public class VirusB : Enemy
{
    public VirusB()
    {
        numeral = new NumeralStruct()
        {
            ATK_Numeral = 5,
            DEF_Numeral = 5,
            HP_Numeral = 50,
            SPD_Numeral = 200,
            ATKSPD_Numeral = 1
        };
        typeVirus = VirusType.VirusB;
        image = loadingImageFromFilePath("Virus/B");
        detectRange = 300f;
    }

}

public class VirusC : Enemy
{
    public VirusC()
    {
        numeral = new NumeralStruct()
        {
            ATK_Numeral = 5,
            DEF_Numeral = 0,
            HP_Numeral = 20,
            SPD_Numeral = 500,
            ATKSPD_Numeral = 1
        };
        typeVirus = VirusType.VirusC;
        image = loadingImageFromFilePath("Virus/C");
        detectRange = 300f;
    }
}
public class VirusD : Enemy
{
    public VirusD()
    {
        numeral = new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 30,
            SPD_Numeral = 200,
            ATKSPD_Numeral = 1
        };
        typeVirus = VirusType.VirusD;
        image = loadingImageFromFilePath("Virus/VirusD");
        detectRange = 300f;
    }
}

public class VirusBoss : Enemy
{
    public VirusBoss()
    {
        numeral = new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 100,
            SPD_Numeral = 100,
            ATKSPD_Numeral = 1
        };
        typeVirus = VirusType.Boss1;
        image = loadingImageFromFilePath("Virus/Boss1");
        detectRange = 10000f;
    }
}

public class VirusBoss2 : Enemy
{
    public VirusBoss2()
    {
        numeral = new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 100,
            SPD_Numeral = 300,
            ATKSPD_Numeral = 1
        };
        typeVirus = VirusType.Boss2;
        image = loadingImageFromFilePath("Virus/Boss1");
        detectRange = 10000f;
    }
}