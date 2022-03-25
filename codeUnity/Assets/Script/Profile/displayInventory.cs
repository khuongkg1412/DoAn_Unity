using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayInventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        displayItems();
    }

    private void displayItems()
    {
        int slot = 0;
        foreach (Inventory_Player item in Player_DataManager.Instance.inventory_Player)
        {
            GameObject obj;
            ItemStruct anItem = findItem(item.ID);
            if (anItem != null && anItem.type_Item > 2)
            {
                slot += 1;
                obj = GameObject.Find("S" + slot + "/Item");
                Texture2D ItemImage = anItem.texture2D;
                Sprite sprite = Sprite.Create(ItemImage, new Rect(0.0f, 0.0f, ItemImage.width, ItemImage.height), new Vector2(0.5f, 0.5f), 100.0f);
                obj.GetComponent<Image>().sprite = sprite;

                obj = GameObject.Find("S" + slot + "/quantity");
                obj.GetComponent<Text>().text = item.quantiy.ToString();

            }
        }

        if (slot < 24)
        {
            GameObject obj;
            for (int i = slot + 1; i <= 24; i++)
            {
                obj = GameObject.Find("S" + i + "/Item");
                obj.SetActive(false);

                obj = GameObject.Find("S" + i + "/quantity");
                obj.GetComponent<Text>().text = "";
            }
        }
    }

    ItemStruct findItem(string ID)
    {
        foreach (ItemStruct Item in Item_DataManager.Instance.Item)
        {
            if (ID.Equals(Item.ID)) return Item;
        }
        return null;
    }
}
