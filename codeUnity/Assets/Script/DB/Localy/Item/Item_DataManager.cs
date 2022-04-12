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

    public List<ItemStruct> itemPieces()
    {
        List<ItemStruct> itemPieces = new List<ItemStruct>();
        foreach (var i in Item)
        {
            if (i.type_Item == (int)TypeItem.Piece)
            {
                itemPieces.Add(i);
            }
        }
        return itemPieces;
    }

    public List<ItemStruct> itemChest()
    {
        List<ItemStruct> itemChest = new List<ItemStruct>();
        foreach (var i in Item)
        {
            if (i.type_Store == (int)Type_Store.Chest)
            {
                itemChest.Add(i);
            }
        }
        return itemChest;
    }

    public List<ItemStruct> itemBuff()
    {
        List<ItemStruct> itemBuff = new List<ItemStruct>();
        foreach (var i in Item)
        {
            if (i.type_Item == (int)TypeItem.Buff)
            {
                itemBuff.Add(i);
            }
        }
        return itemBuff;
    }
}
