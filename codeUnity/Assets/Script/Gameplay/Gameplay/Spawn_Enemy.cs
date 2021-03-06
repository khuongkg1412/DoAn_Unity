using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    public bool isBossStage;
    //Array Location to Spawn Enemy randomly
    public Transform[] positionSpawn;

    //Enemy object to spawn
    public GameObject Enemy;
    //Number of enemies is spawned
    public float numberOfEnemies;
    public float numberOfBoss;

    //Pos has been spawned
    public List<int> spawnedPos;
    private void Start()
    {
        spawningEnemy();
    }
    private void Update()
    {
        // if (isBossStage & numberOfEnemies > 0)
        // {
        //     //Create object
        //     Boss = Instantiate(Enemy, positionSpawn[0].position, transform.rotation);
        //     EnemyObject enemyObject = new EnemyObject();
        //     //Random type and set for virus enemy
        //     Boss.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus = enemyObject.VirusBoss();
        //     Boss.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().setNumeral(Boss.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus.returnHP());
        //     //Decrease number of enemies
        //     numberOfEnemies -= 1;
        // }
        // if (numberOfEnemies > 0)
        // {
        //     int randSpawnLocation = Random.Range(0, positionSpawn.Length);

        //     //Check for position has been spawned
        //     if (!spawnedPos.Contains(randSpawnLocation))
        //     {
        //         //add to pos has spawned
        //         spawnedPos.Add(randSpawnLocation);
        //         //Create object
        //         GameObject gameObjectNew = Instantiate(Enemy, positionSpawn[randSpawnLocation].position, transform.rotation);
        //         //Random type and set for virus enemy
        //         gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus = ranomVirusType();
        //         gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().setNumeral(gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus.returnHP());
        //         //Decrease number of enemies
        //         numberOfEnemies -= 1;
        //     }
        // }
        // else if (numberOfEnemies == 0)
        // {
        //     Destroy(Enemy);
        // }
    }
    public void spawningEnemy()
    {
        while (numberOfEnemies > 0)
        {
            int randomType = Random.Range(0, 3);
            switch (randomType)
            {
                case (int)VirusType.VirusA:
                    int randSpawnLocation = Random.Range(0, positionSpawn.Length);
                    //Check for position has been spawned
                    if (!spawnedPos.Contains(randSpawnLocation))
                    {
                        //add to pos has spawned
                        spawnedPos.Add(randSpawnLocation);
                        //Create object
                        GameObject gameObjectNew = Instantiate(Enemy, positionSpawn[randSpawnLocation].position, transform.rotation);
                        //Random type and set for virus enemy
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus = new VirusA();
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().setNumeral();
                        //Decrease number of enemies
                        numberOfEnemies -= 1;
                    }
                    break;
                case (int)VirusType.VirusB:
                    randSpawnLocation = Random.Range(0, positionSpawn.Length);
                    //Check for position has been spawned
                    if (!spawnedPos.Contains(randSpawnLocation))
                    {
                        //add to pos has spawned
                        spawnedPos.Add(randSpawnLocation);
                        //Create object
                        GameObject gameObjectNew = Instantiate(Enemy, positionSpawn[randSpawnLocation].position, transform.rotation);
                        //Random type and set for virus enemy
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus = new VirusB();
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().setNumeral();
                        //Decrease number of enemies
                        numberOfEnemies -= 1;
                    }
                    break;
                case (int)VirusType.VirusC:
                    randSpawnLocation = Random.Range(0, positionSpawn.Length);
                    //Check for position has been spawned
                    if (!spawnedPos.Contains(randSpawnLocation))
                    {
                        //add to pos has spawned
                        spawnedPos.Add(randSpawnLocation);
                        //Create object
                        GameObject gameObjectNew = Instantiate(Enemy, positionSpawn[randSpawnLocation].position, transform.rotation);
                        //Random type and set for virus enemy
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus = new VirusC();
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().setNumeral();
                        //Decrease number of enemies
                        numberOfEnemies -= 1;
                    }
                    break;
                case (int)VirusType.VirusD:
                    randSpawnLocation = Random.Range(0, positionSpawn.Length);
                    //Check for position has been spawned
                    if (!spawnedPos.Contains(randSpawnLocation))
                    {
                        //add to pos has spawned
                        spawnedPos.Add(randSpawnLocation);
                        //Create object
                        GameObject gameObjectNew = Instantiate(Enemy, positionSpawn[randSpawnLocation].position, transform.rotation);
                        //Random type and set for virus enemy
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().virus = new VirusD();
                        gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_Controller>().setNumeral();
                        //Decrease number of enemies
                        numberOfEnemies -= 1;
                    }
                    break;
            }
            if (numberOfEnemies == 0)
            {
                Destroy(Enemy);
            }
        }

    }
}
