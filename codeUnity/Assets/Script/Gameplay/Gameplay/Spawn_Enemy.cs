using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    public bool isBossStage;
    //Array Location to Spawn Enemy randomly
    public Transform[] positionSpawn;

    //Enemy object to spawn
    public GameObject[] Enemy;
    //Number of enemies is spawned
    public float numberOfEnemies;
    public float numberOfBoss;

    //Pos has been spawned
    public List<int> spawnedPos;
    private void Start()
    {
        spawningEnemy();
    }
    public void spawningEnemy()
    {
        while (numberOfEnemies > 0)
        {
            int randomType = Random.Range(0, Enemy.Length);
            int randSpawnLocation = Random.Range(0, positionSpawn.Length);
            Instantiate(Enemy[randomType], positionSpawn[randSpawnLocation].position, transform.rotation);
            //Decrease number of enemies
            numberOfEnemies -= 1;
        }

    }
}
