using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class openPause : MonoBehaviour
{
    [SerializeField] GameObject settingPrefab;
    [SerializeField] GameObject canvass;

    // Create Prefab when clicking pause button
    public void pauseOnClick()
    {
        //Pause the time of game
        Time.timeScale = 0f;
        Vector3 xyzPos = new Vector3();
        //Get position x,y,z of Canvas
        xyzPos.x = canvass.transform.position.x;
        xyzPos.y = canvass.transform.position.y;
        xyzPos.z = canvass.transform.position.z;

        //Create popup by prefab
        GameObject childOb = Instantiate(settingPrefab, new Vector3(xyzPos.x, xyzPos.y, xyzPos.z), transform.rotation);
        childOb.transform.parent = GameObject.Find("Canvas").transform;
        childOb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        childOb.transform.Find("close_btn").gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(childOb));
    }
}
