using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Citizen : MonoBehaviour
{
    //Array Location to Spawn Enemy randomly
    public Transform[] positionSpawn;

    //Enemy object to spawn
    public GameObject Citizen;

    //Number of enemies is spawned
    public float numberOfCitizen;

    //Pos has been spawned
    public List<int> spawnedPos;

    private void Update()
    {
        if (numberOfCitizen > 0)
        {
            int randSpawnLocation = Random.Range(0, positionSpawn.Length);

            //Check for position has been spawned
            if (!spawnedPos.Contains(randSpawnLocation))
            {
                //add to pos has spawned
                spawnedPos.Add(randSpawnLocation);
                //Create object
                Instantiate(Citizen,
                positionSpawn[randSpawnLocation].position,
                transform.rotation);
                //Decrease number of enemies
                numberOfCitizen -= 1;
            }
        }
        else if (numberOfCitizen == 0)
        {
            Destroy(Citizen);
        }
    }
}
