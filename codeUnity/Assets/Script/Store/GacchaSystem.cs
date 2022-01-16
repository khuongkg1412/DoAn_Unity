using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GacchaSystem : MonoBehaviour
{
    public LootTable gacchaObject;
    // Start is called before the first frame update
    void Start()
    {
        ItemStruct item = gacchaObject.GetRandomItem();
        Debug.Log("Gacha :" + item.name_Item);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
