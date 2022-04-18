using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    //Array Location to Spawn Enemy randomly
    public Transform[] positionSpawn;

    //Enemy object to spawn
    public GameObject[] Enemy;
    //Number of enemies is spawned
    float numberOfEnemies = 0;
    [SerializeField] float difficultLevelsofTotal;

    //Pos has been spawned
    List<int> spawnedPos = new List<int>();
    private void Start()
    {
        spawningEnemy();
    }
    public void spawningEnemy()
    {
        while (difficultLevelsofTotal > 0)
        {
            //Get random position in array
            int randomType = Random.Range(0, Enemy.Length);
            int randSpawnLocation = Random.Range(0, positionSpawn.Length);
            //Get the difficult of virus at random position
            float difficultLevelOfVirus = Enemy[randomType].transform.GetChild(0).gameObject.GetComponent<Virus_Numeral>().returnDifficultLevel();
            //Instantiate virus if the difficult level is less than the total level of difficult
            if (difficultLevelOfVirus <= difficultLevelsofTotal && !spawnedPos.Contains(randSpawnLocation))
            {
                numberOfEnemies += 1;
                //add to pos has spawned
                spawnedPos.Add(randSpawnLocation);
                //Decrease number of enemies
                difficultLevelsofTotal -= difficultLevelOfVirus;
                Instantiate(Enemy[randomType], positionSpawn[randSpawnLocation].position, transform.rotation);
            }
        }
        GameObject.Find("Canvas").GetComponent<Game_Start>().settingNumberOfVirus(numberOfEnemies);
    }
}


