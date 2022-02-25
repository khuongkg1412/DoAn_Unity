using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Citizen_Healing : MonoBehaviour
{

    //Helth bar object
    public GameObject TimeHealingBar;

    private GameObject citizen = null;

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
            if (citizen.GetComponent<Citizen_HP>().isSicked == true)
            {

                TimeHealingBar.SetActive(true);

                citizen.GetComponent<Citizen_HP>().isHeal = true;

                if (player != null)
                {
                    player.GetComponent<Player_Controller>().canShoot = false;
                }
            }
            else if (citizen.GetComponent<Citizen_HP>().isSicked == false)
            {
                citizen.GetComponent<Citizen_HP>().isDoneHealing = true;
            }
        }
    }

    public void pointerUp()
    {
        if (player != null)
        {
            player.GetComponent<Player_Controller>().canShoot = true;
        }

        if (citizen != null)
        {
            citizen.GetComponent<Citizen_HP>().resetHealing();

            GameObject myEventSystem = GameObject.Find("EventSystem");

            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
