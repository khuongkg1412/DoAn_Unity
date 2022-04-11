using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class soundOnOff : MonoBehaviour
{


    public GameObject soundButton;
    [SerializeField] Sprite sourceOn;
    [SerializeField] Sprite sourceOff;
    public bool isON = true;
    // Update is called once per frame
    void Start()
    {
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    void Update()
    {
        checkSoundStatus();
    }
    public void SoundOnClick()
    {
        this.isON = !isON;
    }
    public void checkSoundStatus()
    {
        if (isON)
        {
            soundButton.GetComponent<Image>().sprite = sourceOn;
            changeVolume();
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = sourceOff;
            changeVolume();
        }
    }
    public void changeVolume()
    {
        AudioListener.volume = isON ? 1 : 0;
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetInt("soundVolume", isON ? 1 : 0);
    }
    private void Load()
    {
        isON = PlayerPrefs.GetInt("soundVolume") == 1 ? true : false;
    }
}
