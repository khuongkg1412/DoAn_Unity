using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSetting : MonoBehaviour
{
    [SerializeField] GameObject settingPrefab;

    private bool checkClone = false;

    // Create Prefab when clicking setting button
    public void settingOnClick()
    {
        if (!checkClone)
        {
            GameObject childOb = Instantiate(settingPrefab, new Vector3(0, 0, 71), Quaternion.identity);
            childOb.transform.parent = GameObject.Find("safeArea").transform;
            checkClone = true;
        }
    }
    public void closeOnClick()
    {
        Destroy(settingPrefab);
        checkClone = false;
    }
}
