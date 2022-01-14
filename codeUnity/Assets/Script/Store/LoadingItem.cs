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
    [SerializeField] GameObject BuyButton;
    RawImage dataImage;
    enum QuantityButton
    {
        Plus = 0,
        Minus = 1
    }
    [SerializeField] TMPro.TMP_Text quantityText;
    GameObject coinToggle, diamondToggle;
    float waitingTime = 0f;
    private void Start()
    {
        coinToggle = GameObject.Find("ToggleCoin");
        diamondToggle = GameObject.Find("ToggleDiamond");
    }
    private void Update()
    {
        updateCoinandDiamond();
        checkValidCurrency();
    }
    void checkValidCurrency()
    {
        float diamond = float.Parse(diamondItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
        float coin = float.Parse(coinItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
        /*
        Coin Part
        */
        if (Player_DataManager.Instance.Player.concurrency.Coin < coin)
        {
            Color red = new Color(1, 0, 0);
            coinItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().color = red;
        }
        else if (Player_DataManager.Instance.Player.concurrency.Coin >= coin)
        {
            Color white = new Color(1, 1, 1);
            coinItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().color = white;
        }
        /*
        Diamond Part
        */
        if (Player_DataManager.Instance.Player.concurrency.Diamond < diamond)
        {
            Color red = new Color(1, 0, 0);
            diamondItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().color = red;
        }
        else if (Player_DataManager.Instance.Player.concurrency.Diamond >= diamond)
        {
            Color white = new Color(1, 1, 1);
            diamondItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().color = white;
        }
        /*
        Interacable button if value is 0 Part
        */
        if (diamond != 0 && coin != 0)
        {
            diamondToggle.GetComponent<Toggle>().interactable = true;
            coinToggle.GetComponent<Toggle>().interactable = true;
        }
        else if (diamond == 0 && coin != 0)
        {
            diamondToggle.GetComponent<Toggle>().interactable = false;
            diamondToggle.GetComponent<Toggle>().isOn = false;
            coinToggle.GetComponent<Toggle>().interactable = true;
            coinToggle.GetComponent<Toggle>().isOn = true;
        }
        else if (coin == 0 && diamond != 0)
        {
            diamondToggle.GetComponent<Toggle>().interactable = true;
            diamondToggle.GetComponent<Toggle>().isOn = true;
            coinToggle.GetComponent<Toggle>().interactable = false;
            coinToggle.GetComponent<Toggle>().isOn = false;
        }
        /*
        Check whether is any toggles's on 
        */

        if ((Player_DataManager.Instance.Player.concurrency.Diamond < diamond) && (Player_DataManager.Instance.Player.concurrency.Coin < coin))
        {
            BuyButton.GetComponent<Button>().interactable = false;
        }
        else if ((Player_DataManager.Instance.Player.concurrency.Diamond < diamond) && (coin == 0))
        {
            BuyButton.GetComponent<Button>().interactable = false;
        }
        else if ((Player_DataManager.Instance.Player.concurrency.Coin < coin) && (diamond == 0))
        {
            BuyButton.GetComponent<Button>().interactable = false;
        }
        else if (coinToggle.GetComponent<Toggle>().isOn || diamondToggle.GetComponent<Toggle>().isOn)
        {
            BuyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            BuyButton.GetComponent<Button>().interactable = false;
        }
    }
    void updateCoinandDiamond()
    {
        //Get the current quantity by parse the string in the text field
        int currentQuantity = int.Parse(quantityText.text);
        if (dataItem.type_Item == (int)TypeItem.ItemDaily || dataItem.type_Item == (int)TypeItem.ItemWeekly)
        {
            diamondItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = (dataItem.concurrency.Diamond * currentQuantity).ToString();
            coinItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = (dataItem.concurrency.Coin * currentQuantity).ToString();
        }
        else if (dataItem.type_Item == (int)TypeItem.Chest)
        {
            diamondChest.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = (dataItem.concurrency.Diamond * currentQuantity).ToString();
            coinChest.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = (dataItem.concurrency.Coin * currentQuantity).ToString();
        }
    }

    public void PressBuyButton()
    {
        Player_DataManager.Instance.adding_Item(dataItem, int.Parse(quantityText.text));
        if (coinToggle.GetComponent<Toggle>().isOn)
        {
            Debug.Log("Buy by Coin");
            float buyAmount = -float.Parse(coinItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
            Player_DataManager.Instance.updateCoinConcurrency(buyAmount);
        }
        else
        {
            Debug.Log("Buy by Diamond");
            float buyAmount = -float.Parse(diamondItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text);
            Player_DataManager.Instance.updateDiamondConcurrency(buyAmount);
        }

    }
    //Controll the quantity of item that user wanna buy
    public void quantityControll(int input)
    {
        //Get the current quantity by parse the string in the text field
        int currentQuantity = int.Parse(quantityText.text);
        //Press Plus button would input 0 is equal to (int)QuantityButton.Plus
        if (input == (int)QuantityButton.Plus)
        {
            //Check whether currentQuantity is in valid range
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
        //Press Plus button would input 0 is equal to (int)QuantityButton.Minus
        else if (input == (int)QuantityButton.Minus)
        {
            //Check whether currentQuantity is in valid range
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
    public void HoldingButtonQuantity(int input)
    {
        waitingTime += Time.deltaTime;
        if (waitingTime > 0.25f)
        {
            waitingTime = 0f;
            quantityControll(input);
        }
    }
    //Pointer Up to stop increase or decrease quantity
    public void PointerUP()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");

        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
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
        quantityText.text = "1";
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
