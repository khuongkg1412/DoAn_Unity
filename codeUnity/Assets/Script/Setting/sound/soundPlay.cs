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
            Debug.Log("Dang chay ne");
            instance = this;
            DontDestroyOnLoad(gameObject);
            //return;
        }
        else
        {
            Destroy(gameObject);
        }
        // if (instance == this) return;
        //Destroy(gameObject);
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.Play();
    }
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        Debug.Log("Destroy roi do ban!");
    }
}
