using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Selecting_Stage : MonoBehaviour
{
    [SerializeField] GameObject increaseBtn, decreaseBtn, buffItem, chooseBuffPannel, verticalLayout;
    [SerializeField]
    TMP_Text stageSelected;
    [SerializeField]
    GameObject[] stageArray;

    private int stageNumber = 0;

    List<ItemStruct> listItemBuff = new List<ItemStruct>();
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
    }

    void checkStageWithPlayerData()
    {
        for (int i = 0; i < stageArray.Length; i++)
        {
            //Enable the stage by make the lock com invisible
            if (int.Parse(stageArray[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text) < Player_DataManager.Instance.Player.level.stage)
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
            // Screen.orientation = ScreenOrientation.Landscape;
            // SceneManager.LoadScene("Stage" + int.Parse(level.text));

            openSelectBuff(int.Parse(level.text));
        }
        else
        {
            Debug.Log("Cannot Open this stage cuz you didnot reach that level!!!");
        }

    }
    public void openSelectBuff(int stageNumber)
    {
        //Set title stage
        stageSelected.text = "Stage " + stageNumber;
        //Visible pannel choose buff
        chooseBuffPannel.SetActive(true);
        //Load item type buff
        listItemBuff = Item_DataManager.Instance.itemBuff();
        /*
        Initiate the object in the scence
        */
        foreach (var objectItem in listItemBuff)
        {
            Populate(verticalLayout, objectItem);
        }

    }
    // //Instaniate the object item for each one
    void Populate(GameObject verticalObject, ItemStruct Item)
    {
        //Set data in that prototype 
        buffItem.GetComponent<RawImage>().texture = Item.texture2D;
        //Instaniate the object item
        GameObject item = Instantiate(buffItem, verticalObject.transform);
    }
}
