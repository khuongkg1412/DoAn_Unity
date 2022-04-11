using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class openPause : MonoBehaviour
{
    [SerializeField] GameObject settingPrefab;

    // Create Prefab when clicking pause button
    public void pauseOnClick()
    {
        GameObject childOb = Instantiate(settingPrefab, transform.position, transform.rotation);
        childOb.transform.parent = GameObject.Find("Canvas").transform;
        childOb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        childOb.transform.position = new Vector3(0, 0, 70);
        childOb.transform.Find("close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(childOb));
    }
}
