using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSetting : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject settingPrefab;
    private bool checkClone = false;

    public void settingOnClick()
    {
        if (!checkClone)
        {
            GameObject childOb = Instantiate(settingPrefab, new Vector3(0, 0, 71), Quaternion.identity);
            childOb.transform.parent = GameObject.Find("safeArea").transform;
            checkClone = true;
        }
    }
}
