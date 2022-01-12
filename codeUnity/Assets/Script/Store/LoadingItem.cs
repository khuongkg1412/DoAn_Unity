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
    [SerializeField] GameObject diamondItem, coinItem;
    [SerializeField] GameObject diamondChest, coinChest;
    RawImage dataImage;
    enum QuantityButton
    {
        Plus = 0,
        Minus = 1
    }
    [SerializeField] TMPro.TMP_Text quantityText;
    public void quantityControll(int input)
    {
        int currentQuantity = int.Parse(quantityText.text);
        if (input == (int)QuantityButton.Plus)
        {
            if (currentQuantity > 0 && currentQuantity < 99)
            {
                currentQuantity += 1;
                quantityText.text = currentQuantity.ToString();
            }
            else
            {
                Debug.LogError("Cannot Increase More");
            }
        }
        else if (input == (int)QuantityButton.Minus)
        {
            if (currentQuantity > 1 && currentQuantity <= 99)
            {
                currentQuantity -= 1;
                quantityText.text = currentQuantity.ToString();
            }
            else
            {
                Debug.LogError("Cannot Increase More");
            }
        }
    }
    //Instantiate for all objects in the pannel
    void settingObject()
    {
        /*GeneralInform part*/
        nameItem = transform.GetChild(2).transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
        type = transform.GetChild(2).transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        description = transform.GetChild(2).transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        /*ImagePannel part*/
        dataImage = transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<RawImage>();
    }
    //Instantiate for all objects in the pannel but for chest only
    void settingObjectForChest()
    {
        /*GeneralInform part*/
        nameItem = transform.GetChild(2).transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
        type = transform.GetChild(2).transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        description = transform.GetChild(2).transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        /*ImagePannel part*/
        dataImage = transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<RawImage>();
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
        diamondItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Diamond.ToString();
        coinItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Coin.ToString();
    }
    //Set data for each objects but for chest only
    void dataforItemChest()
    {
        nameItem.text = dataItem.name_Item;
        description.text = dataItem.description_Item;
        type.text = "Chest";
        //Load data from Resource folders
        dataImage.texture = loadingImageFromFilePath(dataItem.image_Item);
        dataImage.SetNativeSize();//Set native size for image
        diamondChest.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Diamond.ToString();
        coinChest.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Coin.ToString();
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
