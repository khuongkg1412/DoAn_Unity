using System.Collections.Generic;

public struct Character
{
    public Character(NumeralStruct numeralStruct)
    {
        this.numeral = numeralStruct;
        this.originalNumeral = numeralStruct;
        this.isMoving = true;
        this.isShooting = true;
        this.isDead = false;
        this.score = 0f;
        this.isRevive = false;
        this.buffInGame = new ItemStruct();
    }
    private NumeralStruct numeral;

    private NumeralStruct originalNumeral;

    private ItemStruct buffInGame;
    private bool isDead;
    private bool isRevive
    {
        get; set;
    }
    private float score
    {
        get; set;
    }

    private bool isMoving
    {
        get; set;
    }
    private bool isShooting
    {
        get; set;
    }
    public void setScore(float score)
    {
        this.score += score;
    }
    public float returnScroe()
    {
        return this.score;
    }
    public void setRevive(bool isRevive)
    {
        this.isRevive = isRevive;
    }
    public bool getRevive()
    {
        return isRevive;
    }
    public bool isMove()
    {
        return isMoving;
    }

    public void setMove(bool isMove)
    {
        isMoving = isMove;
    }

    public bool isShoot()
    {
        return isShooting;
    }

    public void setShoot(bool isShoot)
    {
        isShooting = isShoot;
    }

    public NumeralStruct returnNumeral()
    {
        return this.numeral;
    }

    public bool isPlayerDead()
    {
        return isDead;
    }
    public void getDamage(float damageTaken)
    {
        //Minus damage taken by DEF point
        damageTaken -= (returnDEF() / 2);
        //Minus the HP by taken damage  
        numeral.HP_Numeral -= damageTaken;
        //If Hp point is under 0, set it is equal to 0
        if (numeral.HP_Numeral <= 0)
        {
            if (isRevive)
            {
                this.numeral = originalNumeral;
                this.isRevive = false;
            }
            else
            {
                numeral.HP_Numeral = 0;
                isDead = true;
            }

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

    public void setBuff(ItemStruct item)
    {
        this.buffInGame = item;
    }
    public ItemStruct getItemBuff()
    {
        return this.buffInGame;
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
