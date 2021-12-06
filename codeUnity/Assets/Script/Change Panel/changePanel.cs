using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePanel : MonoBehaviour
{

    public GameObject popup;

    Transform orinPos;
    private void Start() {
         //orinPos.position = popup.transform.position;
    }

    public void openPopupPanel()
    {
        // if (popup != null)
        // {
        //     popup.SetActive(true);
        // }

        popup.transform.position = new Vector3(0,0,0);
    }
    public void closePopupPanel()
    {
       // popup.transform.position = orinPos.position;
    }



}
