using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePanel : MonoBehaviour
{

    public GameObject oldPopUp;
    public GameObject newPopUp;
    private void Start()
    {
        //orinPos.position = oldPopUp.transform.position;
    }
    public void openPopUp()
    {
        oldPopUp.transform.position = new Vector3(1900, 0, 0);    
        newPopUp.transform.position = new Vector3(0, 0, 0);
    }    


}
