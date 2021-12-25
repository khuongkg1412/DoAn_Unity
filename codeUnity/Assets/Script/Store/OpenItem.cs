using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenItem : MonoBehaviour
{
    [SerializeField]
    GameObject dataHandeling;

    public ItemStruct dataItem;

    public GameObject blurBG;

    public GameObject pannel;

    public GameObject pannelChest;

    // public void OpenPannelItem(int i){
    //     if(pannel != null){
    //         if(i>0){
    //         blurBG.SetActive(true);
    //         pannel.SetActive(false);
    //         pannelChest.SetActive(true);
    //         }else{
    //         blurBG.SetActive(true);
    //         pannel.SetActive(true);
    //         pannelChest.SetActive(false);
    //         }
    //     }
    // }
    public void OpenPannelItem()
    {
        switch (dataItem.type_Item)
        {
            case "Medicine_DailyItem":
                blurBG.SetActive(true);
                pannel.SetActive(true);
                pannel.GetComponent<LoadingItem>().dataItem = dataItem;
                pannelChest.SetActive(false);
                break;
            case "Medicine_WeeklyItem":
                blurBG.SetActive(true);
                pannel.SetActive(true);
                pannelChest.SetActive(false);
                break;
            case "Chest":
                blurBG.SetActive(true);
                pannel.SetActive(false);
                pannelChest.SetActive(true);
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
