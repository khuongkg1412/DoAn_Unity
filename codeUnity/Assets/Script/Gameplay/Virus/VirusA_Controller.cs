using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusA_Controller : MonoBehaviour
{
    //Setting up Enemy
    public Enemy virus;
    //Player targetPlayer to enemy move forward
    GameObject[] targetCitizen;
    Transform targetPlayer;
    //The orginal position of enemy, after out of range with player, enemy would comeback here
    Transform originalPos;

    //Decide whether enemy is following player
    public bool isFollow = true;

    //Time before enemy continute follow player after a collision
    float waiToFolllow = 0f;

    /*
    HP Enemy
    */
    GameObject gamePlay;
    //Max Health Point
    float maxHP;

    GameObject HealthBar;

    float maxHPsize;

    public bool isBoss = false;
    private void Start()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
        originalPos = transform.parent.gameObject.transform.GetChild(1).gameObject.transform;
        HealthBar = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        gamePlay = GameObject.Find("Canvas");
        virus = new VirusA();
        setNumeral();
        gameObject.GetComponent<Virus_Numeral>().settingNumeral(virus);
    }
    private void Update()
    {
        //Stop virus follow player for a secend
        if (!isFollow)
        {
            //Count to follow
            waiToFolllow += Time.deltaTime;
            if (waiToFolllow >= 0.5f)
            {
                waiToFolllow = 0;
                //Stop Enemy Moving After Beeing Hit
                Rigidbody2D rd = gameObject.GetComponent<Rigidbody2D>();
                rd.velocity = Vector2.zero;
                //Allow it follow player or Citizen
                isFollow = true;
            }
        }
        else if (virus != null && !isBoss)
        {
            //Follow if in range
            if (distanceToPlayer())
            {
                followPlayer();
            }
            else if (distanceToCitizen() != null)
            {
                followCitizen(distanceToCitizen());
            }
            //Out range then comeback to home position
            else
            {
                comeBackPos();
            }
        }

    }

    public void setNumeral()
    {
        maxHP = virus.returnHP();
        maxHPsize = HealthBar.transform.localScale.x;
        gameObject.GetComponent<SpriteRenderer>().sprite = virus.image;
    }
    bool distanceToPlayer()
    {
        if (Vector3.Distance(targetPlayer.position, transform.position) <= virus.detectRange)
        {
            return true;
        }
        return false;
    }

    GameObject distanceToCitizen()
    {
        //Find all citizen
        targetCitizen = GameObject.FindGameObjectsWithTag("Citizen");
        if (targetCitizen.Length > 0)
        {
            if (targetCitizen[0] != null)
            {
                //Set minimum value for fisrt object in list of obejcts citizens
                float minimumRange = Vector3.Distance(targetCitizen[0].transform.position, transform.position);
                GameObject target = targetCitizen[0];
                //Go throught list and check distance from enemy to citizens
                foreach (var i in targetCitizen)
                {
                    if (i != null)
                    {
                        //Set Value when new minimun range is found
                        if (Vector3.Distance(i.transform.position, transform.position) < minimumRange)
                        {
                            //Set mimum range value and object
                            minimumRange = Vector3.Distance(i.transform.position, transform.position);
                            target = i;
                        }
                    }
                }
                //If mimum range is in range following then return that target
                if (minimumRange <= virus.detectRange)
                {
                    return target;
                }
            }

        }

        //else return null
        return null;
    }
    //Comeback home position
    public void comeBackPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, originalPos.position, virus.returnSPD() * Time.deltaTime);
    }

    //Following p;ayer
    public void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, virus.returnSPD() * Time.deltaTime);
    }

    public void followCitizen(GameObject citizen)
    {
        transform.position = Vector3.MoveTowards(transform.position, citizen.transform.position, virus.returnSPD() * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //When being hit, enemy cannot move around
        isFollow = false;
        // force is how forcefully we will push the player away from the enemy.
        float force = 10000;
        // If the object we hit is the enemy
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Citizen")
        {
            // Calculate Angle Between the collision point and the player
            Vector2 dir = other.contacts[0].point - (Vector2)transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force.
            // This will push back the player
            Rigidbody2D rd = gameObject.GetComponent<Rigidbody2D>();
            rd.AddForce(dir * force);
            // Hit citizen , then decrease HP from zitizen
            if (other.gameObject.tag == "Citizen")
            {
                other.gameObject.GetComponent<Citizen_HP>().isSicked = true;
            }
        }

        /*
        Enemy HP
        */
        if (other.gameObject.tag == "Bullet")
        {
            virus.getDamage(other.gameObject.GetComponent<Bullet>().dameGiven);
            HealthBar.transform.localScale = new Vector3((virus.returnHP() / maxHP) * maxHPsize, HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
            if (virus.getDead())
            {
                Destroy(gameObject);
                if (gamePlay.GetComponent<Game_Start>() != null)
                {
                    GameObject.FindWithTag("Player").GetComponent<Player_Controller>().Character.setScore(20f); ;
                    gamePlay.GetComponent<Game_Start>().UpdateEnemyNumber(1);
                }
                else
                {
                    gamePlay.GetComponent<Game_Tutorial>().UpdateScore(20f);
                    gamePlay.GetComponent<Game_Tutorial>().UpdateEnemyNumber(1);
                }
            }
        }
    }
}
