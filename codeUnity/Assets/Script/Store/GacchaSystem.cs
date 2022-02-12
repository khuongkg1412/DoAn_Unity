using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GacchaSystem : MonoBehaviour
{
    public LootTable gacchaObject;
    public GameObject Layout;
    public RawImage dataImage, ImageChest;

    public TMPro.TMP_Text itemName, itemQuantity;
    private List<ItemStruct> _itemsList = new List<ItemStruct>();
    string IDForItemRemove;
    int randomTime;
    string typeChest;
    [SerializeField] GameObject ChestOpening, itemDisplay, BGChest;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void setRandomTime(int randomTimeSent, ItemStruct itemSent)
    {
        randomTime = randomTimeSent;
        ImageChest.texture = itemSent.texture2D;
    }
    public void backtoStore()
    {
        itemDisplay.SetActive(false);
        BGChest.SetActive(true);
        gacchaItem();
    }
    public void clickToOpen()
    {
        ChestOpening.SetActive(false);
        itemDisplay.SetActive(true);
        gacchaItem();
    }
    public void gacchaItem()
    {
        for (int i = 0; i < randomTime; i++)
        {
            ItemStruct item = gacchaObject.GetRandomItem();
            _itemsList.Add(item);
        }

    }
    private void Update()
    {
        if (_itemsList.Count > 0)
        {
            int quanity = CountItem(_itemsList[0]);
            //Add items to Inventory
            Player_DataManager.Instance.adding_Item(_itemsList[0], quanity);
            Populate(_itemsList[0], quanity);
            RemoveItem(_itemsList[0]);
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
