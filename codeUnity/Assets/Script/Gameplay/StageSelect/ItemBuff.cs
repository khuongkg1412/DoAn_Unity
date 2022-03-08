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
    private float coolDownBuff, coolDownTimer = 0f, buffEffect = 0f, buffEffectTimer = 0f, originNumeral;
    private bool isCoolDown = false, isEffect = false;

    public void pressItem()
    {
        //Set index position in list of buff
        canvas.GetComponent<Selecting_Stage>().indexofBuff = index;
    }
    public void setImage()
    {
        //Set image for buff that has been choosed in Selecting Stage
        gameObject.GetComponent<RawImage>().texture = itemBuff.texture2D;
    }
    public void pressItemInGamePlay()
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
        }
    }
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
            //Plus 10 HP to the current 
            player.GetComponent<Player_Controller>().Character.getHeal(10f, Player_DataManager.Instance.playerCharacter.returnHP());
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
        Process for Healing Buff
    */
    public void SpeedBuff()
    {
        //Get Object Player base on tag
        GameObject player = GameObject.FindWithTag("Player");
        //Store the origin numeral before get buff effect
        originNumeral = player.GetComponent<Player_Controller>().Character.returnSPD();
        //The Speed after geting buff
        double speedUP = player.GetComponent<Player_Controller>().Character.returnSPD() * 1.5;
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
}
