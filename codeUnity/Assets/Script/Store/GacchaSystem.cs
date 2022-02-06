using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GacchaSystem : MonoBehaviour
{
    public LootTable gacchaObject;
    public GameObject Layout;
    public RawImage dataImage;

    public TMPro.TMP_Text itemName, itemQuantity;
    private List<ItemStruct> _itemsList = new List<ItemStruct>();
    string IDForItemRemove;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            ItemStruct item = gacchaObject.GetRandomItem();
            _itemsList.Add(item);
        }
    }
    private void Update()
    {
        if (_itemsList.Count > 0)
        {
            Populate(_itemsList[0], CountItem(_itemsList[0]));
        }
    }
    int CountItem(ItemStruct item)
    {
        int count = 0;
        foreach (var i in _itemsList)
        {
            if (i.ID == item.ID)
            {
                count += 1;
            }
        }
        RemoveItem(item);
        return count;
    }
    void RemoveItem(ItemStruct item)
    {
        IDForItemRemove = item.ID;
        _itemsList.RemoveAll(RateForItem);
    }
    bool RateForItem(ItemStruct item)
    {
        return item.ID == IDForItemRemove;
    }
    //Instaniate the object item for each one
    void Populate(ItemStruct Item, int count)
    {
        //The prototype of each item in store
        GameObject prefab = GameObject.Find("ItemReceived"); // Create GameObject instance
        //Set data in that prototype 
        dataImage.texture = Item.texture2D;
        //dataImage.SetNativeSize();
        itemName.text = Item.name_Item;
        itemQuantity.text = "x" + count.ToString();
        //Instaniate the object item
        GameObject item = Instantiate(prefab, Layout.transform);
        //Set data type for each prototype
        //item.GetComponent<OpenItem>().dataItem = Item;
    }

}
