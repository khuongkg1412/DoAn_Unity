using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPlayer_DataManager : MonoBehaviour
{
    public static ListPlayer_DataManager Instance { get; private set; }

    public List<PlayerStruct> listPlayer = new List<PlayerStruct>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
