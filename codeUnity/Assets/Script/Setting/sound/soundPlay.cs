using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundPlay : MonoBehaviour
{
    private static soundPlay instance = null;
    [SerializeField] AudioSource audioSrc;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.Play();
    }

}
