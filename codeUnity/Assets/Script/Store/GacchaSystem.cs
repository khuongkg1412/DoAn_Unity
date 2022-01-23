using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GacchaSystem : MonoBehaviour
{
    public LootTable gacchaObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            ItemStruct item = gacchaObject.GetRandomItem();

            switch (item.rate_Item)
            {
                case RateItem.Common:
                    Debug.Log("Gacha : Common " + item.rate_Item);
                    break;
                case RateItem.Rare:
                    Debug.Log("Gacha :Rare " + item.rate_Item);
                    break;
                case RateItem.Epic:
                    Debug.Log("Gacha :Epic " + item.rate_Item);
                    break;
                case RateItem.Legendary:
                    Debug.Log("Gacha :Legendary " + item.rate_Item);
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
