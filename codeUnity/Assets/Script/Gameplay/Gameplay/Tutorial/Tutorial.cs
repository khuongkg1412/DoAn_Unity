using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] pointerTutorial;

    public GameObject enemy1, enemy2, citizen;

    public GameObject conditionPos, Player, tutorialPopup;

    public Text textContent;

    private bool isIncrease = true;
    int indexPannel = 0;

    private void Update()
    {
        //Check Increase condition
        checkIncrease();
        //Loop is setting active for each pointer
        for (int i = 0; i < pointerTutorial.Length; i++)
        {
            if (pointerTutorial[i] == null)
            {

            }
            else if (i != indexPannel)
            {
                pointerTutorial[i].SetActive(false);
            }
            else
            {
                pointerTutorial[i].SetActive(true);
            }
        }

    }
    //Increase index
    public void increaseIndex()
    {
        indexPannel += 1;
    }

    //Check increase method
    void checkIncrease()
    {   //Swicth case for indexPannel to check
        switch (indexPannel)
        {
            case 0: //Movement tutorial
                textContent.text = "Dragging the joystick area would allow you to move around.";
                break;
            case 1:  //Map tutorial
                textContent.text = "Look at the map area that would tell you about the surrounding objects.";
                break;
            case 2:  //Shoot tutorial
                textContent.text = "This is the button to adjust the direction of the shot, use it to destroy the enemy.";
                break;
            case 3: //Ennemy tutorial
                //Set active  for enemy
                enemy1.SetActive(true);
                textContent.text = "The virus has appeared. The icon red on the map represents the virus.";
                break;
            case 4:
                textContent.text = "They'll attack if someone is in the red circle. You should use your weapon to kill them before they hurt people.";
                break;
            case 5:

                if (GameObject.Find("Canvas").GetComponent<Game_Start>().returnScore() > 0)
                {
                    conditionPos.SetActive(true);
                    if (Vector3.Distance(Player.transform.position, conditionPos.transform.position) < 50f)
                    {
                        increaseIndex();
                        tutorialPopup.SetActive(true);
                        conditionPos.SetActive(false);
                        enemy2.SetActive(true);
                        citizen.SetActive(true);
                    }
                }
                else
                {
                    tutorialPopup.SetActive(false);
                }
                break;
            case 6:
                textContent.text = "The Citizen has appeared. The icon green on the map represents the citizen.";
                break;
            case 7:
                textContent.text = "They are under attack of virus. They would lose by 2 HP for a second, and they'll die if HP is 0. Keep them alive or you would lose.";
                break;
            case 8:
                textContent.text = "You must help them by touching them and holding the help button. Hold it for 7 seconds, they would be saved. Releasing the button would count from 0.";
                break;
            case 9:
                if (GameObject.Find("Canvas").GetComponent<Game_Start>().returnScore() > 100)
                {

                }
                else
                {
                    tutorialPopup.SetActive(false);
                }
                break;
        }
    }

    void playerGetDame()
    {
        textContent.text = "You must help them by touching them and holding the help button. Hold it for 7 seconds, they would be saved. Releasing the button would count from 0.";
    }
    //Move : This is the control button that allows your character to move.
    //Map : This is a map used to observe the targets around you.
    //Shoot: This is the button to adjust the direction of the shot, use it to destroy the enemy.
    //Virus: This is the enemy that needs to be destroyed.
    //Citizen : + The citizen is here. You can find where he is on the map with the green icon. 
    //+ He would get sick and lose health points while being near viruses and get attacked by them.
}
