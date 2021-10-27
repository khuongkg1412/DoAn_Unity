using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItem : MonoBehaviour
{
    public GameObject blurBG;
    public GameObject pannel;
    public void OpenPannelItem(){
        if(pannel != null){
            blurBG.SetActive(true);
            pannel.SetActive(true);
        }
    }

    public void ClosedPannelItem(){
        if(pannel != null){
            blurBG.SetActive(false);
            pannel.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
