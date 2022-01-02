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
                    player.GetComponent<Player_Movement>().canShoot = false;
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
        TimeHealingBar.SetActive(false);
        if (player != null)
        {
            player.GetComponent<Player_Movement>().canShoot = true;
        }

        if (citizen != null)
        {
            citizen.GetComponent<Citizen_HP>().isHeal = false;

            citizen.GetComponent<Citizen_HP>().timerGetHeal = 7f;

            GameObject myEventSystem = GameObject.Find("EventSystem");

            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
