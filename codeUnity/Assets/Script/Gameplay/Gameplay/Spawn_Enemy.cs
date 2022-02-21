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

    public GameObject Boss;

    //Number of enemies is spawned
    public float numberOfEnemies;

    //Pos has been spawned
    public List<int> spawnedPos;

    private void Update()
    {
        if (isBossStage & numberOfEnemies > 0)
        {
            //Create object
            Boss = Instantiate(Enemy, positionSpawn[0].position, transform.rotation);
            EnemyObject enemyObject = new EnemyObject();
            //Random type and set for virus enemy
            Boss.transform.GetChild(0).gameObject.GetComponent<Enemy>().virus = enemyObject.VirusBoss();
            Boss.transform.GetChild(0).gameObject.GetComponent<Enemy>().setVirusImage();
            Boss.transform.GetChild(0).gameObject.GetComponent<Enemy_HP>().setNumeral(Boss.transform.GetChild(0).gameObject.GetComponent<Enemy>().virus.returnHP());
            //Decrease number of enemies
            numberOfEnemies -= 1;
        }
        if (numberOfEnemies > 0)
        {
            int randSpawnLocation = Random.Range(0, positionSpawn.Length);

            //Check for position has been spawned
            if (!spawnedPos.Contains(randSpawnLocation))
            {
                //add to pos has spawned
                spawnedPos.Add(randSpawnLocation);
                //Create object
                GameObject gameObjectNew = Instantiate(Enemy, positionSpawn[randSpawnLocation].position, transform.rotation);
                //Random type and set for virus enemy
                gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy>().virus = ranomVirusType();
                gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy>().setVirusImage();
                gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy_HP>().setNumeral(gameObjectNew.transform.GetChild(0).gameObject.GetComponent<Enemy>().virus.returnHP());
                //Decrease number of enemies
                numberOfEnemies -= 1;
            }
        }
        else if (numberOfEnemies == 0)
        {
            Destroy(Enemy);
        }
    }

    EnemyObject ranomVirusType()
    {
        EnemyObject virusExample = new EnemyObject();
        int randomType = Random.Range(0, 3);
        switch (randomType)
        {
            case (int)VirusType.VirusA:
                return virusExample.VirusA();
            case (int)VirusType.VirusB:
                return virusExample.VirusB();
            case (int)VirusType.VirusC:
                return virusExample.VirusC();
            case (int)VirusType.VirusD:
                return virusExample.VirusD();
        }
        return null;
    }
}
