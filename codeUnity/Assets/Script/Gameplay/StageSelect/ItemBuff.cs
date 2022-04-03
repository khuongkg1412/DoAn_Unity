using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemBuff : MonoBehaviour
{
    public ItemStruct itemBuff;
    public int index;
    public GameObject canvas;
    public bool isGameplay = false;
    [SerializeField] Image imageCoolDown;
    [SerializeField] TMP_Text coolDownText;
    private float coolDownBuff, coolDownTimer = 0f, buffEffect = 0f, buffEffectTimer = 0f, originNumeral, numberOfBuff = 0f;
    private bool isCoolDown = false, isEffect = false;
    private void Update()
    {
        //Check cool down time of buff
        if (isCoolDown)
        {
            applyCoolDown();
        }
        //Check cool down time of effect from the buff
        if (isEffect)
        {
            BuffEffect();
        }
    }
    public void pressItem()
    {
        //Set index position in list of buff
        canvas.GetComponent<Selecting_Stage>().indexofBuff = index;
    }
    public void setDataForBuff()
    {
        //Set image for buff that has been choosed in Selecting Stage
        gameObject.GetComponent<RawImage>().texture = itemBuff.texture2D;

        numberOfBuff = Player_DataManager.Instance.inventory_Player.Find(x => x.ID == itemBuff.ID).quantiy;
        gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfBuff;
    }
    public void pressItemInGamePlay()
    {
        if (numberOfBuff > 0)
        {
            //Ativate the buff, process base on their name
            switch (itemBuff.name_Item)
            {
                case "Heal":
                    HealingBuff();
                    break;
                case "Speed":
                    SpeedBuff();
                    break;
                case "AttackSpeed":
                    AttackSpeedBuff();
                    break;
                case "Shield":
                    ShieldBuff();
                    break;
                case "Revive":
                    ReviveBuff();
                    break;
            }
        }
        else
        {
            Debug.Log("There no buffs");
        }
        //Update After Pressing The buff
        Player_DataManager.Instance.updateBuffInInventory(itemBuff, (int)numberOfBuff);
    }

    /*
    Process for Buff effect timer
    */
    void BuffEffect()
    {
        //Plus timer 
        buffEffectTimer += Time.deltaTime;
        //Reach the time of buff effect
        if (buffEffectTimer >= buffEffect)
        {
            //reset Timer
            buffEffectTimer = 0f;
            //No longer Effect
            isEffect = false;
            //Find gameobject Player with tag
            GameObject player = GameObject.FindWithTag("Player");
            //Reset the numeral of player, process base on their name
            switch (itemBuff.name_Item)
            {
                case "Speed":
                    //Set the speed Numeral to the origin
                    player.GetComponent<Player_Controller>().Character.setSPD(originNumeral);
                    Debug.Log("return SPD_ " + player.GetComponent<Player_Controller>().Character.returnSPD());
                    break;
                case "AttackSpeed":
                    //Set the speed Numeral to the origin
                    player.GetComponent<Player_Controller>().Character.setATKSPD(originNumeral);
                    Debug.Log("return ATKSPD_ " + player.GetComponent<Player_Controller>().Character.returnATKSPD());
                    break;
                case "Shield":
                    //Set the speed Numeral to the origin
                    player.GetComponent<Player_Controller>().Character.setDEF(originNumeral);
                    Debug.Log("return DEF " + player.GetComponent<Player_Controller>().Character.returnDEF());
                    break;

            }
        }
    }
    /*
    Process for Buff cool down timer
    */
    void applyCoolDown()
    {
        //Subtract time
        coolDownTimer -= Time.deltaTime;
        // Times up
        if (coolDownTimer < 0.0f)
        {
            //No longer count down
            isCoolDown = false;
            //Set text for Timer
            coolDownText.gameObject.SetActive(false);
            //The image cool down
            imageCoolDown.fillAmount = 0.0f;
            //Can activate the buff
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            coolDownText.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCoolDown.fillAmount = coolDownTimer / coolDownBuff;
        }
    }

    /*
        Process for Healing Buff
    */
    public void HealingBuff()
    {
        //Get Object Player base on tag
        GameObject player = GameObject.FindWithTag("Player");
        //Check current health, cannot heal player if the Hp is full
        if (player.GetComponent<Player_Controller>().Character.returnHP() == Player_DataManager.Instance.playerCharacter.returnHP())
        {
            Debug.Log("Player is full HP, cannot use this buff");
        }
        else
        {
            //Decrease buff number
            numberOfBuff -= 1;
            gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfBuff;
            //Plus 10 HP to the current 
            Debug.Log("Healing Up_" + itemBuff.numeral_Item.HP_Numeral);
            player.GetComponent<Player_Controller>().Character.getHeal(itemBuff.numeral_Item.HP_Numeral, Player_DataManager.Instance.playerCharacter.returnHP());
            //Set timer cool down
            coolDownBuff = 10f;
            coolDownTimer = coolDownBuff;
            //Text is active true
            coolDownText.gameObject.SetActive(true);
            //Start Count down
            isCoolDown = true;
            //Cannot click the buff in the cool down time
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
    /*
        Process for  Speed Buff
    */
    public void SpeedBuff()
    {
        //Decrease buff number
        numberOfBuff -= 1;
        gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfBuff;
        //Get Object Player base on tag
        GameObject player = GameObject.FindWithTag("Player");
        //Store the origin numeral before get buff effect
        originNumeral = player.GetComponent<Player_Controller>().Character.returnSPD();
        //The Speed after geting buff
        double speedUP = player.GetComponent<Player_Controller>().Character.returnSPD() + itemBuff.numeral_Item.SPD_Numeral;
        Debug.Log("speedUP _" + itemBuff.numeral_Item.SPD_Numeral);
        //Set it to Character
        player.GetComponent<Player_Controller>().Character.setSPD((float)speedUP);
        //Cool Down Time
        coolDownBuff = 30f;
        coolDownTimer = coolDownBuff;
        //Set time for effect of buff
        buffEffect = 10f;
        //Text is active true
        coolDownText.gameObject.SetActive(true);
        //Start Count down for cooldown and effect time
        isCoolDown = true;
        isEffect = true;
        //Cannot click the buff in the cool down time
        gameObject.GetComponent<Button>().interactable = false;

    }
    /*
        Process for ATK Speed Buff
    */
    public void AttackSpeedBuff()
    {
        //Decrease buff number
        numberOfBuff -= 1;
        gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfBuff;
        //Get Object Player base on tag
        GameObject player = GameObject.FindWithTag("Player");
        //Store the origin numeral before get buff effect
        originNumeral = player.GetComponent<Player_Controller>().Character.returnATKSPD();
        //The Speed after geting buff
        double speedUP = player.GetComponent<Player_Controller>().Character.returnATKSPD() - itemBuff.numeral_Item.ATKSPD_Numeral;
        Debug.Log("ATKspeedUP _" + speedUP);
        //Set it to Character
        player.GetComponent<Player_Controller>().Character.setATKSPD((float)speedUP);
        //Cool Down Time
        coolDownBuff = 30f;
        coolDownTimer = coolDownBuff;
        //Set time for effect of buff
        buffEffect = 10f;
        //Text is active true
        coolDownText.gameObject.SetActive(true);
        //Start Count down for cooldown and effect time
        isCoolDown = true;
        isEffect = true;
        //Cannot click the buff in the cool down time
        gameObject.GetComponent<Button>().interactable = false;

    }

    /*
       Process for Shield Buff
   */
    public void ShieldBuff()
    {
        //Decrease buff number
        numberOfBuff -= 1;
        gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfBuff;
        //Get Object Player base on tag
        GameObject player = GameObject.FindWithTag("Player");
        //Store the origin numeral before get buff effect
        originNumeral = player.GetComponent<Player_Controller>().Character.returnDEF();
        //The Speed after geting buff
        double speedUP = player.GetComponent<Player_Controller>().Character.returnDEF() + itemBuff.numeral_Item.DEF_Numeral;
        Debug.Log("DEF UP _" + speedUP);
        //Set it to Character
        player.GetComponent<Player_Controller>().Character.setDEF((float)speedUP);
        //Cool Down Time
        coolDownBuff = 30f;
        coolDownTimer = coolDownBuff;
        //Set time for effect of buff
        buffEffect = 10f;
        //Text is active true
        coolDownText.gameObject.SetActive(true);
        //Start Count down for cooldown and effect time
        isCoolDown = true;
        isEffect = true;
        //Cannot click the buff in the cool down time
        gameObject.GetComponent<Button>().interactable = false;

    }

    /*
   Process for Revive Buff
    */
    public void ReviveBuff()
    {
        //Decrease buff number
        numberOfBuff -= 1;
        gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfBuff;
        //Get Object Player base on tag
        GameObject player = GameObject.FindWithTag("Player");
        //Store the origin numeral before get buff effect
        player.GetComponent<Player_Controller>().Character.isRevive = true;
        //Cannot click the buff in the cool down time
        gameObject.GetComponent<Button>().interactable = false;
        //Cannot click the buff in the cool down time
        gameObject.transform.GetChild(3).gameObject.SetActive(true);

    }
}
