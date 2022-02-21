using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyObject
{
    private VirusType typeVirus;
    private NumeralStruct numeral;
    private Sprite image;
    private float detectRange;
    public void settingNumeral(NumeralStruct numeral)
    {
        this.numeral = numeral;
    }
    Sprite loadingImageFromFilePath(string Filepath)
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
    public Sprite setImageForVirus()
    {
        return this.image;
    }
    public EnemyObject VirusA()
    {
        EnemyObject virusA = new EnemyObject()
        {
            numeral = new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 30,
                SPD_Numeral = 200,
                ATKSPD_Numeral = 1
            },
            typeVirus = VirusType.VirusA,
            image = loadingImageFromFilePath("Virus/A"),
            detectRange = 300f
        };

        return virusA;
    }

    public EnemyObject VirusB()
    {
        EnemyObject virusB = new EnemyObject()
        {
            numeral = new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 30,
                SPD_Numeral = 200,
                ATKSPD_Numeral = 1
            },
            typeVirus = VirusType.VirusB,
            image = loadingImageFromFilePath("Virus/B"),
            detectRange = 300f
        };

        return virusB;
    }

    public EnemyObject VirusC()
    {
        EnemyObject virusC = new EnemyObject()
        {
            numeral = new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 30,
                SPD_Numeral = 200,
                ATKSPD_Numeral = 1
            },
            typeVirus = VirusType.VirusC,
            image = loadingImageFromFilePath("Virus/C"),
            detectRange = 300f
        };

        return virusC;
    }

    public EnemyObject VirusD()
    {
        EnemyObject virusD = new EnemyObject()
        {
            numeral = new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 30,
                SPD_Numeral = 200,
                ATKSPD_Numeral = 1
            },
            typeVirus = VirusType.VirusD,
            image = loadingImageFromFilePath("Virus/D"),
            detectRange = 300f
        };

        return virusD;
    }

    public EnemyObject VirusBoss()
    {
        EnemyObject VirusBoss = new EnemyObject()
        {
            numeral = new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 30,
                SPD_Numeral = 200,
                ATKSPD_Numeral = 1
            },
            typeVirus = VirusType.Bosss1,
            image = loadingImageFromFilePath("Virus/Boss1"),
            detectRange = 300f
        };

        return VirusBoss;
    }

    public float returnATK()
    {
        return this.numeral.ATK_Numeral;
    }
    public void setATK(float ATK)
    {
        this.numeral.ATK_Numeral = ATK;
    }

    public float returnDEF()
    {
        return this.numeral.DEF_Numeral;
    }

    public void setDEF(float DEF)
    {
        this.numeral.DEF_Numeral = DEF;
    }
    public float returnHP()
    {
        return this.numeral.HP_Numeral;
    }

    public void setHP(float HP)
    {
        this.numeral.HP_Numeral = HP;
    }
    public float returnSPD()
    {
        return this.numeral.SPD_Numeral;
    }
    public void setSPD(float SPD)
    {
        this.numeral.SPD_Numeral = SPD;
    }
    public float returnATKSPD()
    {
        return this.numeral.ATKSPD_Numeral;
    }
    public void setATKSPD(float ATKSPD)
    {
        this.numeral.ATKSPD_Numeral = ATKSPD;
    }
    public float returnDectectRange()
    {
        return this.detectRange;
    }
    public void setDectectRange(float detectRange)
    {
        this.detectRange = detectRange;
    }
}
public enum VirusType
{
    VirusA,
    VirusB,
    VirusC,
    VirusD,
    Bosss1
}
