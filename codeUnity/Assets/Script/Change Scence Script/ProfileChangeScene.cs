using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void SwitchFriendTab()
    {
        SceneManager.LoadScene("Friend list");
    }

    // Update is called once per frame
    public void SwitchProfileTab()
    {
        SceneManager.LoadScene("Main screen");
    }

    public void backtoMainPage()
    {
        SceneManager.LoadScene(1);
    }
}
