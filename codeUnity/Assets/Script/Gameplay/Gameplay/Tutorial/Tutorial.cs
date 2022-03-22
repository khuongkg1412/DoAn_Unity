using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] pointerTutorial;

    public GameObject enemy, citizen;

    public Transform spwanPos1, SpawnPos2;

    public GameObject conditionPos, Player, tutorialPopup;

    public Text textContent;

    private bool isIncrease = true;
    int indexPannel = 0;
    private GameObject player;
    GameObject gameObjectNew;
    float numberOfEnemies = 2;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
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
                GameObject.FindWithTag("Player").GetComponent<Player_Controller>().Character.setShoot(false);
                break;
            case 1:  //Map tutorial
                textContent.text = "Look at the map area that would tell you about the surrounding objects.";
                break;
            case 2:  //Shoot tutorial
                textContent.text = "This is the button to adjust the direction of the shot, use it to destroy the enemy.";
                break;
            case 3: //Ennemy tutorial
                    //Check number of virus has been spawned
                if (numberOfEnemies == 2)
                {
                    //Create object
                    //Random type and set for virus enemy
                    Instantiate(enemy, spwanPos1.position, spwanPos1.rotation);
                    //Decrease number of virus
                    numberOfEnemies -= 1;
                }

                textContent.text = "The virus has appeared. The icon red on the map represents the virus.";
                break;
            case 4:
                textContent.text = "They'll attack if someone is in the red circle. You should use your weapon to kill them before they hurt people.";
                break;
            case 5:
                player.GetComponent<Player_Controller>().Character.setShoot(true);
                if (GameObject.Find("Canvas").GetComponent<Game_Tutorial>().returnScore() > 0)
                {
                    conditionPos.SetActive(true);
                    if (Vector3.Distance(Player.transform.position, conditionPos.transform.position) < 50f)
                    {
                        increaseIndex();
                        tutorialPopup.SetActive(true);
                        conditionPos.SetActive(false);
                        citizen.SetActive(true);
                        player.GetComponent<Player_Controller>().Character.setShoot(false);
                        player.GetComponent<Player_Controller>().Character.setShoot(false);
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
                //Check number of virus has been spawned
                if (numberOfEnemies == 1)
                {//Create object
                    gameObjectNew = Instantiate(enemy, SpawnPos2.position, SpawnPos2.rotation);
                    //Random type and set for virus enemy
                    gameObjectNew.transform.GetChild(0).gameObject.GetComponent<VirusA_Controller>().isFollow = false;
                    //Decrease number of virus
                    numberOfEnemies -= 1;
                }
                break;
            case 8:
                textContent.text = "You must help them by touching them and holding the help button for 7 seconds. Releasing the button would count from 0.";
                gameObjectNew.transform.GetChild(0).gameObject.GetComponent<VirusA_Controller>().isFollow = false;
                break;
            case 9:
                player.GetComponent<Player_Controller>().Character.setMove(true);
                player.GetComponent<Player_Controller>().Character.setShoot(true);
                tutorialPopup.SetActive(false);
                increaseIndex();
                break;
            case 10:
                if (GameObject.Find("Canvas").GetComponent<Game_Tutorial>().returnScore() > 100)
                {
                    GameObject.Find("Canvas").GetComponent<Game_Tutorial>().isGameOver = true;
                    Player_DataManager.Instance.Player.level.stage += 1;
                    //Call to update the information off Player
                    Player_Update.UpdatePlayer();
                    Destroy(gameObject);
                }
                break;
        }
    }
}
