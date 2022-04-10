using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sound : MonoBehaviour
{


    public GameObject soundButton;
    [SerializeField] Sprite sourceOn;
    [SerializeField] Sprite sourceOff;
    private bool isON = true;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        checkSoundStatus();
    }
    public void SoundOnClick()
    {
        this.isON = !isON;
    }
    void checkSoundStatus()
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
    private void Load()
    {

        isON = PlayerPrefs.GetInt("soundVolume") == 1 ? true : false;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("soundVolume", isON ? 1 : 0);
    }

}
