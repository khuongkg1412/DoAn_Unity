using System.Collections.Generic;

public class Character
{
    public Character()
    {

    }
    public Character(NumeralStruct numeralStruct)
    {
        this.numeral = numeralStruct;
    }
    private NumeralStruct numeral;

    private List<ItemStruct> buffInGame = new List<ItemStruct>();

    private bool isDead = false;

    private float score = 0;
    public NumeralStruct returnNumeral()
    {
        return this.numeral;
    }
    public void setScore(float score)
    {
        this.score = score;
    }
    public bool isPlayerDead()
    {
        return isDead;
    }
    public void getDamage(float damageTaken)
    {
        //Minus damage taken by DEF point
        damageTaken -= returnDEF();
        //Minus the HP by taken damage  
        numeral.HP_Numeral -= damageTaken;
        //If Hp point is under 0, set it is equal to 0
        if (numeral.HP_Numeral <= 0)
        {
            numeral.HP_Numeral = 0;
            isDead = true;
        }
    }

    public void getHeal(float healTaken, float maxHP)
    {
        //Inscrease the HP by taken healing 
        this.numeral.HP_Numeral += healTaken;
        //If Hp point is over maximum numeral, set it is equal to max numeral
        if (this.numeral.HP_Numeral > maxHP)
        {
            this.numeral.HP_Numeral = maxHP;
        }
    }

    public void addBuff(ItemStruct item)
    {
        buffInGame.Add(item);
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

}