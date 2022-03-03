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
    Bosss1
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
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 30,
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
            ATK_Numeral = 10,
            DEF_Numeral = 0,
            HP_Numeral = 30,
            SPD_Numeral = 200,
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
        image = loadingImageFromFilePath("Virus/D");
        detectRange = 300f;
    }
}

public class VirusBoss : Enemy
{
    //public void moveUpDown()
    //{
    //    GameObject BossObject = GameObject.Find("Boss");
    //    Vector3 upwardMove = new Vector3(BossObject.transform.position.x, -385, BossObject.transform.position.z);
    //    Vector3 downwardMove = new Vector3(BossObject.transform.position.x, -1140, BossObject.transform.position.z);
    //    //Upward Move
    //    BossObject.transform.position = Vector3.MoveTowards(BossObject.transform.position, upwardMove, numeral.SPD_Numeral * Time.deltaTime);
    //    while (BossObject.transform.position != upwardMove)
    //    {
    //        Debug.Log("Waiting for moving");
    //    }
    //    //Upward Move
    //    BossObject.transform.position = Vector3.MoveTowards(BossObject.transform.position, downwardMove, numeral.SPD_Numeral * Time.deltaTime);
    //}
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
        typeVirus = VirusType.Bosss1;
        image = loadingImageFromFilePath("Virus/Boss1");
        detectRange = 10000f;
    }
}