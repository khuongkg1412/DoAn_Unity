using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] pointerTutorial;

    public GameObject enemy1, enemy2, citizen;

    public GameObject conditionPos, Player;

    public Text textContent, textTile;

    private bool isIncrease = true;
    int indexPannel = 0;

    private void Update()
    {
        checkIncrease();
        for (int i = 0; i < pointerTutorial.Length; i++)
        {
            if (i == indexPannel)
            {
                pointerTutorial[i].SetActive(true);
            }
            else
            {
                pointerTutorial[i].SetActive(false);
            }
        }

    }

    public void increaseIndex()
    {
        // if (indexPannel == 5)
        // {
        //     if (
        //         text1.text ==
        //         "He would get sick and lose health points while being near viruses and get attacked by them."
        //     )
        //     {
        //         indexPannel += 1;
        //     }
        //     text1.text =
        //         "He would get sick and lose health points while being near viruses and get attacked by them.";
        // }
        // else
        // {
        //     indexPannel += 1;
        // }
        Debug.Log("Run");
        if (isIncrease)
        {
            indexPannel += 1;
            isIncrease = false;
        }
    }

    void checkIncrease()
    {
        if (indexPannel < 3)
        {
            isIncrease = true;
            if (indexPannel == 0)
            {
                textContent.text = "This is the control button that allows your character to move.";
                textTile.text = "Movement";
            }
            if (indexPannel == 1)
            {
                textContent.text = "This is a map used to observe the targets around you.";
                textTile.text = "Map";
            }
            if (indexPannel == 2)
            {
                textContent.text = "This is the button to adjust the direction of the shot, use it to destroy the enemy.";
                textTile.text = "Shoot";
            }
        }
        else
        {
            if (indexPannel == 3)
            {
                enemy1.SetActive(true);
            }
            if (GameObject.Find("Canvas").GetComponent<Game_Start>().score > 0 &&
                indexPannel == 4)
            {
                conditionPos.SetActive(true);
                if (Vector3.Distance(Player.transform.position, conditionPos.transform.position) < 50f)
                {
                    increaseIndex();
                    conditionPos.SetActive(false);
                    enemy2.SetActive(true);
                    citizen.SetActive(true);
                }
            }

        }
    }
    //Move : This is the control button that allows your character to move.
    //Map : This is a map used to observe the targets around you.
    //Shoot: This is the button to adjust the direction of the shot, use it to destroy the enemy.
    //Virus: This is the enemy that needs to be destroyed.
    //Citizen : + The citizen is here. You can find where he is on the map with the green icon. 
    //+ He would get sick and lose health points while being near viruses and get attacked by them.
}
