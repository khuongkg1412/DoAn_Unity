using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Citizen_Healing : MonoBehaviour
{
    float countTime = 0f;

    [System.Obsolete]
    public void selectedUpdate()
    {
        if (GameObject.Find("Citizen").active)
        {
            if (
                GameObject
                    .Find("Citizen")
                    .GetComponent<Citizen_Helping>()
                    .isSicked ==
                true
            )
            {
                countTime += Time.deltaTime;
                GameObject
                    .Find("Citizen")
                    .GetComponent<Citizen_Helping>()
                    .isSicked = false;
                GameObject
                    .Find("Citizen")
                    .GetComponent<Citizen_Helping>()
                    .isHeal = true;
                GameObject
                    .Find("Citizen")
                    .GetComponent<Citizen_Helping>()
                    .getHeal();
            }
        }
    }

    public void pointerUp()
    {
        if (GameObject.Find("Citizen").active)
        {
            countTime = 0f;
            GameObject
                .Find("Citizen")
                .GetComponent<Citizen_Helping>()
                .isSicked = true;
            GameObject.Find("Citizen").GetComponent<Citizen_Helping>().isHeal =
                false;
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem
                .GetComponent<UnityEngine.EventSystems.EventSystem>()
                .SetSelectedGameObject(null);
        }
    }
}
