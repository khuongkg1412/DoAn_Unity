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
        isDead = false;
    }
    private bool isDead;
    private VirusType typeVirus;

    private NumeralStruct numeral;
    public void setDead(bool isDead)
    {
        this.isDead = isDead;
    }
    public bool getDead()
    {
        return isDead;
    }
    public void setNumeral(NumeralStruct numeral)
    {
        this.numeral = numeral;
    }
    public NumeralStruct getNumeral()
    {
        return numeral;
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

    public void getDamage(float damageTaken)
    {
        //Calculate the damage taken after decrease it by DEF 50%
        float damageTakenWithDef = damageTaken - (this.numeral.DEF_Numeral / 2);
        //Calculate current HP
        this.numeral.HP_Numeral -= damageTaken;
        //If dead
        if (this.numeral.HP_Numeral <= 0)
        {
            isDead = true;
            this.numeral.HP_Numeral = 0;
        }
    }
    public void setVirusType(VirusType virusType)
    {
        this.typeVirus = virusType;
    }
    public VirusType getVirusType()
    {
        return typeVirus;
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
        setNumeral(new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 30,
            SPD_Numeral = 200,
            ATKSPD_Numeral = 1
        });
        setVirusType(VirusType.VirusA);
        image = loadingImageFromFilePath("Virus/A");
        detectRange = 300f;
    }

}
public class VirusB : Enemy
{
    public VirusB()
    {
        setNumeral(new NumeralStruct()
        {
            ATK_Numeral = 5,
            DEF_Numeral = 7,
            HP_Numeral = 30,
            SPD_Numeral = 200,
            ATKSPD_Numeral = 1
        });
        setVirusType(VirusType.VirusB);
        image = loadingImageFromFilePath("Virus/B");
        detectRange = 300f;
    }

}

public class VirusC : Enemy
{
    public VirusC()
    {
        setNumeral(new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 20,
            SPD_Numeral = 500,
            ATKSPD_Numeral = 1
        });
        setVirusType(VirusType.VirusC);
        image = loadingImageFromFilePath("Virus/C");
        detectRange = 300f;
    }
}
public class VirusD : Enemy
{
    public VirusD()
    {
        setNumeral(new NumeralStruct()
        {
            ATK_Numeral = 7,
            DEF_Numeral = 0,
            HP_Numeral = 60,
            SPD_Numeral = 200,
            ATKSPD_Numeral = 1
        });
        setVirusType(VirusType.VirusD);
        image = loadingImageFromFilePath("Virus/VirusD");
        detectRange = 300f;
    }
}

public class VirusBoss : Enemy
{
    public VirusBoss()
    {
        setNumeral(new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 5,
            HP_Numeral = 120,
            SPD_Numeral = 100,
            ATKSPD_Numeral = 1
        });
        setVirusType(VirusType.Boss1);
        image = loadingImageFromFilePath("Virus/Boss1");
        detectRange = 10000f;
    }
}

public class VirusBoss2 : Enemy
{
    int numberOfBeards;
    public bool isDead;
    public VirusBoss2()
    {
        numberOfBeards = 3;
        setNumeral(new NumeralStruct()
        {
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 30 * numberOfBeards,
            SPD_Numeral = 300,
            ATKSPD_Numeral = 1
        });
        setVirusType(VirusType.Boss2);
        image = loadingImageFromFilePath("Virus/Boss1");
        detectRange = 10000f;
        isDead = false;
    }
    public void getDamage(float damageTaken)
    {
        //Calculate the damage taken after decrease it by DEF
        float damageTakenWithDef = damageTaken - returnDEF();
        float currentHP = returnHP();
        currentHP -= damageTakenWithDef;
        setHP(currentHP);
        //Boss 2 has revive mechanism, base on the number ofbreads, revice if beards is more than 1
        if (currentHP <= 0)
        {
            setHP(0);
            numberOfBeards -= 1;
            isDead = true;
        }
    }
    public void reviveLife()
    {
        if (numberOfBeards > 1)
        {
            setNumeral(new NumeralStruct()
            {
                ATK_Numeral = 10,
                DEF_Numeral = 0,
                HP_Numeral = 30 * numberOfBeards,
                SPD_Numeral = 300,
                ATKSPD_Numeral = 1
            });
            image = loadingImageFromFilePath("Virus/R" + numberOfBeards);
            isDead = false;
        }

    }
}