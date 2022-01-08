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
}
