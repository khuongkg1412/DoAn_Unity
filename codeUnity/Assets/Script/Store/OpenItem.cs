using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItem : MonoBehaviour
{
    public GameObject blurBG;
    public GameObject pannel;
    public GameObject pannelChest;
    public void OpenPannelItem(int i){
        if(pannel != null){
            if(i>0){
            blurBG.SetActive(true);
            pannel.SetActive(false);
            pannelChest.SetActive(true);
            }else{
            blurBG.SetActive(true);
            pannel.SetActive(true);
            pannelChest.SetActive(false);
            }
           
        }
    }

    public void ClosedPannelItem(){
        if(pannel != null){
            blurBG.SetActive(false);
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
