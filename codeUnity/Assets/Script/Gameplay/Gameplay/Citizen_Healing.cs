using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Citizen_Healing : MonoBehaviour
{
    float countTime = 0f;

    //Helth bar object
    public GameObject TimeHealingBar;

    public GameObject citizen = null;

    public void setCitizenObject(GameObject sentCitizen)
    {
        citizen = sentCitizen;
    }

    [System.Obsolete]
    public void selectedUpdate()
    {
        if (citizen != null)
        {
            Debug.Log("Run");
            if (citizen.GetComponent<Citizen_Helping>().isSicked == true)
            {
                TimeHealingBar.SetActive(true);
                countTime += Time.deltaTime;

                citizen.GetComponent<Citizen_Helping>().isSicked = false;

                citizen.GetComponent<Citizen_Helping>().isHeal = true;

                citizen.GetComponent<Citizen_Helping>().getHeal();
            }
        }
    }

    public void pointerUp()
    {
        TimeHealingBar.SetActive(false);
        if (citizen != null)
        {
            countTime = 0f;
            citizen.GetComponent<Citizen_Helping>().isSicked = true;

            citizen.GetComponent<Citizen_Helping>().isHeal = false;

            GameObject myEventSystem = GameObject.Find("EventSystem");
            
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
