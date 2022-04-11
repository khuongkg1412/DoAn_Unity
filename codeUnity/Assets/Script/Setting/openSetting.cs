using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class openSetting : MonoBehaviour
{
    [SerializeField] GameObject settingPrefab;

    // Create Prefab when clicking setting button
    public void settingOnClick()
    {
        GameObject childOb = Instantiate(settingPrefab, new Vector3(0, 0, 70), transform.rotation);
        childOb.transform.parent = GameObject.Find("Canvas").transform;
        childOb.transform.localScale = new Vector3(1, 1, 1);
        childOb.transform.Find("close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(childOb));
    }
}
