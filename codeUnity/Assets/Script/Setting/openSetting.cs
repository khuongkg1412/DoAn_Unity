using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class openSetting : MonoBehaviour
{
    [SerializeField] GameObject settingPrefab;

    //private bool checkClone = false;

    // Create Prefab when clicking setting button
    public void settingOnClick()
    {
        GameObject childOb = Instantiate(settingPrefab, new Vector3(0, 0, 71), Quaternion.identity);
        childOb.transform.parent = GameObject.Find("safeArea").transform;
        childOb.transform.Find("close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(childOb));
    }

}
