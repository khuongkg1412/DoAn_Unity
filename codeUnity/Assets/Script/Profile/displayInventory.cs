using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class displayInventory : MonoBehaviour
{

    public GameObject piece_infor;
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
            ItemStruct anItem = findItembyID(item.ID);
            if (anItem != null && anItem.type_Item > 2)
            {
                slot += 1;
                obj = GameObject.Find("S" + slot + "/Item");
                Texture2D ItemImage = anItem.texture2D;
                Sprite sprite = Sprite.Create(ItemImage, new Rect(0.0f, 0.0f, ItemImage.width, ItemImage.height), new Vector2(0.5f, 0.5f), 100.0f);
                obj.GetComponent<Image>().sprite = sprite;

                obj = GameObject.Find("S" + slot + "/quantity");
                obj.GetComponent<Text>().text = item.quantiy.ToString();

                obj = GameObject.Find("S" + slot + "/ID");
                obj.GetComponent<Text>().text = item.ID.ToString();
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

    private ItemStruct findItembyID(string ID)
    {
        foreach (ItemStruct Item in Item_DataManager.Instance.Item)
        {
            if (ID.Equals(Item.ID)) return Item;
        }
        return null;
    }

    private string findItembyName(string nameItem)
    {
        foreach (ItemStruct Item in Item_DataManager.Instance.Item)
        {
            if (nameItem.Equals(Item.name_Item) && Item.type_Item != 4) return Item.ID;
        }
        return null;
    }

    private Inventory_Player findItemInInventory(string ID)
    {
        foreach (Inventory_Player item in Player_DataManager.Instance.inventory_Player)
        {
            if (ID.Equals(item.ID)) return item;
        }
        return null;
    }

    public void displayInforItem(GameObject slot)
    {
        string ID = GameObject.Find(slot.name + "/ID").GetComponent<Text>().text;
        ItemStruct anItem = findItembyID(ID);
        Inventory_Player anItemInInventory = findItemInInventory(ID);

        GameObject scrollItemObj;

        if (anItem.type_Item == 4)
        {
            Texture2D OutfitImage = anItem.texture2D;
            Sprite sprite = Sprite.Create(OutfitImage, new Rect(0.0f, 0.0f, OutfitImage.width, OutfitImage.height), new Vector2(0.5f, 0.5f), 100.0f);

            scrollItemObj = (GameObject)Instantiate(piece_infor, transform);

            scrollItemObj.transform.Find("Name Item").gameObject.GetComponent<Text>().text = "Piece of " + anItem.name_Item;
            scrollItemObj.transform.Find("infor box/Image").gameObject.GetComponent<Image>().sprite = sprite;
            scrollItemObj.transform.Find("infor box/piece").gameObject.GetComponent<Text>().text = anItemInInventory.quantiy + " / " + anItem.piece;
            scrollItemObj.transform.Find("close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(scrollItemObj));

            if (anItemInInventory.quantiy == anItem.piece)
            {
                scrollItemObj.transform.Find("infor box/ok_btn").gameObject.GetComponent<Button>().interactable = true;
                scrollItemObj.transform.Find("infor box/ok_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => CraftPieceToItem(anItem));
            }
        }
    }

    private void CraftPieceToItem(ItemStruct piece)
    {
        string outfitID = findItembyName(piece.name_Item);
        Inventory_Player anItemInInventory = findItemInInventory(piece.ID);

        Player_DataManager.Instance.CraftItem(outfitID, anItemInInventory);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
