using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selecting_Stage : MonoBehaviour
{
    //PlayerStruct player;

    [SerializeField]
    GameObject[] stageArray;
    private void Awake()
    {
        //player = SaveSystem.LoadDataPlayer();
    }

    private void Start()
    {
        for (int i = 0; i < stageArray.Length; i++)
        {
            if (i < Player_DataManager.Instance.Player.level_Player)
            {
                enableStage(i);
            }
            else
            {
                disableStage(i);
            }

        }
    }

    void enableStage(int index)
    {
        stageArray[index].transform.GetChild(1).gameObject.SetActive(false);
    }

    void disableStage(int index)
    {
        stageArray[index].transform.GetChild(1).gameObject.SetActive(true);
    }

    public void loadScence(int level)
    {
        //PlayerStruct player = SaveSystem.LoadDataPlayer();
        if (level <= Player_DataManager.Instance.Player.level_Player)
        {
            Screen.orientation = ScreenOrientation.Landscape;
            SceneManager.LoadScene("Stage" + level);
        }
        else
        {
            Debug.Log("Cannot Open this stage cuz you didnot reach that level!!!");
        }

    }
}
