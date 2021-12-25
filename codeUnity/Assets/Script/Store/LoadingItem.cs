using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingItem : MonoBehaviour
{
    public ItemStruct dataItem;
    TMPro.TMP_Text name, type, description;
    GameObject diamond, coin;
    private void Start() {
        name = transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        type = transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        description = transform.GetChild(3).GetComponent<TMPro.TMP_Text>();
        diamond = transform.GetChild(4).gameObject;
        coin = transform.GetChild(5).gameObject;
        loadingData();
    }
    void loadingData(){
         switch (dataItem.type_Item)
        {
            case "Medicine_DailyItem":
                name.text = dataItem.name_Item;
                type.text = dataItem.type_Item;
                description.text = dataItem.description_Item ;
                if(dataItem.paymethod_Item == 0){
                    coin.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.price_Item.ToString();
                    diamond.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = "0";
                }else{
                    diamond.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = dataItem.price_Item.ToString();
                    coin.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = "0";
                }
                break;
            case "Medicine_WeeklyItem":

                break;
            case "Chest":

                break;
            default:
                break;
        }
    }
}
