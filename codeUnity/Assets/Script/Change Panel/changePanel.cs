using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePanel : MonoBehaviour
{
    public GameObject chat_popup;

    public void openChatPopup()
    {
        if (chat_popup != null)
        {
            chat_popup.SetActive(true);
        }
    }
    public void closeChatPopup()
    {
            chat_popup.SetActive(false);
    }



}
