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
    public float numberOfEnemies;
    [SerializeField] float difficultLevelsofTotal;

    private void Start()
    {
        spawningEnemy();
    }
    public void spawningEnemy()
    {
        while (difficultLevelsofTotal > 0)
        {
            int randomType = Random.Range(0, Enemy.Length);
            int randSpawnLocation = Random.Range(0, positionSpawn.Length);
            float difficultLevelOfVirus = Enemy[randomType].transform.GetChild(0).gameObject.GetComponent<Virus_Numeral>().returnDifficultLevel();
            if (difficultLevelOfVirus < difficultLevelsofTotal)
            {
                //Decrease number of enemies
                difficultLevelsofTotal -= difficultLevelOfVirus;
                Instantiate(Enemy[randomType], positionSpawn[randSpawnLocation].position, transform.rotation);
            }
        }
    }
}
/*
while (numberOfEnemies > 0)
        {
            int randomType = Random.Range(0, Enemy.Length);
            int randSpawnLocation = Random.Range(0, positionSpawn.Length);
            Instantiate(Enemy[randomType], positionSpawn[randSpawnLocation].position, transform.rotation);
            //Decrease number of enemies
            numberOfEnemies -= 1;
        }
*/

