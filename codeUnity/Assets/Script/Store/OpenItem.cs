using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenItem : MonoBehaviour
{
    public ItemStruct dataItem;

    public GameObject blurBG;

    public GameObject pannel;

    public GameObject pannelChest;

    public void OpenPannelItem()
    {
        switch (dataItem.type_Store)
        {
            case (int)Type_Store.ItemDaily:
                blurBG.SetActive(true);
                pannel.SetActive(true);
                pannel.GetComponent<LoadingItem>().dataItem = dataItem;
                pannel.GetComponent<LoadingItem>().loadingData();
                pannelChest.SetActive(false);
                break;
            case (int)Type_Store.ItemWeekly:
                blurBG.SetActive(true);
                pannel.SetActive(true);
                pannel.GetComponent<LoadingItem>().dataItem = dataItem;
                pannel.GetComponent<LoadingItem>().loadingData();
                pannelChest.SetActive(false);
                break;
            case (int)Type_Store.Chest:
                blurBG.SetActive(true);
                pannel.SetActive(false);
                pannelChest.SetActive(true);
                pannelChest.GetComponent<LoadingItem>().dataItem = dataItem;
                pannelChest.GetComponent<LoadingItem>().loadingData();
                break;
            default:
                break;
        }
    }

    public void ClosedPannelItem()
    {
        if (pannel != null)
        {
            blurBG.SetActive(false);
        }
    }
}
