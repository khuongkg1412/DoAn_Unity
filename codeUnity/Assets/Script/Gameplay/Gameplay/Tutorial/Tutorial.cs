using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] pannelArray;

    public GameObject enemy1, enemy2, citizen;

    public GameObject conditionPos, Player;

    public Text text1;

    int indexPannel = 0;

    private void Update()
    {
        for (int i = 0; i < pannelArray.Length; i++)
        {
            if (indexPannel == i)
            {
                if (pannelArray[i] != null)
                {
                    pannelArray[i].SetActive(true);
                }
            }
            else
            {
                if (pannelArray[i] != null)
                {
                    pannelArray[i].SetActive(false);
                }
            }
        }
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

    public void increaseIndex()
    {
        if (indexPannel == 5)
        {
            if (
                text1.text ==
                "He would get sick and lose health points while being near viruses and get attacked by them."
            )
            {
                indexPannel += 1;
            }
            text1.text =
                "He would get sick and lose health points while being near viruses and get attacked by them.";
        }
        else
        {
            indexPannel += 1;
        }
    }
}
