using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingItem : MonoBehaviour
{
    public ItemStruct dataItem;
    private TMPro.TMP_Text nameItem, type, description;

    GameObject diamond, coin;

    RawImage dataImage;

    private void Start()
    {
        //loadingData();
    }

    void settingObject()
    {
        nameItem = transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        type = transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        description = transform.GetChild(3).GetComponent<TMPro.TMP_Text>();
        dataImage =
            transform
                .GetChild(0)
                .gameObject
                .transform
                .GetChild(0)
                .GetComponent<RawImage>();
        diamond = transform.GetChild(4).gameObject;
        coin = transform.GetChild(5).gameObject;
    }

    void settingObjectForChest()
    {
        nameItem = transform.GetChild(1).GetComponent<TMPro.TMP_Text>();

        //type = transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        description = transform.GetChild(3).GetComponent<TMPro.TMP_Text>();
        dataImage =
            transform
                .GetChild(0)
                .gameObject
                .transform
                .GetChild(0)
                .GetComponent<RawImage>();
        diamond = transform.GetChild(4).gameObject;
        coin = transform.GetChild(5).gameObject;
    }

    void dataforItem()
    {
        nameItem.text = dataItem.name_Item;
        type.text = dataItem.type_Item;
        description.text = dataItem.description_Item;
        dataImage.texture = loadingImageFromFilePath(dataItem.image_Item);
        dataImage.SetNativeSize();

        diamond.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Diamond.ToString();
        coin.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Coin.ToString();
    }
    void dataforItemChest()
    {
        nameItem.text = dataItem.name_Item;
        description.text = dataItem.description_Item;
        dataImage.texture = loadingImageFromFilePath(dataItem.image_Item);
        dataImage.SetNativeSize();

        diamond.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Diamond.ToString();
        coin.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.concurrency.Coin.ToString();
    }

    public void loadingData()
    {
        switch (dataItem.type_Item)
        {
            case "Medicine_DailyItem":
                settingObject();
                dataforItem();
                break;
            case "Medicine_WeeklyItem":
                settingObject();
                dataforItem();
                break;
            case "Chest":
                settingObjectForChest();
                dataforItemChest();
                break;
            default:
                break;
        }
    }

    Texture2D loadingImageFromFilePath(string Filepath)
    {
        if (Resources.Load<Sprite>(Filepath) != null)
        {
            return Resources.Load<Texture2D>(Filepath);
        }
        return null;
    }
}
