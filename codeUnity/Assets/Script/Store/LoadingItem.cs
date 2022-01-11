using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingItem : MonoBehaviour
{
    //Store Item that would be loaded into Pannel
    public ItemStruct dataItem;
    //Text content for name, type and description
    private TMPro.TMP_Text nameItem, type, description;
    //Get Diamond, coin
    GameObject diamond, coin;

    RawImage dataImage;

    //Instantiate for all objects in the pannel
    void settingObject()
    {
        nameItem = transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        type = transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        description = transform.GetChild(3).GetComponent<TMPro.TMP_Text>();
        dataImage = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<RawImage>();
        diamond = transform.GetChild(4).gameObject;
        coin = transform.GetChild(5).gameObject;
    }
    //Instantiate for all objects in the pannel but for chest only
    void settingObjectForChest()
    {
        nameItem = transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        description = transform.GetChild(3).GetComponent<TMPro.TMP_Text>();
        dataImage = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<RawImage>();
        diamond = transform.GetChild(4).gameObject;
        coin = transform.GetChild(5).gameObject;
    }
    //Set data for each objects
    void dataforItem()
    {
        nameItem.text = dataItem.name_Item;
        if (dataItem.type_Item == (int)TypeItem.ItemDaily)
        {
            type.text = "Item Daily";
        }
        else if (dataItem.type_Item == (int)TypeItem.ItemWeekly)
        {
            type.text = "Item Weekly";
        }
        description.text = dataItem.description_Item;
        //Load data from Resource folders
        dataImage.texture = loadingImageFromFilePath(dataItem.image_Item);
        dataImage.SetNativeSize(); //Set native size for image
        diamond.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Diamond.ToString();
        coin.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Coin.ToString();
    }
    //Set data for each objects but for chest only
    void dataforItemChest()
    {
        nameItem.text = dataItem.name_Item;
        description.text = dataItem.description_Item;
        //Load data from Resource folders
        dataImage.texture = loadingImageFromFilePath(dataItem.image_Item);
        dataImage.SetNativeSize();//Set native size for image
        diamond.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Diamond.ToString();
        coin.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Coin.ToString();
    }
    /*
    This method is use to determine what type of item is, and what function would be call to process that type
    Actually it would be call from OpenItem.cs
    */
    public void loadingData()
    {
        switch (dataItem.type_Item)
        {
            case (int)TypeItem.ItemDaily:
                settingObject();
                dataforItem();
                break;
            case (int)TypeItem.ItemWeekly:
                settingObject();
                dataforItem();
                break;
            case (int)TypeItem.Chest:
                settingObjectForChest();
                dataforItemChest();
                break;
            default:
                break;
        }
    }
    //Insert filepath then load image from Resouce folder
    Texture2D loadingImageFromFilePath(string Filepath)
    {
        //Check filepath is valid
        if (Resources.Load<Sprite>(Filepath) != null)
        {
            //Return image in Texture2D type
            return Resources.Load<Texture2D>(Filepath);
        }
        return null;
    }
}
