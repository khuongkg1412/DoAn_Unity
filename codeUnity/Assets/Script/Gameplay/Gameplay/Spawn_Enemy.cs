using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    //Array Location to Spawn Enemy randomly
    public Transform[] positionSpawn;

    //Enemy object to spawn
    public GameObject Enemy;

    //Number of enemies is spawned
    public float numberOfEnemies;

    //Pos has been spawned
    public List<int> spawnedPos;

    private void Update()
    {
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
                Debug.Log("RunA");
                return virusExample.VirusA();
            case (int)VirusType.VirusB:
                Debug.Log("RunB");
                return virusExample.VirusA();
            case (int)VirusType.VirusC:
                Debug.Log("RunC");
                return virusExample.VirusA();
            case (int)VirusType.VirusD:
                Debug.Log("RunD");
                return virusExample.VirusA();
        }
        return null;
    }
}
