using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeName : MonoBehaviour
{
    public Text newName;

    public void acceptChange()
    {
        if (newName.text.Length == 0)
        {
            Debug.Log("chưa có gì hết");
        }
        else
        {
            Player_DataManager.Instance.changeName(newName.text);

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
