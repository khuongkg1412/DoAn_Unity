using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DataManager : MonoBehaviour
{
    public static Item_DataManager Instance { get; private set; }

    public List<ItemStruct> Item = new List<ItemStruct>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool checkContainsInListItem(ItemStruct item)
    {
        bool isContain = false;
        Item.ForEach(x =>
        {
            if (x.ID.Equals(item.ID))
            {
                isContain = true;
            }
        });
        return isContain;
    }
    public void setTextureForItem()
    {
        Item.ForEach(item => item.texture2D = loadingImageFromFilePath(item.image_Item));
    }
    Texture2D loadingImageFromFilePath(string Filepath)
    {
        if (Resources.Load<Sprite>(Filepath) != null)
        {
            return Resources.Load<Texture2D>(Filepath);
        }
        return null;
    }

    public List<ItemStruct> loadItemPieces()
    {
        List<ItemStruct> itemPieces = new List<ItemStruct>();
        foreach (var i in Item)
        {
            if (i.type_Item == (int)TypeItem.Piece)
            {
                if (!itemPieces.Contains(i))
                {
                    itemPieces.Add(i);
                }
            }
        }
        return itemPieces;
    }



    public List<ItemStruct> loadItemChest()
    {
        List<ItemStruct> itemChest = new List<ItemStruct>();
        foreach (var i in Item)
        {
            if (i.type_Store == (int)Type_Store.Chest)
            {
                if (!itemChest.Contains(i))
                {
                    itemChest.Add(i);
                }
            }
        }

        return itemChest;
    }

    public List<ItemStruct> loadItemBuff()
    {
        List<ItemStruct> itemBuff = new List<ItemStruct>();
        foreach (var i in Item)
        {
            if (i.type_Item == (int)TypeItem.Buff)
            {
                if (!itemBuff.Contains(i))
                {
                    itemBuff.Add(i);
                }
            }
        }
        return itemBuff;
    }
}
