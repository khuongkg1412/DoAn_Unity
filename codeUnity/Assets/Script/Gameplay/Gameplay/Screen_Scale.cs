using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Scale : MonoBehaviour
{
    public GameObject gridMain;

    private void Start()
    {
        Debug.Log("Width: " + (float) Screen.width);
        Debug.Log("Width: " + (float) Screen.height);
        Debug.Log("SafeArea: " + Screen.safeArea);
    }

    private void Update()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 1080.0f / 1920f;

        // determine the game window's current aspect ratio
        //float windowaspect = (float) Screen.width / (float) Screen.height;
        float windowaspect = (float) Screen.safeArea.width / (float) Screen.safeArea.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        
        // obtain camera component so we can modify its viewport
        // Camera camera = GetComponent<Camera>();
        float scalewidth = 1.0f / scaleheight;
        gridMain.transform.localScale = new Vector3(scaleheight, scalewidth, 0);
    }
}
