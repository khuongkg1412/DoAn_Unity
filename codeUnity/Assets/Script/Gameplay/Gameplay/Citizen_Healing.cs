using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Citizen_Healing : MonoBehaviour
{

    //Helth bar object
    public GameObject TimeHealingBar;

    public GameObject citizen = null;

    private GameObject player;

    public void setCitizenObject(GameObject sentCitizen)
    {
        citizen = sentCitizen;
    }

    public void disableCitizenObject()
    {
        citizen = null;
        TimeHealingBar.SetActive(false);
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    [System.Obsolete]
    public void selectedUpdate()
    {
        if (citizen != null)
        {
            if (citizen.GetComponent<Citizen_Helping>().isSicked == true)
            {
                TimeHealingBar.SetActive(true);

                // citizen.GetComponent<Citizen_Helping>().isSicked = false;

                citizen.GetComponent<Citizen_Helping>().isHeal = true;

                // citizen.GetComponent<Citizen_Helping>().getHeal();

                if (player != null)
                {
                    player.GetComponent<Player_Movement>().canShoot = false;
                }
            }
            else if (citizen.GetComponent<Citizen_Helping>().isSicked == false)
            {

            }
            else if (citizen.GetComponent<Citizen_Helping>().isDoneHealing)
            {
                TimeHealingBar.SetActive(false);
            }
        }
    }

    public void pointerUp()
    {
        TimeHealingBar.SetActive(false);
        if (player != null)
        {
            player.GetComponent<Player_Movement>().canShoot = true;
        }

        if (citizen != null)
        {
            citizen.GetComponent<Citizen_Helping>().isSicked = true;

            citizen.GetComponent<Citizen_Helping>().isHeal = false;

            citizen.GetComponent<Citizen_Helping>().timerGetHeal = 0f;

            GameObject myEventSystem = GameObject.Find("EventSystem");

            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
