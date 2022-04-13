using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Selecting_Stage : MonoBehaviour
{
    [SerializeField] GameObject increaseBtn, decreaseBtn, buffItem, chooseBuffPannel, verticalLayout, btnPlay, Error;
    [SerializeField]
    TMP_Text stageSelected, buffSelect;
    [SerializeField]
    GameObject[] stageArray;

    private int stageNumber = 0;
    private int levelStage = 0;
    List<ItemStruct> listItemBuff = new List<ItemStruct>();
    bool isInstaniate = false;
    public void increaseNumberForStage()
    {
        foreach (var i in stageArray)
        {
            stageNumber += 1;
            i.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = stageNumber.ToString();
        }
    }
    public void decreaseNumberForStage()
    {
        stageNumber -= 8;
        for (int i = stageArray.Length - 1; i >= 0; i--)
        {
            stageNumber -= 1;
            stageArray[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = stageNumber.ToString();
        }
        stageNumber = int.Parse(stageArray[8].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
    }

    private void Start()
    {
        //Set number on stage button
        increaseNumberForStage();
    }
    private void Update()
    {
        //Check DB for stage 
        checkStageWithPlayerData();
        //Check number to disable or enable the change stage button
        if (stageNumber <= 9)
        {
            increaseBtn.GetComponent<Button>().interactable = true;
            decreaseBtn.GetComponent<Button>().interactable = false;
        }
        else if (stageNumber > 10)
        {
            increaseBtn.GetComponent<Button>().interactable = false;
            decreaseBtn.GetComponent<Button>().interactable = true;
        }

        if (chooseBuffPannel.activeInHierarchy)
        {
            //   bool check = false;
            for (int i = 0; i < listBuff.Count; i++)
            {
                if (i == indexofBuff)
                {
                    listBuff[i].transform.GetChild(1).GetComponent<RawImage>().enabled = true;
                    buffSelect.text = "Choosing Buff: " + listBuff[i].GetComponent<ItemBuff>().itemBuff.name_Item;
                }
                else
                {
                    listBuff[i].transform.GetChild(1).GetComponent<RawImage>().enabled = false;
                }
            }
        }
    }

    void checkStageWithPlayerData()
    {
        for (int i = 0; i < stageArray.Length; i++)
        {
            //Enable the stage by make the lock com invisible
            if (int.Parse(stageArray[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text) <= Player_DataManager.Instance.Player.level.stage)
            {
                stageArray[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else //Make the lock visible to disable the stage
            {
                stageArray[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void loadScence(TMP_Text level)
    {
        //PlayerStruct player = SaveSystem.LoadDataPlayer();
        if (int.Parse(level.text) <= Player_DataManager.Instance.Player.level.stage)
        {
            levelStage = int.Parse(level.text);
            openSelectBuff(int.Parse(level.text));
        }
        else
        {
            Debug.Log("Cannot Open this stage cuz you didnot reach that level!!!");
        }

    }
    public void CancelButton()
    {
        //Invisible pannel choose buff
        chooseBuffPannel.SetActive(false);
    }
    public void openSelectBuff(int stageNumber)
    {
        //Set title stage
        stageSelected.text = "Stage " + stageNumber;
        //Visible pannel choose buff
        chooseBuffPannel.SetActive(true);
        //Load item type buff
        listItemBuff = Item_DataManager.Instance.itemBuffC();
        //Index to set to the index of buff list

        /*
        Initiate the object in the scence
        */
        if (!isInstaniate)
        {
            isInstaniate = true;
            foreach (var objectItem in listItemBuff)
            {
                foreach (var inventItem in Player_DataManager.Instance.inventory_Player)
                {
                    if (inventItem.ID.Equals(objectItem.ID))
                    {
                        Populate(verticalLayout, objectItem, (int)inventItem.quantiy);
                    }
                }
            }
        }

    }
    List<GameObject> listBuff = new List<GameObject>();
    public int indexofBuff = 0;
    // //Instaniate the object item for each one
    void Populate(GameObject verticalObject, ItemStruct Item, int quantity)
    {
        //Set data in that prototype 
        buffItem.GetComponent<RawImage>().texture = Item.texture2D;
        buffItem.transform.GetChild(0).GetComponent<TMP_Text>().text = "x" + quantity;

        //Instaniate the object item
        GameObject item = Instantiate(buffItem, verticalObject.transform);
        listBuff.Add(item);
        item.GetComponent<ItemBuff>().itemBuff = Item;
        item.GetComponent<ItemBuff>().index = indexofBuff;
        indexofBuff += 1;
    }

    public void pressPlayButton()
    {
        if (Player_DataManager.Instance.Player.level.life > 0)
        {
            //Add buff to Character in instace Player_DataManager
            ItemStruct item = listBuff[indexofBuff].GetComponent<ItemBuff>().itemBuff;
            Player_DataManager.Instance.playerCharacter.setBuff(item);
            Player_DataManager.Instance.decreaseLife();
            Player_DataManager.Instance.timeofLastPlay();
            //Load Stage 
            Screen.orientation = ScreenOrientation.Landscape;
            SceneManager.LoadScene("Stage" + levelStage);
        }
        else
        {
            GameObject ErorrToast = (GameObject)Instantiate(Error, transform);
            ErorrToast.transform.Find("Message").gameObject.GetComponent<Text>().text = "Run out of energy";
            Destroy(ErorrToast, 2);
        }
    }
}
