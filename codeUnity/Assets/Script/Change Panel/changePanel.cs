using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePanel : MonoBehaviour
{

    public GameObject popup;


    public void openPopupPanel()
    {
        if (popup != null)
        {
            popup.SetActive(true);
        }
    }
    public void closePopupPanel()
    {
        popup.SetActive(false);
    }



}
